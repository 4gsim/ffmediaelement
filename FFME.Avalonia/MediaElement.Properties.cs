using System;
using System.ComponentModel;
using Unosquare.FFME.Common;

namespace Unosquare.FFME;

public partial class MediaElement
{
    [Category(nameof(MediaElement))]
    [Description("Specifies how the media should behave when it has ended. The default behavior is to Pause the media.")]
    public MediaPlaybackState LoopingBehavior
    {
        get => MediaPlaybackState.Pause;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("The audio balance for left and right audio channels. Valid ranges are -1.0 to 1.0")]
    public double Balance
    {
        get => Constants.DefaultBalance;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("The playback volume. Ranges from 0.0 to 1.0")]
    public double Volume
    {
        get => Constants.DefaultVolume;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("Gets or sets whether audio samples should be rendered.")]
    public bool IsMuted
    {
        get => false;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("Gets or sets a value that indicates whether the MediaElement will display frames for seek operations before the final seek position is reached.")]
    public bool VerticalSyncEnabled
    {
        get => true;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("Gets or sets a value that indicates whether the MediaElement will display frames for seek operations before the final seek position is reached.")]
    public bool ScrubbingEnabled
    {
        get => true;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("Specifies how the underlying media should behave when it has loaded. The default behavior is to Play the media.")]
    public MediaPlaybackState LoadedBehavior
    {
        get => MediaPlaybackState.Manual;
        set => throw new NotSupportedException();
    }

    [Category(nameof(MediaElement))]
    [Description("Specifies the position of the underlying media. Set this property to seek though the media stream.")]
    public TimeSpan Position
    {
        get => TimeSpan.Zero;
        set => throw new NotSupportedException();
    }
}