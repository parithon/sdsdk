using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public class SetTitlePayload
  {
    [JsonProperty("title")]
    public string Title { get; internal set; }

    [JsonProperty("target")]
    public DeviceTarget? Target { get; internal set; }

    [JsonProperty("state")]
    public short? State { get; internal set; }
  }
}
