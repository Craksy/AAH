using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaPlayground.ViewModels;
using ReactiveUI;

namespace AvaloniaPlayground.Views;

public partial class AuctionPage : ReactiveUserControl<AuctionPageViewModel> {
    public AuctionPage() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { AvaloniaXamlLoader.Load(this); });
        AvaloniaXamlLoader.Load(this);
    }
}