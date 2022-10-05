using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class History : ReactiveUserControl<HistoryViewModel> {
    public History() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(d => { });
        AvaloniaXamlLoader.Load(this);
    }
}