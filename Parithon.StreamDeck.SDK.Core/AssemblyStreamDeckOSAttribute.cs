using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
  public sealed class AssemblyStreamDeckOSAttribute : Attribute
  {
    public AssemblyStreamDeckOSAttribute()
    {
    }

    public AssemblyStreamDeckOSAttribute(string platform, string minimumVersion)
    {
      Platform = platform;
      MinimumVersion = minimumVersion;
    }

    public string Platform { get; set; }
    public string MinimumVersion { get; set; }
  }
}
