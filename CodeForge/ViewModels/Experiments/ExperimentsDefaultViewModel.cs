using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeForge.ViewModels.Experiments;

public partial class ExperimentsDefaultViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _buttonClickText = "Binding test";

    [RelayCommand]
    private void TestBinding()
    {
        ButtonClickText = "Binding Works!";
    }
}