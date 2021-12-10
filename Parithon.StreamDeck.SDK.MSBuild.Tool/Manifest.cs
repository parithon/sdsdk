using System.Reflection;
using Parithon.StreamDeck.SDK;
using Parithon.StreamDeck.SDK.Models;

internal static class ReflectionExtension
{
  public static dynamic GetInstance(this Type type)
  {
    var ctor = type.GetConstructors().First();
    var parameters = ctor.GetParameters().Select(p => p.ParameterType.GetInstance()).ToArray();
    return ctor.Invoke(parameters);
  }
}

internal class Manifest
{
  public Manifest(Assembly assembly)
  {
    var streamDeckAttribute = assembly.GetCustomAttribute<StreamDeckManifestAttribute>();
    var types = assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(typeof(StreamDeckAction)));
    this.Actions = new List<dynamic>();
    foreach (var type in types)
    {
      var action = type.GetInstance();
      this.Actions.Add(action);
    }
    var os = GetOS(assembly.GetCustomAttributes<StreamDeckOSAttribute>());
    var versionStr = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "0.0.0";
    var versionStrRegex = @"(?<version>(?:\d+\.?)*)[-+]";
    if (System.Text.RegularExpressions.Regex.IsMatch(versionStr, versionStrRegex))
    {
      versionStr = System.Text.RegularExpressions.Regex.Match(versionStr, versionStrRegex).Groups["version"].Value;
    }
    var version = new Version(versionStr);
    this.Author = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
    this.Category = streamDeckAttribute?.Category;
    this.CategoryIcon = streamDeckAttribute?.CategoryIcon ?? streamDeckAttribute?.Icon;
    this.CodePath = os.SingleOrDefault(o => o.Platform == Platform.Mac) != null ? $"{assembly.GetName().Name}" : $"{assembly.GetName().Name}.exe";
    this.CodePathMac = streamDeckAttribute?.CodePathMac ?? (os.Count() > 1 && os.Any(o => o.Platform == Platform.Mac) ? $"{assembly.GetName().Name}" : null);
    this.CodePathWin = streamDeckAttribute?.CodePathWin;
    this.Description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? throw new ArgumentNullException("AssemblyDescription", "An assembly description is required for the manifest.");
    this.Icon = streamDeckAttribute?.Icon;
    this.Name = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
    this.Profiles = null;
    this.PropertyInspectorPath = streamDeckAttribute?.PropertyInspectorPath;
    this.DefaultWindowSize = null;
    this.URL = streamDeckAttribute?.URL;
    this.Version = version;
    this.SDKVersion = 2;
    this.OS = os;
    this.Software = new
    {
      MinimumVersion = "4.1"
    };
    var applicationsToMonitor = assembly.GetCustomAttributes<StreamDeckApplicationToMonitorAttribute>();
    if (applicationsToMonitor.Any())
    {
      this.ApplicationsToMonitor = new ApplicationsToMonitor();
      var macApps = applicationsToMonitor.Where(a => a.OS == Platform.Mac);
      var winApps = applicationsToMonitor.Where(a => a.OS == Platform.Windows);
      if (macApps.Any())
      {
        this.ApplicationsToMonitor.mac = new List<string>(macApps.Select(a => a.Name));
      }
      if (winApps.Any())
      {
        this.ApplicationsToMonitor.windows = new List<string>(winApps.Select(a => a.Name));
      }
    }
  }

  private static IEnumerable<dynamic> GetOS(IEnumerable<StreamDeckOSAttribute> osattribs)
  {
    List<dynamic> platforms = new();
    if (!osattribs.Any())
    {
      platforms.AddRange(new [] {
        new { Platform = Platform.Windows, MinimumVersion = "10" },
        new { Platform = Platform.Mac, MinimumVersion = "10.11" }
      });
    }
    foreach (var osattrib in osattribs)
    {
      platforms.Add(new { osattrib.Platform, osattrib.MinimumVersion });
    }
    return platforms;
  }

  public ICollection<dynamic> Actions { get; private set; }
  public string Author { get; private set; }
  public string Category { get; private set; }
  public string CategoryIcon { get; private set; }
  public string CodePath { get; private set; }
  public string CodePathMac { get; private set; }
  public string CodePathWin { get; private set; }
  public string Description { get; private set; }
  public string Icon { get; private set; }
  public string Name { get; private set; }
  public ICollection<dynamic> Profiles { get; private set; }
  public string PropertyInspectorPath { get; private set; }
  public dynamic DefaultWindowSize { get; private set; }
  public string URL { get; private set; }
  public Version Version { get; private set; }
  public short SDKVersion { get; private set; }
  public IEnumerable<dynamic> OS { get; private set; }
  public dynamic Software { get; private set; }
  public ApplicationsToMonitor ApplicationsToMonitor { get; private set; }
}
