using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Parithon.StreamDeck.SDK
{
  public class StreamDeckClientBuilder : IServiceCollection
  {
    private readonly IConfiguration configuration;
    private readonly Dictionary<string, Type> registeredActions;
    internal readonly IServiceCollection Services;

    public int Count => this.Services.Count;

    public bool IsReadOnly => this.Services.IsReadOnly;

    public ServiceDescriptor this[int index] { get => this.Services[index]; set => this.Services[index] = value; }

    public StreamDeckClientBuilder(string[] args, bool? waitForDebugger = false)
    {
      if (waitForDebugger.HasValue && waitForDebugger == true)
      {
        var wait = true;
        System.Threading.Tasks.Task.Run(() => {
          System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
          wait = false;
        });
        while (!System.Diagnostics.Debugger.IsAttached && wait)
        {
          System.Threading.Thread.Sleep(100);
        }
      }
      Dictionary<string, string> switchMappings = new()
      {
        { "-port", "port" },
        { "-pluginUUID", "pluginUUID" },
        { "-registerEvent", "registerEvent" },
        { "-info", "info" }
      };

      this.configuration = new ConfigurationBuilder()
        .AddCommandLine(args, switchMappings)
        .Build();

      this.registeredActions = new Dictionary<string, Type>();

      CancellationTokenSource cancellationTokenSource = new();
      Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();

      Services = new ServiceCollection()
        .Configure<StreamDeckStartupArguments>(configuration)
        .AddSingleton(cancellationTokenSource);

      var types = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(StreamDeckAction)));
      foreach (var type in types)
      {
        Services.AddTransient(type);
        registeredActions.Add(type.FullName.ToLower(), type);
      }
    }

    public StreamDeckClient Build()
    {
      return Services
        .AddSingleton(registeredActions)
        .AddSingleton<StreamDeckClient>()
        .BuildServiceProvider()
        .GetRequiredService<StreamDeckClient>();
    }

    public int IndexOf(ServiceDescriptor item) => this.Services.IndexOf(item);

    public void Insert(int index, ServiceDescriptor item) => this.Services.Insert(index, item);

    public void RemoveAt(int index) => this.Services.RemoveAt(index);

    public void Add(ServiceDescriptor item) => this.Services.Add(item);

    public void Clear() => this.Services.Clear();

    public bool Contains(ServiceDescriptor item) => this.Services.Contains(item);

    public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => this.Services.CopyTo(array, arrayIndex);

    public bool Remove(ServiceDescriptor item) => this.Services.Remove(item);

    public IEnumerator<ServiceDescriptor> GetEnumerator() => this.Services.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.Services.GetEnumerator();
  }
}
