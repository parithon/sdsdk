using System;
using Parithon.StreamDeck.SDK;
using Parithon.StreamDeck.SDK.Models;

[assembly: StreamDeckManifest(Icon = "Images/virt_cam_on", Category = "Testing")]
[assembly: StreamDeckOS(Platform = Platform.windows, MinimumVersion = "10")]
[assembly: StreamDeckOS(Platform = Platform.mac, MinimumVersion = "10.11")]
[assembly: StreamDeckApplicationToMonitor(Name = "obs", OS = Platform.mac)]
[assembly: StreamDeckApplicationToMonitor(Name = "obs.exe", OS = Platform.windows)]
[assembly: StreamDeckApplicationToMonitor(Name = "obs64.exe", OS = Platform.windows)]

namespace Dev.Parithon.StreamDeck.TestConsole
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
#if DEBUG
      System.Diagnostics.Debugger.Launch();
#endif

      var me = new FakeObject();
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
