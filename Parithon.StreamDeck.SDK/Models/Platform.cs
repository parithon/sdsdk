using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parithon.StreamDeck.SDK.Models
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum Platform
  {
    mac,
    windows
  }
}
