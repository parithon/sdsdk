using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public class DeviceInfo
  {
    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("type")]
    public DeviceType Type { get; private set; }

    [JsonProperty("size")]
    public DeviceSize Size { get; private set; }
  }
}
