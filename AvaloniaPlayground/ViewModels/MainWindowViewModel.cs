using System.Reactive;
using ReactiveUI;

namespace AvaloniaPlayground.ViewModels;

public enum Page {
    Dashboard,
    History,
    Inventory,
    Settings
}

public class MainWindowViewModel : ReactiveObject, IScreen {
    public string Greeting => "Welcome to Avalonia!";
    public RoutingState Router { get; } = new();
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
    public ReactiveCommand<Unit, Unit> GoBack { get; }


    public ReactiveCommand<Unit, IRoutableViewModel> GoToDashboard { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoToHistoryPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GotoProfilePage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GotoAuctionPage { get; }

    public ReactiveCommand<Unit, Unit> StartMoving { get; }


    public MainWindowViewModel() {
        GoNext = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new DashboardViewModel(this)));
        GoBack = Router.NavigateBack;
        GoToDashboard =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new DashboardViewModel(this)));
        GoToHistoryPage =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new HistoryViewModel(this)));
        GotoProfilePage =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new ProfileViewModel(this)));
        GotoAuctionPage =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new AuctionPageViewModel(this)));
    }
}