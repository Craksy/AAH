using System.Collections.ObjectModel;
using Avalonia.Collections;
using MaterialDesign.Avalonia.PackIcon;
using ReactiveUI;

namespace Frontend.ViewModels;

public class HistoryViewModel : ReactiveObject, IRoutableViewModel {
    public IScreen HostScreen { get; } = null!;
    public string? UrlPathSegment { get; } = "History";

    public AvaloniaList<TabItemViewModel> TabItems { get; set; } = new();

    public HistoryViewModel() {
        TabItems.Add(new TabItemViewModel("Dashboard", new DashboardViewModel(), PackIconKind.ViewDashboard));
        TabItems.Add(new TabItemViewModel("Profile", new ProfileViewModel(), PackIconKind.Settings));
    }
}