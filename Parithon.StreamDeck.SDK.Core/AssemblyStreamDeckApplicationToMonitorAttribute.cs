using System;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  public sealed class AssemblyStreamDeckApplicationToMonitorAttribute : Attribute
  {
    public string OS { get; set; }
    public string Name { get; set; }
  }
}
