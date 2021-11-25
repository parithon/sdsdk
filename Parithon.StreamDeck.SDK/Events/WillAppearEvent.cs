using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  public class WillAppearEvent : StreamDeckEvent
  {
    [JsonProperty("action")]
    public string Action { get; private set; }

    [JsonProperty("event")]
    public override string Event => WillAppear;

    [JsonProperty("context")]
    public string Context { get; private set; }

    [JsonProperty("device")]
    public string Device { get; private set; }

    [JsonProperty("payload")]
    public AppearPayload Payload { get; private set; }
  }
}
