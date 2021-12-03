using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

Assembly buildAssembly = Assembly.LoadFrom(args[0]);
Manifest manifest = new Manifest(buildAssembly);

string manifestJSON = JsonConvert.SerializeObject(manifest, new JsonSerializerSettings()
{
  NullValueHandling = NullValueHandling.Ignore,
  Formatting = Formatting.Indented
});

var targetPath = Path.GetDirectoryName(args[0]);
if (targetPath == null) return;

var filepath = Path.Combine(targetPath, "manifest.json");

Console.WriteLine($"Generated manifest -> {filepath}");

File.WriteAllText(filepath, manifestJSON);
