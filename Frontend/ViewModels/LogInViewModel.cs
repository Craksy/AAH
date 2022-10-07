using AutoAuctionProjekt.Classes;

namespace Frontend.ViewModels;

public class LogInViewModel : ViewModelBase
{
    public User? CurrentUser { get; set; }
    public bool _loggedInTest { get; set; }
    public string userName { get; set; } = "";
    public string passWord { get; set; } = "";
    public string LoginResult { get; set; }

    // public bool LoggedInTest
    // {
    //     get => _loggedInTest;
    //     set => this.RaiseAndSetIfChanged(ref _loggedInTest, value);
    // }
    public LogInViewModel() {
    }
}