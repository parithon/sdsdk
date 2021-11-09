using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Parithon.StreamDeck.SDK.Targets
{
  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Assembly buildAssembly = Assembly.LoadFrom(args[0]);
        var manifest = new Manifest(buildAssembly);
        Console.WriteLine($"ICON => {manifest.Icon}");
        Console.WriteLine($"OS => {manifest.OS.Count()}");
        var manifestJSON = JsonSerializer.Serialize(manifest);
        Console.WriteLine(manifestJSON);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }

  internal class Manifest
  {
    public Manifest(Assembly assembly)
    {
      this.Icon = assembly.GetCustomAttribute<AssemblyStreamDeckIconAttribute>()?.Icon;
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

    public string Icon { get; private set; }
    public IEnumerable<dynamic> OS { get; private set; } = new List<dynamic>();
  }
}
