using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeForge.ViewModels.Settings;

public partial class SettingsDefaultViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _buttonClickText = "Binding test";

    [RelayCommand]
    private void TestBinding()
    {
        ButtonClickText = "Binding Works!";
    }
}