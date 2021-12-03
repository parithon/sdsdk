using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parithon.StreamDeck.SDK
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum Alignment
  {
    Top,
    Middle,
    Bottom
  }
}
