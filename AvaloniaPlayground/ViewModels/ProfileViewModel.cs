using ReactiveUI;

namespace AvaloniaPlayground.ViewModels;

public class ProfileViewModel : ReactiveObject, IRoutableViewModel {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Profile";

    public ProfileViewModel(IScreen screen) {
        HostScreen = screen;
    }
}