using System;
using System.Collections.Generic;
using System.Text;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK.Messages
{
  internal class GetSettingsMessage : IMessage
  {
    public GetSettingsMessage(string context)
    {
      this.Context = context;
    }

    public string Event => StreamDeckEvent.GetSettings;

    public string Context { get; }
  }
}
