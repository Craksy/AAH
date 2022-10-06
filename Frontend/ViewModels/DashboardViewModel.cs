using AutoAuctionProjekt.Classes;
using AutoAuctionProjekt.Classes.Data;
using Avalonia.Collections;
using ReactiveUI;

namespace Frontend.ViewModels; 

    // public record TestAuction(Vehicle vehicle, ISeller seller, decimal minimumPrice);
public class DashboardViewModel : ReactiveObject {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Dashboard";
    static Database _db = Database.Instance;
    public AvaloniaList<Auction> CurrentAuctions { get; set; }
    public AvaloniaList<Auction> YourAuctions { get; set; }

    public DashboardViewModel()
    {
        CurrentAuctions = new AvaloniaList<Auction>();
        CurrentAuctions.AddRange(_db.GetCurrentAuctions());

        YourAuctions = new AvaloniaList<Auction>();
        // YourAuctions.AddRange(_db.GetYourAuctions());
    }
}