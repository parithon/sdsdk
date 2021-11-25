using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK.Events
{
  /// <summary>
  /// Received after using the <seealso cref="StreamDeckClient.GetSettingsAsync(string)"/> method 
  /// to retrieve the persistent data stored for the action.
  /// </summary>
  public class DidReceiveSettingsEvent : StreamDeckEvent
  {
    /// <summary>
    /// The action unique identifier
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; private set; }

    /// <summary>
    /// The event identifier
    /// </summary>
    [JsonProperty("event")]
    public override string Event => DidReceiveSettings;

    /// <summary>
    /// A unique value identifying the instance's action
    /// </summary>
    [JsonProperty("context")]
    public string Context { get; private set; }

    /// <summary>
    /// A unique value identifying the device
    /// </summary>
    [JsonProperty("device")]
    public string Device { get; private set; }

    /// <summary>
    /// The specific information about this action
    /// </summary>
    [JsonProperty("payload")]
    public SettingsPayload Payload { get; private set; }
  }
}
