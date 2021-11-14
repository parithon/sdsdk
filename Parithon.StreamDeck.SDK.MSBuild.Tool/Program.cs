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

Console.WriteLine(manifestJSON);

string? targetPath = Path.GetDirectoryName(args[0]);
if (targetPath == null) return;

File.WriteAllText(Path.Combine(targetPath, "manifest.json"), manifestJSON);

