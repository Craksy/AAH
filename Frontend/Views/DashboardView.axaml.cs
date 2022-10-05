using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views; 

public partial class DashboardView : ReactiveUserControl<DashboardViewModel> {
    public DashboardView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}