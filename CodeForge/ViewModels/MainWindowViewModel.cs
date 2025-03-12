using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using CodeForge.ViewModels.API;
using CodeForge.ViewModels.Experiments;
using CodeForge.ViewModels.Settings;
using CodeForge.ViewModels.Tools;
using CodeForge.Views.API;
using CodeForge.Views.Experiments;
using CodeForge.Views.Settings;
using CodeForge.Views.Tools;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeForge.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty] 
    private ViewModelBase _currentPage = new MainViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        Console.WriteLine($"Selected item changed to: {value.ModelType.Name}");

        if (value.SubItems.Any())
        {
            if (CurrentPage.GetType() == value.ModelType)
            {
                value.IsExpanded = !value.IsExpanded;
            }
            else
            {
                SelectedListItem = value;

                // Check if value.ModelType is a ViewModelBase
                if (typeof(ViewModelBase).IsAssignableFrom(value.ModelType))
                {
                    Console.WriteLine($"Attempting to create instance of {value.ModelType.Name}");
                    if (Activator.CreateInstance(value.ModelType) is ViewModelBase viewModel)
                    {
                        Console.WriteLine($"Navigating to: {viewModel.GetType().Name}");
                        CurrentPage = viewModel;
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Failed to create ViewModel for {value.ModelType.Name}");
                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: {value.ModelType.Name} is NOT a ViewModelBase");
                }

                value.IsExpanded = true;
            }
        }
        else
        {
            if (typeof(ViewModelBase).IsAssignableFrom(value.ModelType))
            {
                Console.WriteLine($"Attempting to create instance of {value.ModelType.Name}");
                if (Activator.CreateInstance(value.ModelType) is ViewModelBase viewModel)
                {
                    Console.WriteLine($"Navigating to: {viewModel.GetType().Name}");
                    CurrentPage = viewModel;
                }
                else
                {
                    Console.WriteLine($"ERROR: Failed to create ViewModel for {value.ModelType.Name}");
                }
            }
            else
            {
                Console.WriteLine($"ERROR: {value.ModelType.Name} is NOT a ViewModelBase");
            }
        }
    }


    // Menu list for all admin views
    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(ToolsDefaultViewModel), "ToolsIcon", "Tools", new ObservableCollection<ListItemTemplate>
        {
            new ListItemTemplate(typeof(PdfMergerViewModel), "ArrowIcon", "Pdf Merger"),
        }),

        new ListItemTemplate(typeof(ApiDefaultViewModel), "ApiIcon", "API", new ObservableCollection<ListItemTemplate>()),
        new ListItemTemplate(typeof(ExperimentsDefaultViewModel), "ExperimentsIcon", "Experiments", new ObservableCollection<ListItemTemplate>()),
        new ListItemTemplate(typeof(SettingsDefaultViewModel), "SettingsIcon", "Settings", new ObservableCollection<ListItemTemplate>()),
    };


    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
    
    public ICommand ExitCommand { get; }

    public MainWindowViewModel()
    {
        ExitCommand = new RelayCommand(ExitApplication);
    }

    private void ExitApplication()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}

public partial class ListItemTemplate : ViewModelBase
{
    public string Label { get; set; }
    public Type ModelType { get; }
    public StreamGeometry ListItemIcon { get; } 
    public ObservableCollection<ListItemTemplate> SubItems { get; } // Stores submenu items
    
    [ObservableProperty]
    private bool _isExpanded;
    private const string StreamGeometryNotFound = "Icon not found";
        
    public ListItemTemplate(Type type, string iconKey, string customLabel, ObservableCollection<ListItemTemplate>? subItems = null)
    {
        ModelType = type;
        Label = string.IsNullOrWhiteSpace(customLabel) ? type.Name.Replace("ViewModel", "") : customLabel;

        Application.Current!.TryFindResource(iconKey, out var res);
        var streamGeometry = res as StreamGeometry ?? StreamGeometry.Parse(StreamGeometryNotFound);
        ListItemIcon = streamGeometry;

        SubItems = subItems ?? new ObservableCollection<ListItemTemplate>();
    }
}