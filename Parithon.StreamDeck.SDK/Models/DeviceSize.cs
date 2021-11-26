using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public struct DeviceSize
  {
    [JsonProperty("rows")]
    public short Rows { get; private set; }

    [JsonProperty("columns")]
    public short Columns { get; private set; }
  }
}
