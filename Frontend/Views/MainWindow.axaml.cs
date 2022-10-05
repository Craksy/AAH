using System;
using System.Diagnostics;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;
using TabItem = Frontend.ViewModels.TabItem;

namespace Frontend.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public AvaloniaProperty<TabItem> SelectedTabProperty { get; }
        = AvaloniaProperty.Register<MainWindow, TabItem>(nameof(SelectedTab),
            defaultBindingMode: BindingMode.TwoWay);

    private bool _isDrawerOpen;
    
    public static readonly DirectProperty<MainWindow, bool> IsDrawerOpenProperty =
        AvaloniaProperty.RegisterDirect<MainWindow, bool>(
            "IsDrawerOpen", o => o.IsDrawerOpen, (o, v) => o.IsDrawerOpen = v);

    public bool IsDrawerOpen {
        get => _isDrawerOpen;
        set => SetAndRaise(IsDrawerOpenProperty, ref _isDrawerOpen, value);
    }

    public TabItem SelectedTab {
        get => (TabItem) GetValue(SelectedTabProperty)!;
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
            // this.Bind(ViewModel, vm => vm.IsDrawerOpen, v => v.IsDrawerOpen)
            //     .DisposeWith(d);
            var contentArea = this.FindControl<Border>("ContentArea");
            contentArea.AddHandler(Button.ClickEvent, OnContentAreaButtonClick);
        });
        AvaloniaXamlLoader.Load(this);
    }

    private void OnContentAreaButtonClick(object? sender, RoutedEventArgs e) {
        if (e.Source is Button button && button.Name == "LoginButton") {
            ViewModel.IsLoggedIn = true;
            ViewModel.RaisePropertyChanged(nameof(ViewModel.IsLoggedIn));
            e.Handled = true;
        }
        else {
            e.Handled = false;
        }
    }

    private void BeginDragWindow(object? sender, PointerPressedEventArgs e) {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) {
            BeginMoveDrag(e);
        }
    }
}