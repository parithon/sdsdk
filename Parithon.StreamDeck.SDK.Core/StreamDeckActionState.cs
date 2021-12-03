using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
    public FontStyle? FontStyle { get; set; }
    public short? FontSize { get; set; }
    public bool? FontUnderline { get; set; }
  }
}
