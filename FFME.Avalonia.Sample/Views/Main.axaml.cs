using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FFME.Avalonia.Sample.Views;

public partial class Main : Window
{
    public Main()
    {
        InitializeComponent();
        _ = Task.Run(async () => 
        {
            var path = Path.GetFullPath("./sample.mp4");
            await MediaPlayer.Open(new Uri(path));
            await MediaPlayer.Play();
        });
#if DEBUG
        this.AttachDevTools();
#endif
    }
}