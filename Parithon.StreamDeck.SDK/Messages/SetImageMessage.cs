using System;
using System.IO;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Messages
{
  public class SetImageMessage : IMessage
  {
    public SetImageMessage(string context, DeviceTarget target, short state = 0, string path = null)
    {
      this.Context = context;
      string base64 = string.Empty;
      if (path != null && File.Exists(path))
      {
        var bytes = File.ReadAllBytes(path);
        base64 = $"data:image/{Path.GetExtension(path).Substring(1)};base64,{Convert.ToBase64String(bytes)}";
      }
      this.Payload = new SetImagePayload(target, state, base64);
    }
    public string Event => "setImage";

    [JsonProperty("context")]
    public string Context { get; }

    [JsonProperty("payload")]
    public SetImagePayload Payload { get; }
  }
}
