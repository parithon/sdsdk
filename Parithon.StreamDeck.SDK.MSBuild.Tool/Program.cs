using System.Reflection;
using Newtonsoft.Json;

Assembly buildAssembly = Assembly.LoadFrom(args[0]);
Manifest manifest = new Manifest(buildAssembly);

List<Exception> exceptions = new();

var iconPath = Path.Combine(Path.GetDirectoryName(buildAssembly.Location), $"{manifest.Icon}.png");
if (!File.Exists(iconPath))
{
  exceptions.Add(new FileNotFoundException("Could not find manifest icon.", iconPath));
}

foreach (var action in manifest.Actions)
{
  var actionIconPath = Path.Combine(Path.GetDirectoryName(buildAssembly.Location), $"{action.Icon}.png");
  if (!File.Exists(actionIconPath))
  {
    exceptions.Add(new FileNotFoundException($"Cound not find action icon for '{action.UUID}'.", actionIconPath));
  }

  for (int i = 0; i < action.States.Count; i++)
  {
    var states = action.States as IEnumerable<dynamic>;
    var state = states.Skip(i).FirstOrDefault();
    if (state == null) break;

    var stateIconPath = Path.Combine(Path.GetDirectoryName(buildAssembly.Location), $"{state.Image}.png");
    if (!File.Exists(stateIconPath))
    {
      exceptions.Add(new FileNotFoundException($"Cound not find action '{action.UUID}' state {i} icon.", stateIconPath));
    }
  }
}

if (exceptions.Any())
{
  if (exceptions.Count == 1)
  {
    throw new Exception("An error occurred while generating the manifest.", exceptions.First());
  }
  else
  {
    throw new AggregateException("Multiple errors were encountered while generating the manifest.", exceptions);
  }
}

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
