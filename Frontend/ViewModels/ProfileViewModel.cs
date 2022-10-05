using ReactiveUI;

namespace Frontend.ViewModels;

public class ProfileViewModel : ReactiveObject, IRoutableViewModel {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Profile";
    
}