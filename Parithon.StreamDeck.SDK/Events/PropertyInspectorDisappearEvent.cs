using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK.Events
{
  public class PropertyInspectorDisappearEvent : StreamDeckEvent
  {
    public string Action { get; private set; }
    public override string Event => PropertyInspectorDidDisappear;
    public string Context { get; private set; }
    public string Device { get; private set; }
  }
}
