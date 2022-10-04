using System.Net.Mime;
using AutoAuctionProjekt.Classes.Data;
using AutoAuctionProjekt.Classes.Vehicles;
using Frontend.Views;
using ReactiveUI;
using SkiaSharp;

namespace Frontend.ViewModels; 

public class DashboardViewModel : ReactiveObject, IRoutableViewModel{
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Dashboard";
    
    static Database _db = Database.Instance;
    static string _str = _db.GetCar();
    
    public string Test
    {
        get => _str;
        set
        {
            _str = value;
        }
    }

    public DashboardViewModel()
    {
    }
}