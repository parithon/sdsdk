using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Events;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Messages
{
  public class SetTitleMessage : IMessage
  {
    public SetTitleMessage(string context, string title)
    {
      this.Context = context;
      this.Payload = new();
      this.Payload.Title = title;
    }

    public SetTitleMessage(string context, string title, dynamic target, short? state) : this(context, title)
    {
      this.Payload.Target = target;
      this.Payload.State = state;
    }

    public string Event => StreamDeckEvent.SetTitle;

    [JsonProperty("context")]
    public string Context { get; }

    [JsonProperty("payload")]
    public SetTitlePayload Payload { get; }
  }
}
