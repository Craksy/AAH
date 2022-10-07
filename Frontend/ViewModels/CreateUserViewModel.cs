using System;
using System.Diagnostics;
using System.Reactive;
using AutoAuctionProjekt.Classes;
using AutoAuctionProjekt.Classes.Data;
using Avalonia.Collections;
using ReactiveUI;

namespace Frontend.ViewModels; 

public class CreateUserViewModel : ReactiveObject {
    
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ZipCode { get; set; }
    public string RegistrationNumber { get; set; }

    private AvaloniaList<UserType> UserTypes { get; } = new() { UserType.PrivateUser, UserType.CorporateUser };
    private UserType _userType = UserType.PrivateUser;
    private string _creationResult;

    public UserType UserType {
        get => _userType;
        set {
            this.RaiseAndSetIfChanged(ref _userType, value);
            this.RaisePropertyChanged(nameof(RegistrationNumberName));
        }
    }

    public string RegistrationNumberName => UserType == UserType.PrivateUser ? "CPR Number" : "CVR Number";
    
    public ReactiveCommand<Unit, Unit> CreateUserCommand { get; }

    public string CreationResult
    {
        get => _creationResult;
        set => this.RaiseAndSetIfChanged(ref _creationResult, value);
    }
    
    

    public CreateUserViewModel()
    {
        this.WhenAnyValue(
                x => x.UserName, 
                x => x.Password, 
                x => x.ConfirmPassword, 
                x => x.ZipCode,
                x => x.UserType,
                x => x.RegistrationNumber)
            .Subscribe(_ => this.RaisePropertyChanged());
        _creationResult = "";
        CreateUserCommand = ReactiveCommand.Create(() => {
            var db = Database.Instance;
            if (Password != ConfirmPassword)
                return;
            try
            {
                db.CreateUser(UserName, Password, ZipCode, RegistrationNumber, UserType);    
                UserName = "";
                Password = "";
                ConfirmPassword = "";
                ZipCode = "";
                RegistrationNumber = "";
                CreationResult = "User created successfully";
            }catch (Exception e)
            {
                Debug.WriteLine("Error creating user: " + e.Message);
                CreationResult = "Failed to create user: " + e.Message;
            }
        });
    }
}