using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Unosquare.FFME.Avalonia.Sample.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _videoVisible;

    [RelayCommand]
    public void Switch()
    {
        VideoVisible = !VideoVisible;
    }
}