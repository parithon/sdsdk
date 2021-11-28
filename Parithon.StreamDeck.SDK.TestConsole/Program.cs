using System;
using System.Linq;
using Parithon.StreamDeck.SDK;
using TestConsole.Actions;

namespace TestConsole
{
  public class FakeObject
  {
    public FakeObject()
    {
    }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      FakeObject me = new FakeObject();
      var client = new StreamDeckClientBuilder(args, waitForDebugger: false)
        .AddSingleton(me)
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
