using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public class Coordinates
  {
    [JsonProperty("row")]
    public short Row { get; private set; }

    [JsonProperty("column")]
    public short Column { get; private set; }
  }
}
