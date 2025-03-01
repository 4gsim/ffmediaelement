using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Unosquare.FFME.Container;
using Unosquare.FFME.Engine;

namespace Unosquare.FFME.Platform;

internal class VideoRenderer(MediaEngine mediaCore) : IMediaRenderer
{
    private int _currentStride;

    public object Test { get; } = new object{};

    public MediaEngine MediaCore { get; } = mediaCore;

    public MediaElement MediaElement => (MediaElement)MediaCore.Parent;

    public void OnClose()
    {

    }

    public void OnPause()
    {

    }

    public void OnPlay()
    {

    }

    public void OnSeek()
    {

    }

    public void OnStarting()
    {

    }

    public void OnStop()
    {

    }

    private WriteableBitmap PrepareVideoFrameBuffer(VideoBlock block)
    {
        var targetBitmap = MediaElement.VideoView.Source;
        var newPixelSize = new PixelSize(block.PixelWidth, block.PixelHeight);
        var needsCreation = targetBitmap == null;
        var needsModification = targetBitmap != null && 
            (targetBitmap.PixelSize != newPixelSize ||
            _currentStride != block.PictureBufferStride);

        if (!needsCreation && !needsModification)
        {
            return targetBitmap!;
        }

        targetBitmap = new WriteableBitmap(new PixelSize(block.PixelWidth, block.PixelHeight),
                                            new Vector(96.0, 96.0),
                                            PixelFormat.Bgra8888,
                                            AlphaFormat.Unpremul);
        _currentStride = block.PictureBufferStride;
        MediaElement.VideoView.Source = targetBitmap;

        MediaElement.VideoView.ImageSize = new Size(block.PixelWidth, block.PixelHeight);

        return targetBitmap;
    }

    private unsafe void WriteVideoFrameBuffer(VideoBlock block, WriteableBitmap targetBitmap)
    {
        var minStride = (PixelFormat.Bgra8888.BitsPerPixel * block.PixelWidth + 7) / 8;
        if (minStride > block.PictureBufferStride)
        {
            return;
        }

        if  (!block.TryAcquireReaderLock(out var readLock))
        {
            readLock?.Dispose();
            return;
        }

        using (readLock)
        {
            using (var locked = targetBitmap.Lock())
            {
                for (var y = 0; y < block.PixelHeight; y++)
                {
                    Unsafe.CopyBlock((locked.Address + locked.RowBytes * y).ToPointer(),
                        (block.Buffer + y * block.PictureBufferStride).ToPointer(), (uint)minStride);
                }
            }
        }

        MediaElement.VideoView.IsDirty = true;
    }

    public void Render(MediaBlock mediaBlock, TimeSpan clockPosition)
    {
        if (mediaBlock is not VideoBlock)
        {
            return;
        }

        var block = (VideoBlock)mediaBlock;

        var targetBitmap = PrepareVideoFrameBuffer(block);

        WriteVideoFrameBuffer(block, targetBitmap);
    }

    public void Update(TimeSpan clockPosition)
    {
        
    }
}