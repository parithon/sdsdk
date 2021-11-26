using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Parithon.StreamDeck.SDK.Models
{
  public interface IGlobalSettings
  {
    dynamic Settings { get; }
  }

  public class GlobalSettingsPayload : IGlobalSettings
  {
    [JsonProperty("settings")]
    public dynamic Settings { get; private set; }
  }
}
