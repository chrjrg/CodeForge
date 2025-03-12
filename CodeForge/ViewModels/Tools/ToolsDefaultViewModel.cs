using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeForge.ViewModels.Tools;

public partial class ToolsDefaultViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _buttonClickText = "Binding test";

    [RelayCommand]
    private void TestBinding()
    {
        ButtonClickText = "Binding Works!";
    }
}