using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class AuctionPageView : ReactiveUserControl<AuctionPageViewModel> {
    public AuctionPageView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { AvaloniaXamlLoader.Load(this); });
        AvaloniaXamlLoader.Load(this);
    }
}