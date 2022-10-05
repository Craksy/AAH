using System.ComponentModel;
using AutoAuctionProjekt.Classes;
using Avalonia.Collections;
using Frontend.Views;
using MaterialDesign.Avalonia.PackIcon;
using ReactiveUI;

namespace Frontend.ViewModels;

public class LogInViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; } = null!;
    public string? UrlPathSegment { get; } = "LogIn";
    public User? CurrentUser { get; set; }
    public bool _loggedInTest { get; set; }
    public string userName { get; set; } = "";
    public string passWord { get; set; } = "";
    public string LoginResult { get; set; }

    // public bool LoggedInTest
    // {
    //     get => _loggedInTest;
    //     set => this.RaiseAndSetIfChanged(ref _loggedInTest, value);
    // }

    public AvaloniaList<TabItemViewModel> TabItems { get; set; } = new();

    public LogInViewModel()
    {
        TabItems.Add(new TabItemViewModel("Dashboard", new DashboardViewModel(), PackIconKind.ViewDashboard));
        TabItems.Add(new TabItemViewModel("Profile", new ProfileViewModel(), PackIconKind.Settings));
    }
}