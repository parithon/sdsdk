using System;
using System.Threading.Tasks;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK.Abstracts
{
  public interface IStreamDeckClient
  {
    event EventHandler<EventArgs> Connected;
    event EventHandler<EventArgs> Disconnected;

    event EventHandler<ApplicationDidLaunchEvent> ApplicationDidLaunch;
    event EventHandler<ApplicationDidTerminateEvent> ApplicationDidTerminate;
    event EventHandler<DeviceDidConnectEvent> DeviceDidConnect;
    event EventHandler<DeviceDidDisconnectEvent> DeviceDidDisconnect;
    event EventHandler<DidReceiveGlobalSettingsEvent> DidReceiveGlobalSettings;
    event EventHandler<DidReceiveSettingsEvent> DidReceiveSettings;
    event EventHandler<KeyDownEvent> KeyDown;
    event EventHandler<KeyUpEvent> KeyUp;
    event EventHandler<TitleParametersDidChangeEvent> TitleParametersDidChange;
    event EventHandler<WillAppearEvent> WillAppear;
    event EventHandler<WillDisappearEvent> WillDisappear;
    event EventHandler<SendToPluginEvent> SendToPlugin;
    event EventHandler<EventArgs> PropertyInspectorDidAppear;
    event EventHandler<EventArgs> PropertyInspectorDidDisappear;
    event EventHandler<EventArgs> SystemDidWakeUp;

    string UUID { get; }

    void Execute();
    Task GetSettingsAsync(string context);
    Task GetGlobalSettingsAsync(string context);
  }
}
