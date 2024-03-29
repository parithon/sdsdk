﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK
{
  public class StreamDeckActionState
  {
    public string Image { get; set; }
    public string MultiActionImage { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public bool? ShowTitle { get; set; }
    public string TitleColor { get; set; }
    public Alignment? TitleAlignment { get; set; }
    public string FontFamily { get; set; }
    public string FontStyle { get; set; }
    public short? FontSize { get; set; }
    public bool? FontUnderline { get; set; }
  }
}
