using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
  public sealed class AssemblyStreamDeckIconAttribute : Attribute
  {
    public AssemblyStreamDeckIconAttribute()
    {
    }

    public AssemblyStreamDeckIconAttribute(string icon)
    {
      Icon = icon;
    }

    public string Icon { get; set; }
  }
}
