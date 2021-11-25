using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK
{
  public class StreamDeckStartupArguments
  {
    public int Port { get; set; }
    public string PluginUUID { get; set; }
    public string RegisterEvent { get; set; }
    public string Info { get; set; }
  }

  internal static class StreamDeckStartupArgumentsExtensions
  {
    public static void Deconstruct(this StreamDeckStartupArguments args, out int port, out string uuid, out string registerEvent, out string info)
    {
      port = args.Port;
      uuid = args.PluginUUID;
      registerEvent = args.RegisterEvent;
      info = args.Info;
    }
  }
}
