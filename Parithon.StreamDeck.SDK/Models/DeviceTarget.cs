using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parithon.StreamDeck.SDK.Models
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum DeviceTarget : short
  {
    [JsonProperty("software")]
    Software,
    [JsonProperty("hardware")]
    Hardware,
    [JsonProperty("both")]
    Both
  }
}
