using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Events
{
  public class PropertyInspectorDisappearEvent : StreamDeckEvent
  {
    [JsonProperty("action")]
    public string Action { get; private set; }

    [JsonProperty("event")]
    public override string Event => PropertyInspectorDidDisappear;

    [JsonProperty("context")]
    public string Context { get; private set; }

    [JsonProperty("device")]
    public string Device { get; private set; }
  }
}
