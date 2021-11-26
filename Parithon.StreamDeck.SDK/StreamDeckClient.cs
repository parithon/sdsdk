using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parithon.StreamDeck.SDK.Events;
using Parithon.StreamDeck.SDK.Messages;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK
{
  public class StreamDeckClient
  {
    private readonly int _port;
    private readonly string _uuid;
    private readonly string _registerEvent;
    private readonly string _info;
    private readonly CancellationToken _cancellationToken;
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, Type> _registeredActions;
    private readonly ManualResetEvent _connectedEvent = new(false);
    private readonly ManualResetEvent _disconnectedEvent = new(false);
    private readonly SemaphoreSlim _sendToStreamDeckSemaphore = new(1);
    private readonly Dictionary<string, StreamDeckAction> _actions = new();

    private ClientWebSocket client = null;

    public StreamDeckClient(IOptions<StreamDeckStartupArguments> args, CancellationTokenSource cts, IServiceProvider serviceProvider)
    {
      (this._port, this._uuid, this._registerEvent, this._info) = args.Value;
      this._cancellationToken = cts.Token;
      this._serviceProvider = serviceProvider;
      this._registeredActions = serviceProvider.GetRequiredService<Dictionary<string, Type>>();
    }

    #region StreamDeckClient events
    public event EventHandler<EventArgs> Connected;
    public event EventHandler<EventArgs> Disconnected;
    #endregion // StreamDeckClient events

    #region StreamDeck events
    public event EventHandler<ApplicationDidLaunchEvent> ApplicationDidLaunch;
    public event EventHandler<ApplicationDidTerminateEvent> ApplicationDidTerminate;
    public event EventHandler<DeviceDidConnectEvent> DeviceDidConnect;
    public event EventHandler<DeviceDidDisconnectEvent> DeviceDidDisconnect;
    public event EventHandler<DidReceiveGlobalSettingsEvent> DidReceiveGlobalSettings;
    public event EventHandler<DidReceiveSettingsEvent> DidReceiveSettings;
    public event EventHandler<KeyDownEvent> KeyDown;
    public event EventHandler<KeyUpEvent> KeyUp;
    public event EventHandler<TitleParametersDidChangeEvent> TitleParametersDidChange;
    public event EventHandler<WillAppearEvent> WillAppear;
    public event EventHandler<WillDisappearEvent> WillDisappear;
    #endregion // StreamDeck events

    public void Execute()
    {
      if (client == null)
      {
        this.client = new();
        this.RunAsync();
      }

      if (!this._connectedEvent.WaitOne(TimeSpan.FromSeconds(10)))
      {
        // Block thread until we disconnect.
        while (!this._disconnectedEvent.WaitOne(TimeSpan.FromMilliseconds(100)))
        {
          Thread.Sleep(1);
        }
      }
    }

    public Task GetSettingsAsync(string context)
    {
      return this.SendAsync(new GetSettingsMessage(context));
    }

    public Task GetGlobalSettingsAsync(string context)
    {
      return this.SendAsync(new GetGlobalSettingsMessage(context));
    }

    private async void RunAsync()
    {
      try
      {
        await this.client.ConnectAsync(new Uri($"ws://localhost:{this._port}"), _cancellationToken);

        if (this.client.State != WebSocketState.Open)
        {
          await this.StopAsync();
          return;
        }

        await this.SendAsync(new RegisterEventMessage(this._registerEvent, this._uuid));

        Connected?.Invoke(this, EventArgs.Empty);

        await this.ReceiveAsync();
      }
      finally
      {
        await this.StopAsync();
      }
    }

    private async Task StopAsync()
    {
      if (this.client == null) return;
      ClientWebSocket socket = this.client;
      this.client = null;
      await Task.Run(() =>
      {
        CancellationTokenSource cts = new();
        _ = socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnecting", cts.Token);
        if (!this._disconnectedEvent.WaitOne(TimeSpan.FromMilliseconds(100)))
        {
          cts.Cancel();
        }
      });

      try
      {
        socket.Dispose();
      }
      catch { }

      Disconnected?.Invoke(this, EventArgs.Empty);
    }

    private async Task SendAsync(IMessage message)
    {
      await this.SendAsync(JsonConvert.SerializeObject(message, new JsonSerializerSettings()
      {
        NullValueHandling = NullValueHandling.Ignore
      }));
    }

    private async Task SendAsync(string json)
    {
      if (this.client == null) return;

      try
      {
        await this._sendToStreamDeckSemaphore.WaitAsync();

        byte[] buffer = Encoding.UTF8.GetBytes(json);

        await this.client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, endOfMessage: true, this._cancellationToken);
      }
      catch
      {
        await this.StopAsync();
      }
      finally
      {
        this._sendToStreamDeckSemaphore.Release();
      }
    }

    private async Task ReceiveAsync()
    {
      int bufferSize = 1024 * 1024;
      byte[] buffer = new byte[bufferSize];
      ArraySegment<byte> bufferArray = new(buffer);
      StringBuilder stringBuilder = new();

      while (!this._cancellationToken.IsCancellationRequested && this.client != null)
      {
        WebSocketReceiveResult result = await this.client.ReceiveAsync(bufferArray, this._cancellationToken);
        if (result == null) continue;

        if (result.MessageType == WebSocketMessageType.Close ||
              result.CloseStatus != null && result.CloseStatus.HasValue && result.CloseStatus.Value != WebSocketCloseStatus.Empty)
        {
          return;
        }
        else if (result.MessageType != WebSocketMessageType.Text)
        {
          return;
        }

        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
        if (result.EndOfMessage)
        {
          string json = stringBuilder.ToString();
          stringBuilder.Clear();
          StreamDeckEvent data = JsonConvert.DeserializeObject<StreamDeckEvent>(json, new JsonSerializerSettings()
          {
            Converters = new[] { new StreamDeckEventSerializer() }
          });
          if (data == null) continue;
#if DEBUG
          System.Diagnostics.Debug.WriteLine($"Event: {data.Event}");
#endif
          switch (data.Event)
          {
            case StreamDeckEvent.ApplicationDidLaunch:
              var launchevt = data as ApplicationDidLaunchEvent;
              ApplicationDidLaunch?.Invoke(this, launchevt);
              break;
            case StreamDeckEvent.ApplicationDidTerminate:
              var termevt = data as ApplicationDidTerminateEvent;
              ApplicationDidTerminate?.Invoke(this, termevt);
              break;
            case StreamDeckEvent.DeviceDidConnect:
              var connectevt = data as DeviceDidConnectEvent;
              DeviceDidConnect?.Invoke(this, connectevt);
              break;
            case StreamDeckEvent.DeviceDidDisconnect:
              var disconnectevt = data as DeviceDidDisconnectEvent;
              DeviceDidDisconnect?.Invoke(this, disconnectevt);
              break;
            case StreamDeckEvent.DidReceiveGlobalSettings:
              var globalsettingsevt = data as DidReceiveGlobalSettingsEvent;
              DidReceiveGlobalSettings?.Invoke(this, globalsettingsevt);
              break;
            case StreamDeckEvent.DidReceiveSettings:
              var receivesettingsevt = data as DidReceiveSettingsEvent;
              DidReceiveSettings?.Invoke(this, receivesettingsevt);
              break;
            case StreamDeckEvent.KeyDown:
              var keydownevt = data as KeyDownEvent;
              KeyDownAction(keydownevt.Context, keydownevt.Payload);
              KeyDown?.Invoke(this, keydownevt);
              break;
            case StreamDeckEvent.KeyUp:
              var keyupevt = data as KeyUpEvent;
              KeyUpAction(keyupevt.Context, keyupevt.Payload);
              KeyUp?.Invoke(this, keyupevt);
              break;
            case StreamDeckEvent.TitleParametersDidChange:
              var titleevt = data as TitleParametersDidChangeEvent;
              SetActionTitle(titleevt.Context, titleevt.Payload);
              TitleParametersDidChange?.Invoke(this, titleevt);
              break;
            case StreamDeckEvent.WillAppear:
              if (data is not WillAppearEvent appearevt) continue;
              RegisterAction(appearevt.Action, appearevt.Context, appearevt.Payload);
              WillAppear?.Invoke(this, appearevt);
              break;
            case StreamDeckEvent.WillDisappear:
              var disappearevt = data as WillDisappearEvent;
              UnregisterAction(disappearevt.Context);
              WillDisappear?.Invoke(this, disappearevt);
              break;
            default:
              break;
          }
        }
      }
    }

    private void KeyDownAction(string context, KeyPayload payload)
    {
      if (!this._actions.TryGetValue($"{context}", out StreamDeckAction action)) return;
      action.OnKeyDown(payload);
    }

    private void KeyUpAction(string context, KeyPayload payload)
    {
      if (!this._actions.TryGetValue($"{context}", out StreamDeckAction action)) return;
      action.OnKeyUp(payload);
    }

    private void RegisterAction(string action, string context, AppearPayload payload)
    {
      if (this._actions.ContainsKey(context)) return;
      if (!this._registeredActions.TryGetValue(action, out Type actionType)) return;
      var a = this._serviceProvider.GetRequiredService(actionType) as StreamDeckAction;
      a.Context = context;
      a.SendAsync = this.SendAsync;
      a.Initialize(payload);
      this._actions.Add($"{context}", a);
    }

    private void UnregisterAction(string context)
    {
      var key = $"{context}";
      if (!this._actions.ContainsKey(key)) return;
      this._actions.Remove(key);
      GC.Collect();
    }

    private void SetActionTitle(string context, TitleParametersPayload payload)
    {
      if (!this._actions.TryGetValue(context, out StreamDeckAction action)) return;
      action.SetTitle(payload.Title);
    }
  }
}
