﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Parithon.StreamDeck.SDK;
using Parithon.StreamDeck.SDK.Messages;
using Parithon.StreamDeck.SDK.Models;

namespace Dev.Parithon.StreamDeck.TestConsole.Actions
{
  public class CounterAction : StreamDeckAction
  {
    private readonly Timer timmer = new(TimeSpan.FromSeconds(1).TotalMilliseconds);
    private short counter = 0;

    public CounterAction()
    {
      States.Add(new StreamDeckActionState()
      {
        Image = "Images/virt_cam_on",
        TitleAlignment = Alignment.Bottom
      });
      timmer = new(TimeSpan.FromSeconds(2).TotalMilliseconds);
      timmer.AutoReset = true;
      timmer.Elapsed += async (s, e) =>
      {
        timmer.Stop();
        counter = 0;
        await SetTitle();
      };
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:Using dynamic types might cause types or members to be removed by trimmer.", Justification = "Settings are an end user object.")]
    public override async void Initialize(AppearPayload payload)
    {
      if (payload.Settings.Counter != null)
      {
        counter = payload.Settings.Counter;
        await SetTitle();
      }
      base.Initialize(payload);
    }

    private async Task SetTitle()
    {
      await SendAsync(new SetSettingsMessage(Context, new { Counter = counter }));
      await SendAsync(new SetTitleMessage(Context, $"{counter}"));
    }

    public override string Icon => "Images/virt_cam_on";

    public override string Name => "Counter";

    public override string PropertyInspectorPath => "PI/CounterAction.html";

    public override void SendToPlugin(dynamic payload)
    {
#if DEBUG
      System.Diagnostics.Debug.WriteLine($"SendToPlugin: {payload}");
      System.Diagnostics.Debug.WriteLine($"command: {payload.command}");
#endif
    }

    public override void PropertyInspector(bool isVisible)
    {
#if DEBUG
      System.Diagnostics.Debug.WriteLine($"PropertyInspector: {isVisible}");
#endif
      base.PropertyInspector(isVisible);
    }

    public override void OnKeyDown(KeyPayload payload)
    {
      timmer.Start();
    }

    public override async void OnKeyUp(KeyPayload payload)
    {
      if (timmer.Enabled)
      {
        timmer.Stop();
        counter++;
        await SetTitle();
      }
    }
  }
}
