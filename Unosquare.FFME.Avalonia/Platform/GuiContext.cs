using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace Unosquare.FFME.Platform;

public class GuiContext : IGuiContext
{
    public GuiContextType Type => throw new NotImplementedException();

    public void EnqueueInvoke(Action callback) => InvokeAsync(callback);

    public ConfiguredTaskAwaitable InvokeAsync(Action callback) => InvokeAsyncInternal(DispatcherPriority.Normal, callback).ConfigureAwait(true);

    private async Task InvokeAsyncInternal(DispatcherPriority priority, Action callback)
    {
        await Dispatcher.UIThread.InvokeAsync(() => { callback.DynamicInvoke(); }, priority);
    }
}