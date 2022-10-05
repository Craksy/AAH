using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class Sidebar : UserControl {
    public static readonly DirectProperty<Sidebar, IEnumerable> TabItemsProperty =
        AvaloniaProperty.RegisterDirect<Sidebar, IEnumerable>(
            nameof(TabItems),
            o => o.TabItems,
            (o, v) => o.TabItems = v);

    private IEnumerable _tabItems = new AvaloniaList<object>();

    public IEnumerable TabItems {
        get => _tabItems;
        set => SetAndRaise(TabItemsProperty, ref _tabItems, value);
    }


    public AvaloniaProperty<TabItemViewModel> SelectedTabProperty { get; }
        = AvaloniaProperty.Register<MainWindow, TabItemViewModel>(nameof(SelectedTab),
            defaultBindingMode: BindingMode.TwoWay);

    public TabItemViewModel? SelectedTab {
        get => (TabItemViewModel) GetValue(SelectedTabProperty)!;
        set => SetValue(SelectedTabProperty, value);
    }

    public AvaloniaProperty<bool> ExpandedProperty { get; }
        = AvaloniaProperty.Register<Sidebar, bool>(nameof(Expanded),
            defaultBindingMode: BindingMode.TwoWay);

    public bool Expanded {
        get => (bool) GetValue(ExpandedProperty)!;
        set {
            SetValue(ExpandedProperty, value);
            PseudoClasses.Set(":expanded", value);
        }
    }

    public Sidebar() {
        TabItems = new AvaloniaList<TabItemViewModel>();
        InitializeComponent();
    }

    private void InitializeComponent() {
        // this.WhenActivated(d => {
        //     // this.Bind(ViewModel, vm => vm.Tabs, v => v.TabItems).DisposeWith(d);
        //     this.Bind(ViewModel, vm => vm.SelectedTab, v => v.SelectedTab).DisposeWith(d);
        //     this.Bind(ViewModel, vm => vm.Expanded, v => v.Expanded).DisposeWith(d);
        // });
        AvaloniaXamlLoader.Load(this);
    }

    private void ExpanderClick(object? sender, RoutedEventArgs e) {
        Expanded = !Expanded;
        Debug.WriteLine("ExpanderClick");
        Debug.WriteLine("Number of items: ");
        foreach (TabItemViewModel i in TabItems) {
            Debug.WriteLine(i.Title);
            Debug.WriteLine(i.Icon);
        }
    }
}