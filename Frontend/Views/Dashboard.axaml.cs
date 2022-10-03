using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views; 

public partial class Dashboard : ReactiveUserControl<DashboardViewModel> {
    public Dashboard() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}