using System.Reflection;
using Parithon.StreamDeck.SDK;

internal class Manifest
{
  public Manifest(Assembly assembly)
  {
    this.Icon = assembly.GetCustomAttribute<AssemblyStreamDeckIconAttribute>()?.Icon ?? "Unknown";
    this.OS = GetOS(assembly.GetCustomAttributes<AssemblyStreamDeckOSAttribute>());
  }

  private IEnumerable<dynamic> GetOS(IEnumerable<AssemblyStreamDeckOSAttribute> osattribs)
  {
    if (!osattribs.Any())
    {
      yield return new { Platform = "windows", MinimumVersion = "10" };
      yield break;
    }
    foreach (var osattrib in osattribs)
    {
      yield return new { osattrib.Platform, osattrib.MinimumVersion };
    }
  }

  public ICollection<dynamic> Actions { get; private set; } = new List<dynamic>();
  public string Icon { get; private set; }
  public IEnumerable<dynamic> OS { get; private set; } = new List<dynamic>();
}
