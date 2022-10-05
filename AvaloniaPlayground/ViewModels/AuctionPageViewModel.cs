using ReactiveUI;

namespace AvaloniaPlayground.ViewModels;

public class AuctionPageViewModel : ReactiveObject, IRoutableViewModel {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Auction";

    public AuctionPageViewModel(IScreen screen) {
        HostScreen = screen;
    }
}