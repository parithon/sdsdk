using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Events
{
  public class SendToPluginEvent : StreamDeckEvent
  {
    [JsonProperty("action")]
    public string Action { get; private set; }

    [JsonProperty("event")]
    public override string Event => StreamDeckEvent.SendToPlugin;

    [JsonProperty("Context")]
    public string Context { get; private set; }

    [JsonProperty("payload")]
    public dynamic Payload { get; private set; }
  }
}
