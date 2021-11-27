using System;
using Parithon.StreamDeck.SDK;

namespace TestConsole
{
  internal class Program
  {
    static void Main(string[] args)
    {
      dynamic me = new { };
      var client = new StreamDeckClientBuilder(args, waitForDebugger: false)
        .Build();

      client.Connected += (s, e) => Console.WriteLine("Connected to StreamDeck");
      client.Disconnected += (s, e) => Console.WriteLine("Disconnected from StreamDeck");

      client.DeviceDidConnect += (s, e) =>
      {
        Console.WriteLine($"Device '{e.DeviceInfo.Name}' connected.");
        client.GetGlobalSettingsAsync(e.Device);
      };

      client.DidReceiveGlobalSettings += (s, e) =>
      {
        Console.WriteLine($"Received Global Settings: '{e.Payload.Settings}'");
      };

      client.Execute();
    }
  }
}
