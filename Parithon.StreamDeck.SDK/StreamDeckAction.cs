using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parithon.StreamDeck.SDK.Messages;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK
{
  public abstract class StreamDeckAction : IDisposable
  {
    private bool disposedValue;

    public abstract string Icon { get; }

    public abstract string Name { get; }

    public virtual string Title { get; private set; }

    public virtual string PropertyInspectorPath { get; }

    public virtual ICollection<StreamDeckActionState> States { get; } = new List<StreamDeckActionState>();

    public virtual bool? SupportedInMultiActions { get; }

    public virtual string Tooltip { get; }

    public string UUID => GetType().FullName.ToLowerInvariant();

    public virtual bool? VisibleInActionsList { get; }

    public abstract void OnKeyDown(KeyPayload payload);

    public abstract void OnKeyUp(KeyPayload payload);

    public string Context { get; set; }

    public virtual void Initialize(AppearPayload payload) { }

    public virtual Func<IMessage, Task> SendAsync { get; set; }

    public virtual void SendToPlugin(dynamic payload) { }

    public virtual void PropertyInspector(bool isVisible) { }

    public virtual void SetTitle(string title)
    {
      this.Title = title;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        disposedValue = true;
      }
    }

    public void Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
