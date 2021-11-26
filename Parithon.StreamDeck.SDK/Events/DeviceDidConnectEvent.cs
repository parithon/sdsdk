using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  public class DeviceDidConnectEvent : StreamDeckEvent
  {
    [JsonProperty("event")]
    public override string Event => DeviceDidConnect;

    [JsonProperty("device")]
    public string Device { get; private set; }

    [JsonProperty("deviceInfo")]
    public DeviceInfo DeviceInfo { get; private set; }
  }
}
