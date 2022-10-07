using AutoAuctionProjekt.Classes.Data;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class LogInView : ReactiveUserControl<LogInViewModel>
{
    private MainWindowViewModel MainWin;

    public LogInView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(d => {
            MainWin = this.FindLogicalAncestorOfType<MainWindow>().ViewModel;
        });
        AvaloniaXamlLoader.Load(this);
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Database.Instance.DBLogIn(ViewModel.userName, ViewModel.passWord)) {
            ViewModel.LoginResult = "Wow, you're so great at logging in, that's so bare minimum of you@";
            MainWin.IsLoggedIn = true;
            MainWin.RaisePropertyChanged(nameof(MainWin.IsLoggedIn));
        }
        else
            ViewModel.LoginResult = "Wow, well done not logging in";
        ViewModel?.RaisePropertyChanged(nameof(ViewModel.LoginResult));
    }

    private void BypassLogin_Click(object? sender, RoutedEventArgs e) {
        MainWin.IsLoggedIn = true;
        MainWin.RaisePropertyChanged(nameof(MainWin.IsLoggedIn));
    }
}