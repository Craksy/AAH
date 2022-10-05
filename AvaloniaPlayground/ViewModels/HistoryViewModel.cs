using ReactiveUI;

namespace AvaloniaPlayground.ViewModels;

public class HistoryViewModel : ReactiveObject, IRoutableViewModel {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "History";

    public HistoryViewModel(IScreen screen) {
        HostScreen = screen;
    }
}