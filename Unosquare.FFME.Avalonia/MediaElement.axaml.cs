using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using Unosquare.FFME.Engine;
using Unosquare.FFME.Platform;

namespace Unosquare.FFME;

public partial class MediaElement : UserControl, IDisposable
{
    private bool _isDisposed;

    internal IGuiContext GuiContext { get; private set; }

    public VideoView VideoView { get; private set; }

    private CompositionCustomVisual? _visual;

    public MediaElement()
    {
        InitializeComponent();
        GuiContext = new GuiContext();
        MediaCore = new MediaEngine(this, new MediaConnector(this));
        VideoView = new VideoView();
        Viewport.AttachedToVisualTree += ViewportAttachedToVisualTree;
    }

    private void ViewportAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender == null)
        {
            return;
        }
        var viewPort = (Control)sender;
        var visual = ElementComposition.GetElementVisual(viewPort);
        if (visual == null)
        {
            return;
        }
        _visual = visual.Compositor.CreateCustomVisual(VideoView);
        ElementComposition.SetElementChildVisual(viewPort, _visual);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var size = base.ArrangeOverride(finalSize);
        if (_visual != null)
        {
            _visual.Size = new Vector(size.Width, size.Height);
            _visual.SendHandlerMessage(new ResizeCommand(size));
        }
        return size;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        try
        {
            Close();
        }
        finally
        {
            Dispose();
        }
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            MediaCore.Dispose();
            _isDisposed = true;
        }
    }
}