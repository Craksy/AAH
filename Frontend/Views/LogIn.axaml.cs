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
    private static LogInViewModel _logInViewModel { get; }= new LogInViewModel();

    public LogIn()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(d => { });
        AvaloniaXamlLoader.Load(this);
    }


    
    private void Test_OnClick(object? sender, RoutedEventArgs e)
    {

        // _logInViewModel.Test;
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {

        string userName = _logInViewModel.userName;
        string passWord = _logInViewModel.passWord;
        _db.DBLogIn(userName, passWord);
    }
}