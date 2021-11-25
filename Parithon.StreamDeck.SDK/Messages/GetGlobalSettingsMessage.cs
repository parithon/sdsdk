using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK.Messages
{
  internal class GetGlobalSettingsMessage : IMessage
  {
    public GetGlobalSettingsMessage(string context)
    {
      this.Context = context;
    }

    [JsonProperty("event")]
    public string Event => StreamDeckEvent.GetGlobalSettings;

    [JsonProperty("context")]
    public string Context { get; }
  }
}
