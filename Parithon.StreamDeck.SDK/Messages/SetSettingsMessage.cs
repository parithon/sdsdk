using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK.Messages
{
  public class SetSettingsMessage : IMessage
  {
    public SetSettingsMessage(string context, dynamic settings)
    {
      this.Context = context;
      this.Payload = settings;
    }

    public string Event => StreamDeckEvent.SetSettings;

    [JsonProperty("context")]
    public string Context { get; }

    [JsonProperty("payload")]
    public dynamic Payload { get; }
  }
}
