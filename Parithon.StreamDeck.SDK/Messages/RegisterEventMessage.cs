using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Messages
{
  internal class RegisterEventMessage : IMessage
  {
    public RegisterEventMessage(string registerEvent, string uuid)
    {
      this.Event = registerEvent;
      this.UUID = uuid;
    }

    [JsonProperty("event")]
    public string Event { get; }

    [JsonProperty("uuid")]
    public string UUID { get; }
  }
}
