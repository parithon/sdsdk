using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public class TitleParameters
  {
    [JsonProperty("fontFamily")]
    public string FontFamily { get; set; }

    [JsonProperty("fontSize")]
    public short FontSize { get; set; }

    [JsonProperty("fontStyle")]
    public string FontStyle { get; set; }

    [JsonProperty("fontUnderline")]
    public bool FontUnderline { get; set; }

    [JsonProperty("showTitle")]
    public bool ShowTitle { get; set; }

    [JsonProperty("titleAlignment")]
    public string TitleAlignment { get; set; }

    [JsonProperty("titleColor")]
    public string TitleColor { get; set; }
  }
}
