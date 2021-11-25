using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public class ApplicationPayload
  {
    [JsonProperty("application")]
    public string Application { get; private set; }
  }
}
