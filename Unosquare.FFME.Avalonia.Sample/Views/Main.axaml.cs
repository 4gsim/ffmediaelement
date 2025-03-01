using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace Unosquare.FFME.Avalonia.Sample.Views;

public partial class Main : Window
{
    private bool _changed;

    public Main()
    {
        InitializeComponent();
        _ = Task.Run(async () => 
        {
            var path1 = Path.GetFullPath("./sample1.mp4");
            await MediaPlayer1.Open(new Uri(path1));

            var path2 = Path.GetFullPath("./sample2.avi");
            await MediaPlayer2.Open(new Uri(path2));
            MediaPlayer2.MediaEnded += async (s, e) => 
            {
                if (!_changed)
                {
                    await MediaPlayer2.Close();
                    var path3 = Path.GetFullPath("./sample3.mov");
                    await MediaPlayer2.Open(new Uri(path3));
                    await Task.Delay(2000);
                    await MediaPlayer2.Play();
                    _changed = true;
                }
            };
            await MediaPlayer2.Play();
        });
#if DEBUG
        this.AttachDevTools();
#endif
    }
}