using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using AutoAuctionProjekt;
using AutoAuctionProjekt.Classes;
using AutoAuctionProjekt.Classes.Data;
using AutoAuctionProjekt.Classes.Vehicles;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Frontend.Views;
using ReactiveUI;
using SkiaSharp;

namespace Frontend.ViewModels; 

    public record TestAuction(Vehicle vehicle, ISeller seller, decimal minimumPrice);
public class DashboardViewModel : ReactiveObject, IDataTemplate {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Dashboard";
    static Database _db = Database.Instance;
    // private string _test { get; set; } = _db.GetCurrentAuctions()[1].Seller.UserName;

    public AvaloniaList<Auction> CurrentAuctions { get; set; }

    public IControl Build(object Username)
    {
        return new TextBlock() { Text = (string)Username };
    }

    public bool Match(object data)
    {
        return data is string;
    }

    public DashboardViewModel()
    {
        CurrentAuctions = new AvaloniaList<Auction>();
        CurrentAuctions.AddRange(_db.GetCurrentAuctions());
        
        // CurrentAuctions = new ObservableCollection<CurrentAuction>(GenerateMockCurrentAuctionTable());
        // _db.GetCurrentAuctions();
    }
}