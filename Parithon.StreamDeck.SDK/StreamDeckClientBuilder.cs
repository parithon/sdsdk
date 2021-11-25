using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Parithon.StreamDeck.SDK
{
  public class StreamDeckClientBuilder
  {
    private readonly IConfiguration configuration;
    private readonly Dictionary<string, Type> registeredActions;
    internal readonly IServiceCollection Services;

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
  }
}
