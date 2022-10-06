using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Reactive;
using Avalonia;
using Avalonia.Controls.Primitives;
using Frontend.Views;
using MaterialDesign.Avalonia.PackIcon;
using ReactiveUI;

namespace Frontend.ViewModels;

public class TabItem : ReactiveObject {
    public string Title { get; }
    public ReactiveObject Page { get; }
    public PackIconKind? Icon { get; }

    public TabItem(string title, ReactiveObject page, PackIconKind? icon = null) {
        Title = title;
        Page = page;
        Icon = icon;
    }

}

public class MainWindowViewModel : ReactiveObject
{
    public bool IsLoggedIn { get; set; }
    
    public ObservableCollection<TabItem> Tabs { get; }
    
    private TabItem? _selectedTab;
    public TabItem SelectedTab {
        get => _selectedTab!;
        set => this.RaiseAndSetIfChanged(ref _selectedTab, value);
    }
    
    public MainWindowViewModel() {
        Tabs = new ObservableCollection<TabItem> {
            new("LogIn", new LogInViewModel(), PackIconKind.Exchange),
            new("Dashboard", new DashboardViewModel(), PackIconKind.ViewDashboard),
            new("History", new HistoryViewModel(), PackIconKind.History),
            new("Profile", new ProfileViewModel(), PackIconKind.Settings),
            new("Auctions", new AuctionPageViewModel(), PackIconKind.Exchange),
        };
    }
}