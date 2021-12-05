using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parithon.StreamDeck.SDK.Messages
{
  internal class ShowAlertMessage : IMessage
  {
    public ShowAlertMessage(string context)
    {
      this.Context = context;
    }

    [JsonProperty("event")]
    public string Event => "showAlert";

    [JsonProperty("context")]
    public string Context { get; private set; }
  }
}
