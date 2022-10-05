// using System.Collections.ObjectModel;
// using Avalonia;
// using Avalonia.Collections;
// using Avalonia.Data;
// using Frontend.Views;
// using ReactiveUI;
//
// namespace Frontend.ViewModels;
//
// public class SidebarViewModel : ReactiveObject {
//     public AvaloniaList<TabItemViewModel> Tabs { get; set; }
//     public bool Expanded { get; set; }
//     private TabItemViewModel? _selectedTab;
//
//     public TabItemViewModel SelectedTab {
//         get => _selectedTab!;
//         set => this.RaiseAndSetIfChanged(ref _selectedTab, value);
//     }
//
//     public SidebarViewModel() {
//         Tabs ??= new AvaloniaList<TabItemViewModel> {
//             new TabItemViewModel("Dashboard", new DashboardViewModel()),
//         };
//     }
// }

