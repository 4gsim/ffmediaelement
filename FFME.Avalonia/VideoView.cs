using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Rendering.Composition;

namespace Unosquare.FFME;

public class VideoView : CompositionCustomVisualHandler
{
    public WriteableBitmap? Source { get; set; }

    public bool IsRunning { get; set; }

    private Size _imageSize;

    private Size _containerSize;

    public override void OnRender(ImmediateDrawingContext drawingContext)
    {
        if (Source != null)
        {
            var drawSize = Stretch.Uniform.CalculateSize(_containerSize, _imageSize);
            var y = (GetRenderBounds().Height - drawSize.Height) / 2;
            drawingContext.DrawBitmap(Source, new Rect(0, y, drawSize.Width, drawSize.Height));
        }
    }

    public override void OnMessage(object message)
    {
        if (message is StartCommand startCommand)
        {
            _imageSize = startCommand.Size;
            RegisterForNextAnimationFrameUpdate();
        }
        if (message is ResizeCommand resizeCommand)
        {
            _containerSize = resizeCommand.Size;
        }
    }

    public override void OnAnimationFrameUpdate()
    {
        if (IsRunning)
        {
            Invalidate();
            RegisterForNextAnimationFrameUpdate();
        }
    }
}