using System;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
  public sealed class StreamDeckOSAttribute : Attribute
  {
    public Platform Platform { get; set; }
    public string MinimumVersion { get; set; }
  }
}
