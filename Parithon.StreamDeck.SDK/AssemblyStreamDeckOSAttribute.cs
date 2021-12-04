using System;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
  public sealed class AssemblyStreamDeckOSAttribute : Attribute
  {
    public string Platform { get; set; }
    public string MinimumVersion { get; set; }
  }
}
