using System;
using System.Diagnostics;
using AutoAuctionProjekt.Classes.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class LogInView : ReactiveUserControl<LogInViewModel>
{
    static Database _db = Database.Instance;

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
        Debug.WriteLine("Attempting to log in...");
        try {
            _db.DBLogIn(ViewModel.userName, ViewModel.passWord);
            ViewModel.LoginResult = "Wow, you're so great at logging in, that's so bare minimum of you " +
                                ViewModel._loggedInTest;
            MainWin.IsLoggedIn = true;
            MainWin.RaisePropertyChanged(nameof(MainWin.IsLoggedIn));
        }
        catch (Exception err) {
            Debug.WriteLine("Error: " + err.Message);
            ViewModel.LoginResult = "Wow, well done not logging in " + err;
        }
        finally {
            Debug.WriteLine("Login attempt finished");
            ViewModel.RaisePropertyChanged(nameof(ViewModel.LoginResult));
        }
        
        // update viewmodel LoginResult
        // model.CurrentUser!.UserName = model.userName;
    }

    private void BypassLogin_Click(object? sender, RoutedEventArgs e) {

        try {
            _db.DBLogIn(ViewModel.userName, ViewModel.passWord);
        }
        catch {
            // ignored
        }

        ViewModel.LoginResult = "Wow, you're so great at logging in, that's so bare minimum of you " +
                                ViewModel._loggedInTest;
        MainWin.IsLoggedIn = true;
        MainWin.RaisePropertyChanged(nameof(MainWin.IsLoggedIn));
    }
}