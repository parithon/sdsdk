using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Messages
{
  public interface IMessage
  {
    [JsonProperty("event")]
    string Event { get; }
  }
}
