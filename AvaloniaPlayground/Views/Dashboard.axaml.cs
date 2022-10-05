using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaPlayground.ViewModels;
using ReactiveUI;

namespace AvaloniaPlayground.Views; 

public partial class Dashboard : ReactiveUserControl<DashboardViewModel> {
    public Dashboard() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}