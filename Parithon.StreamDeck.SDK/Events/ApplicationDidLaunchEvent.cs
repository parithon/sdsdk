using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  public class ApplicationDidLaunchEvent : StreamDeckEvent
  {
    [JsonProperty("event")]
    public override string Event => ApplicationDidLaunch;

    [JsonProperty("payload")]
    public ApplicationPayload Payload { get; private set; }
  }
}
