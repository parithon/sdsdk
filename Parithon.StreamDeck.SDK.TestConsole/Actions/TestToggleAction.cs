using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parithon.StreamDeck.SDK;
using Parithon.StreamDeck.SDK.Messages;

namespace TestConsole.Actions
{
  public class TestToggleAction : StreamDeckAction
  {
    private readonly object me;

    public TestToggleAction(object me)
    {
      this.me = me;
      this.States.Add(new StreamDeckActionState { Image = "Images/virt_cam_off" });
      this.States.Add(new StreamDeckActionState { Image = "Images/virt_cam_on" });
    }

    public override string Icon => "Images/virt_cam_on";

    public override string Name => "Test Action";

    public override void OnKeyDown(KeyPayload payload)
    {
    }

    public override void OnKeyUp(KeyPayload payload)
    {
    }
  }
}
