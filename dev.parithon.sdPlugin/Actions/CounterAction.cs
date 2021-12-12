using System.Timers;
using Timer = System.Timers.Timer;

namespace dev.parithon.sdPlugin.Actions
{
  internal class CounterAction : StreamDeckAction
  {
    private readonly Timer timer = new(TimeSpan.FromSeconds(2).TotalMilliseconds);
    private short counter = 0;

    public CounterAction()
    {
      States.Add(new StreamDeckActionState()
      {
        Image = "Images/blank",
        TitleAlignment = Alignment.Middle
      });
      timer.AutoReset = true;
      timer.Elapsed += async (s, e) => {
        timer.Stop();
        counter = 0;
        await SetTitle();
      };
    }

    public override string Icon => "Images/blank";

    public override string Name => "Counter";

    public override string PropertyInspectorPath => "Images/CounterAction.html";

    public override async void Initialize(AppearPayload payload)
    {
      if (payload.Settings.Counter != null)
      {
        counter = payload.Settings.Counter;
        await SetTitle();
      }
      base.Initialize(payload);
    }

    public override void OnKeyDown(KeyPayload payload)
    {
      timer.Start();
    }

    public override async void OnKeyUp(KeyPayload payload)
    {
      if (timer.Enabled)
      {
        timer.Stop();
        counter++;
        await SetTitle();
      }
    }

    private async Task SetTitle()
    {
      await SendAsync(new SetSettingsMessage(Context, new { Counter = counter }));
      await SendAsync(new SetTitleMessage(Context, $"{counter}"));
    }
  }
}
