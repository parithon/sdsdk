using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  public class WillDisappearEvent : StreamDeckEvent
  {
    [JsonProperty("action")]
    public string Action { get; private set; }

    [JsonProperty("event")]
    public override string Event => WillDisappear;

    [JsonProperty("context")]
    public string Context { get; private set; }

    [JsonProperty("device")]
    public string Device { get; private set; }

    [JsonProperty("payload")]
    public AppearPayload Payload { get; private set; }
  }
}
