using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK.Events
{
  public class PropertyInspectorAppearEvent : StreamDeckEvent
  {
    public string Action { get; private set; }
    public override string Event => PropertyInspectorDidAppear;
    public string Context { get; private set; }
    public string Device { get; private set; }
  }
}
