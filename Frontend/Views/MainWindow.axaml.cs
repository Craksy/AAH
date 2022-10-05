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
        });
        AvaloniaXamlLoader.Load(this);
    }

    private void BeginDragWindow(object? sender, PointerPressedEventArgs e) {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) {
            BeginMoveDrag(e);
        }
    }
}