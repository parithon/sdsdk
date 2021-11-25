using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Events
{
  public class DeviceDidDisconnectEvent : StreamDeckEvent
  {
    [JsonProperty("event")]
    public override string Event => DeviceDidDisconnect;

    [JsonProperty("device")]
    public string Device { get; private set; }
  }
}
