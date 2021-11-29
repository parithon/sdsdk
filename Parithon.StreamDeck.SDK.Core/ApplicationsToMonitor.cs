using System.Collections.Generic;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK
{
  public class ApplicationsToMonitor
  {
    [JsonProperty("mac")]
    public ICollection<string> mac { get; set; }

    [JsonProperty("windows")]
    public ICollection<string> windows { get; set; }
  }
}
