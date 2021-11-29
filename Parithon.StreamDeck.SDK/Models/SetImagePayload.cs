using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Models
{
  public class SetImagePayload
  {
    public SetImagePayload(DeviceTarget target, short state, string image = null)
    {
      this.Target = target;
      this.State = state;
      if (!string.IsNullOrEmpty(image))
      {
        this.Image = image;
      }
    }

    [JsonProperty("image")]
    public string Image { get; }

    [JsonProperty("target")]
    public DeviceTarget Target { get; }

    [JsonProperty("state")]
    public short State { get; }
  }
}
