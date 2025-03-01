using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Rendering.Composition;

namespace Unosquare.FFME;

public class VideoView : CompositionCustomVisualHandler
{
    public WriteableBitmap? Source { get; set; }

    public Size ImageSize { get; set; }

    public bool IsDirty { get; set; }

    private Size _containerSize;

    private bool _isAnimating;

    public override void OnRender(ImmediateDrawingContext drawingContext)
    {
        if (Source != null)
        {
            var drawSize = Stretch.Uniform.CalculateSize(_containerSize, ImageSize);
            var y = (GetRenderBounds().Height - drawSize.Height) / 2;
            drawingContext.DrawBitmap(Source, new Rect(0, y, drawSize.Width, drawSize.Height));
        }
    }

    public override void OnMessage(object message)
    {
        if (message is ResizeCommand resizeCommand)
        {
            _containerSize = resizeCommand.Size;
            if (!_isAnimating)
            {
                RegisterForNextAnimationFrameUpdate();
                _isAnimating = true;
            }
        }
    }

    public override void OnAnimationFrameUpdate()
    {
        if (IsDirty)
        {
            Invalidate();
            IsDirty = false;
        }
        RegisterForNextAnimationFrameUpdate();
    }
}