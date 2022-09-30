using ReactiveUI;

namespace Frontend.ViewModels; 

public class DashboardViewModel : ReactiveObject, IRoutableViewModel{
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Dashboard";

    public DashboardViewModel() {
    }
}