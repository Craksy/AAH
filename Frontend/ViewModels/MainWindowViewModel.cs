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

public class TabItem : TemplatedControl {
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<TabItem, string>(nameof(Title));

    public static readonly StyledProperty<PackIconKind> IconProperty =
        AvaloniaProperty.Register<TabItem, PackIconKind>(nameof(Icon));

    public static readonly StyledProperty<ReactiveObject> PageProperty =
        AvaloniaProperty.Register<TabItem, ReactiveObject>(nameof(Page));

    public ReactiveObject Page {
        get => GetValue(PageProperty);
        set => SetValue(PageProperty, value);
    }

    public string Title {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public PackIconKind Icon {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}

public class TabItemViewModel : ReactiveObject {
    public string Title { get; }
    public ReactiveObject Page { get; }
    public PackIconKind? Icon { get; }

    public TabItemViewModel(string title, ReactiveObject page, PackIconKind? icon = null) {
        Title = title;
        Page = page;
        Icon = icon;
    }

}

public class MainWindowViewModel : ReactiveObject, IScreen
{
    private static LogInViewModel n = new LogInViewModel();
    public RoutingState Router { get; } = new();
    private bool _isLoggedIn { get; set; } = n.CurrentUser != null;

    private bool _isDrawerOpen;
    public bool IsDrawerOpen {
        get => _isDrawerOpen;
        set {
            _isDrawerOpen = value;
            this.RaisePropertyChanged(nameof(IsDrawerOpen));
        }
    }

    public PackIconKind DrawerIcon { get; set; } = PackIconKind.ChevronDoubleRight;

    public ReactiveCommand<Unit, Unit> ToggleDrawerCommand { get; }

    public ObservableCollection<TabItemViewModel> Tabs { get; }
    private TabItemViewModel? _selectedTab;

    public TabItemViewModel SelectedTab {
        get => _selectedTab!;
        set => this.RaiseAndSetIfChanged(ref _selectedTab, value);
    }
    public MainWindowViewModel() {
        ToggleDrawerCommand = ReactiveCommand.Create(() => {
            IsDrawerOpen = !IsDrawerOpen;
            DrawerIcon = IsDrawerOpen ? PackIconKind.ChevronDoubleLeft : PackIconKind.ChevronDoubleRight;
            Debug.WriteLine("Drawer toggled" + IsDrawerOpen);
        });

        Tabs = new ObservableCollection<TabItemViewModel> {
            new("LogIn", new LogInViewModel(), PackIconKind.Exchange),
            new("Dashboard", new DashboardViewModel(), PackIconKind.ViewDashboard),
            new("History", new HistoryViewModel(), PackIconKind.History),
            new("Profile", new ProfileViewModel(), PackIconKind.Settings),
            new("Auctions", new AuctionPageViewModel(), PackIconKind.Exchange),
        };
    }
}