using System;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Messages
{
  internal class SetImageMessage : IMessage
  {
    public SetImageMessage(string context, DeviceTarget target, short state = 0, string path = null)
    {
      this.Context = context;
      string base64 = string.Empty;
      if (path != null && System.IO.File.Exists(path))
      {
        var bytes = System.IO.File.ReadAllBytes(path);
        base64 = Convert.ToBase64String(bytes);
      }
      this.Payload = new SetImagePayload(target, state, base64);
    }
    public string Event => "setImage";
    public string Context { get; }
    public SetImagePayload Payload { get; }
  }
}
