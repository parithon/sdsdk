using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  public class AssemblyStreamDeckApplicationToMonitorAttribute : Attribute
  {
    public string Name { get; set; }
  }
}
