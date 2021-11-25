using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK.Messages
{
  public class ShowOKMessage : IMessage
  {
    public ShowOKMessage(string context)
    {
      this.Context = context;
    }

    [JsonProperty("event")]
    public string Event => StreamDeckEvent.ShowOK;

    [JsonProperty("context")]
    public string Context { get; }
  }
}
