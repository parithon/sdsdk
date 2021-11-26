using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK
{
  public interface IMessage
  {
    [JsonProperty("event")]
    string Event { get; }
  }
}
