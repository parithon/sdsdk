using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK.Messages
{
  public class SetStateMessage : IMessage
  {
    public SetStateMessage(string context, short state)
    {
      this.Context = context;
      this.Payload = new { state };
    }

    [JsonProperty("event")]
    public string Event => StreamDeckEvent.SetState;

    [JsonProperty("context")]
    public string Context { get; }

    [JsonProperty("payload")]
    public dynamic Payload { get; }
  }
}
