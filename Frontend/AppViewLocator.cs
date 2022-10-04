using System;
using Frontend.ViewModels;
using Frontend.Views;
using ReactiveUI;

namespace Frontend;

public class AppViewLocator : IViewLocator {
    public IViewFor ResolveView<T>(T viewModel, string? contract = null) => viewModel switch {
        DashboardViewModel context => new Dashboard {DataContext = context},
        ProfileViewModel context => new Profile {DataContext = context},
        HistoryViewModel context => new History {DataContext = context},
        LogInViewModel context => new LogIn {DataContext = context},
        AuctionPageViewModel context => new AuctionPage {DataContext = context},
        _ => throw new ArgumentOutOfRangeException("Unknown view model type: " + viewModel.GetType())
    };
}