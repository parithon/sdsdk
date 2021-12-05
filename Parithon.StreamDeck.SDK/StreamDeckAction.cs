using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parithon.StreamDeck.SDK.Messages;
using Parithon.StreamDeck.SDK.Models;

namespace Parithon.StreamDeck.SDK
{
  public abstract class StreamDeckAction
  {
    public abstract string Icon { get; }

    public abstract string Name { get; }

    public virtual string Title { get; private set; }

    public virtual string PropertyInspectorPath { get; }

    public virtual ICollection<StreamDeckActionState> States { get; } = new List<StreamDeckActionState>();

    public virtual bool? SupportedInMultiActions { get; }

    public virtual string Tooltip { get; }

    public string UUID => GetType().FullName;

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
  }
}
