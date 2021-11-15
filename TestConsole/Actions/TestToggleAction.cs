using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parithon.StreamDeck.SDK.Core;

namespace TestConsole.Actions
{
  public class TaskAction : StreamDeckAction
  {
    public TaskAction()
    {
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
