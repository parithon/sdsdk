using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  public class ApplicationDidTerminateEvent : StreamDeckEvent
  {
    [JsonProperty("event")]
    public override string Event => ApplicationDidTerminate;

    [JsonProperty("payload")]
    public ApplicationPayload Payload { get; private set; }
  }
}
