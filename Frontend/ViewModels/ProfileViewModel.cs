using AutoAuctionProjekt.Classes;
using AutoAuctionProjekt.Classes.Data;
using Avalonia.Collections;
using ReactiveUI;

namespace Frontend.ViewModels;
public class ProfileViewModel : ReactiveObject {
    private Database _db;
    public AvaloniaList<Auction> Auctions { get; set; }

    public ProfileViewModel() {
        _db = Database.Instance;
        // Auctions = new AvaloniaList<Auction>();
        // Auctions.AddRange(_db.GetCurrentAuctions());
    }
}