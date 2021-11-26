using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

Assembly buildAssembly = Assembly.LoadFrom(args[0]);
Manifest manifest = new Manifest(buildAssembly);

string manifestJSON = JsonSerializer.Serialize(manifest, new JsonSerializerOptions()
{
  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
  WriteIndented = true
});

var targetPath = Path.GetDirectoryName(args[0]);
if (targetPath == null) return;

var filepath = Path.Combine(targetPath, "manifest.json");

Console.WriteLine($"Generated manifest -> {filepath}");

File.WriteAllText(filepath, manifestJSON);
