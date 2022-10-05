using System;
using AutoAuctionProjekt.Classes.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class LogInView : ReactiveUserControl<LogInViewModel>
{
    static Database _db = Database.Instance;

    public LogInView()
    {
        InitializeComponent();
        DataContext = new LogInViewModel();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(d => { });
        AvaloniaXamlLoader.Load(this);
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        LogInViewModel? model = (LogInViewModel?)DataContext;
        //var getUsername = _db.GetLoggedInUser(model.userName);
        try
        {
            _db.DBLogIn(model.userName, model.passWord);
        }
        catch (Exception err)
        {
            model.LoginResult = "Wow, well done not logging in " + model._loggedInTest;
            model.RaisePropertyChanged(nameof(model.LoginResult));
            return;
        }
        
        // update viewmodel LoginResult
        // model.CurrentUser!.UserName = model.userName;
        model.LoginResult = "Wow, you're so great at logging in, that's so bare minimum of you " + model._loggedInTest;
        model.RaisePropertyChanged(nameof(model.LoginResult));
    }
}