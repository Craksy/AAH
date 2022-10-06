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
    public UserType UserType {
        get => _userType;
        set {
            this.RaiseAndSetIfChanged(ref _userType, value);
            this.RaisePropertyChanged(nameof(RegistrationNumberName));
        }
    }

    public string RegistrationNumberName => UserType == UserType.PrivateUser ? "CPR Number" : "CVR Number";
    
    public ReactiveCommand<Unit, Unit> CreateUserCommand { get; }

    public CreateUserViewModel() {
        CreateUserCommand = ReactiveCommand.Create(() => {
            var db = Database.Instance;
            if (Password != ConfirmPassword)
                return;
            var user = db.CreateUser(UserName, Password, ZipCode, RegistrationNumber, UserType);
        });
    }
}