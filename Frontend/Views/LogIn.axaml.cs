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

public partial class LogIn : ReactiveUserControl<LogInViewModel>
{
    static Database _db = Database.Instance;

    public LogIn()
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
        var result = _db.DBLogIn(model!.userName, model.passWord);
        
        // update viewmodel LoginResult
        model.CurrentUser!.UserName = model.userName;
        model.LoginResult = result;
        model.RaisePropertyChanged(nameof(model.LoginResult));
    }
}