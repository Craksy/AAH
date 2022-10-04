using System;
using System.Diagnostics;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public AvaloniaProperty<TabItemViewModel> SelectedTabProperty { get; }
        = AvaloniaProperty.Register<MainWindow, TabItemViewModel>(nameof(SelectedTab),
            defaultBindingMode: BindingMode.TwoWay);

    private bool _isDrawerOpen;
    
    public static readonly DirectProperty<MainWindow, bool> IsDrawerOpenProperty =
        AvaloniaProperty.RegisterDirect<MainWindow, bool>(
            "IsDrawerOpen", o => o.IsDrawerOpen, (o, v) => o.IsDrawerOpen = v);

    public bool IsDrawerOpen {
        get => _isDrawerOpen;
        set => SetAndRaise(IsDrawerOpenProperty, ref _isDrawerOpen, value);
    }

    public TabItemViewModel SelectedTab {
        get => (TabItemViewModel) GetValue(SelectedTabProperty)!;
        set => SetValue(SelectedTabProperty, value);
    }

    public MainWindow() {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent() {
        this.WhenActivated(d => {
            this.Bind(ViewModel, vm => vm.IsDrawerOpen, v => v.IsDrawerOpen)
                .DisposeWith(d);
        });
        AvaloniaXamlLoader.Load(this);
    }

    private void ToggleSidebar() {
        if (DataContext is MainWindowViewModel vm) {
            vm.IsDrawerOpen = !vm.IsDrawerOpen;
        }
    }

    private void BeginDragWindow(object? sender, PointerPressedEventArgs e) {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) {
            BeginMoveDrag(e);
        }
    }
}