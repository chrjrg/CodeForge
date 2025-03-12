using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeForge.ViewModels.API;

public partial class ApiDefaultViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _buttonClickText = "Binding test";

    [RelayCommand]
    private void TestBinding()
    {
        ButtonClickText = "Binding Works!";
    }
}