using Parithon.StreamDeck.SDK.Abstracts;

namespace Parithon.StreamDeck.SDK.Events
{
  public abstract class StreamDeckEvent
  {
    #region Receive Events
    internal const string DidReceiveSettings = "didReceiveSettings";
    internal const string DidReceiveGlobalSettings = "didReceiveGlobalSettings";
    internal const string KeyDown = "keyDown";
    internal const string KeyUp = "keyUp";
    internal const string WillAppear = "willAppear";
    internal const string WillDisappear = "willDisappear";
    internal const string TitleParametersDidChange = "titleParametersDidChange";
    internal const string DeviceDidConnect = "deviceDidConnect";
    internal const string DeviceDidDisconnect = "deviceDidDisconnect";
    internal const string ApplicationDidLaunch = "applicationDidLaunch";
    internal const string ApplicationDidTerminate = "applicationDidTerminate";
    internal const string SendToPlugin = "sendToPlugin";
    internal const string PropertyInspectorDidAppear = "propertyInspectorDidAppear";
    internal const string PropertyInspectorDidDisappear = "propertyInspectorDidDisappear";
    internal const string SystemDidWakeUp = "systemDidWakUp";
    #endregion

    #region Send Events
    internal const string GetSettings = "getSettings";
    internal const string GetGlobalSettings = "getGlobalSettings";
    internal const string SetTitle = "setTitle";
    internal const string SetSettings = "setSettings";
    internal const string SetState = "setState";
    internal const string ShowOK = "showOk";
    #endregion

    public abstract string Event { get; }
  }
}
