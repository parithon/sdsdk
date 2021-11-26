using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Parithon.StreamDeck.SDK.Models
{
  public class TitleParametersPayload
  {
    [JsonProperty("coordinates")]
    public Coordinates Coordinates { get; private set; }

    [JsonProperty("settings")]
    public dynamic Settings { get; private set; }

    [JsonProperty("state")]
    public short State { get; private set; }

    [JsonProperty("title")]
    public string Title { get; private set; }

    [JsonProperty("titleParameters")]
    public TitleParameters TitleParameters { get; private set; }
  }
}
