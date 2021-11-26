using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK
{
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
  public sealed class AssemblyStreamDeckAttribute : Attribute
  {
    public string Category { get; set; }
    public string CategoryIcon { get; set; }
    public string Icon { get; set; }
    public string CodePathMac { get; set; }
    public string CodePathWin { get; set; }
    public string PropertyInspectorPath { get; set; }
    public string URL { get; set; }
  }
}
