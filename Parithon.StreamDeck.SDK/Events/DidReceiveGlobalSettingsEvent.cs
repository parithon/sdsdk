using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  /// <summary>
  /// Received after using the <seealso cref="StreamDeckClient.GetGlobalSettingsAsync(string)"/> method
  /// to retrieve the global persistent data stored for the plugin
  /// </summary>
  public class DidReceiveGlobalSettingsEvent : StreamDeckEvent
  {
    /// <summary>
    /// The event identifier
    /// </summary>
    [JsonProperty("event")]
    public override string Event => DidReceiveGlobalSettings;

    /// <summary>
    /// The global settings for the plugin
    /// </summary
    [JsonProperty("payload")]
    public GlobalSettingsPayload Payload { get; private set; }
  }
}
