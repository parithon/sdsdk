using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK
{
  public class StreamDeckActionState
  {
    public string Image { get; set; }
    public string MultiActionImage { get; }
    public string Name { get; }
    public string Title { get; }
    public bool? ShowTitle { get; }
    public string TitleColor { get; }
    public Alignment? TitleAlignment { get; }
    public string FontFamily { get; }
    public FontStyle? FontStyle { get; }
    public short? FontSize { get; }
    public bool? FontUnderline { get; }
  }
}
