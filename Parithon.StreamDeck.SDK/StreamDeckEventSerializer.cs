using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parithon.StreamDeck.SDK.Events;

namespace Parithon.StreamDeck.SDK
{
  internal class StreamDeckEventSerializer : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return (objectType == typeof(StreamDeckEvent));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      JObject jo = JObject.Load(reader);
      string evt = jo["event"].Value<string>();
      switch (evt)
      {
        case StreamDeckEvent.ApplicationDidLaunch:
          return jo.ToObject<ApplicationDidLaunchEvent>();
        case StreamDeckEvent.ApplicationDidTerminate:
          return jo.ToObject<ApplicationDidTerminateEvent>();
        case StreamDeckEvent.DeviceDidConnect:
          return jo.ToObject<DeviceDidConnectEvent>();
        case StreamDeckEvent.DeviceDidDisconnect:
          return jo.ToObject<DeviceDidDisconnectEvent>();
        case StreamDeckEvent.DidReceiveGlobalSettings:
          return jo.ToObject<DidReceiveGlobalSettingsEvent>();
        case StreamDeckEvent.DidReceiveSettings:
          return jo.ToObject<DidReceiveSettingsEvent>();
        case StreamDeckEvent.KeyDown:
          return jo.ToObject<KeyDownEvent>();
        case StreamDeckEvent.KeyUp:
          return jo.ToObject<KeyUpEvent>();
        case StreamDeckEvent.TitleParametersDidChange:
          return jo.ToObject<TitleParametersDidChangeEvent>();
        case StreamDeckEvent.WillAppear:
          return jo.ToObject<WillAppearEvent>();
        case StreamDeckEvent.WillDisappear:
          return jo.ToObject<WillDisappearEvent>();
      }

      return null;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override bool CanWrite => false;
  }
}
