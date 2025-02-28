using Unosquare.FFME.Common;
using Unosquare.FFME.Engine;

namespace Unosquare.FFME.Platform;

internal partial class MediaConnector
{
    public IMediaRenderer CreateRenderer(MediaType mediaType, MediaEngine mediaCore)
    {
        return new VideoRenderer(mediaCore);
    }
}