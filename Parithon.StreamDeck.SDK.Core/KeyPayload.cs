using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK
{
  public class KeyPayload
  {
    [JsonProperty("settings")]
    public dynamic Settings { get; private set; }

    [JsonProperty("coordinates")]
    public Coordinates Coordinates { get; private set; }

    [JsonProperty("state")]
    public short State { get; private set; }

    [JsonProperty("userDesiredState")]
    public short UserDesiredState { get; private set; }

    [JsonProperty("isInMultiAction")]
    public bool IsInMultiAction { get; private set; }
  }
}
