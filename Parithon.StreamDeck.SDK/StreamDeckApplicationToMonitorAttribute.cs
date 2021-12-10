using System;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  public sealed class StreamDeckApplicationToMonitorAttribute : Attribute
  {
    public Platform OS { get; set; }
    public string Name { get; set; }
  }
}
