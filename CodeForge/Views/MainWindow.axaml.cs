using Avalonia.Controls;
using CodeForge.ViewModels;

namespace CodeForge.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}