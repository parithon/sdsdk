global using Parithon.StreamDeck.SDK;
global using Parithon.StreamDeck.SDK.Events;
global using Parithon.StreamDeck.SDK.Messages;
global using Parithon.StreamDeck.SDK.Models;

[assembly: StreamDeckManifest(Icon = "Images/blank")]
[assembly: StreamDeckOS(Platform = Platform.windows, MinimumVersion = "10")]
[assembly: StreamDeckOS(Platform = Platform.mac, MinimumVersion = "10.11")]

var client = new StreamDeckClientBuilder(args, waitForDebugger: false)
  .Build();

/// <summary>
/// An event that is fired whenever a StreamDeck client is connected.
/// </summary>
client.DeviceDidConnect += (s, e) =>
{
  Console.WriteLine($"Device '{e.DeviceInfo.Name}' connected.");
  client.GetGlobalSettingsAsync(e.Device);
};

client.Execute();
