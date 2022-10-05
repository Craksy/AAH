using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mime;
using AutoAuctionProjekt.Classes.Data;
using AutoAuctionProjekt.Classes.Vehicles;
using Frontend.Views;
using ReactiveUI;
using SkiaSharp;

namespace Frontend.ViewModels; 

public class DashboardViewModel : ReactiveObject {
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "Dashboard";
    public ObservableCollection<CurrentAuction> CurrentAuctions { get; }
    static Database _db = Database.Instance;
    
    public DashboardViewModel()
    {
        CurrentAuctions = new ObservableCollection<CurrentAuction>(GenerateMockCurrentAuctionTable());
    }

    public class CurrentAuction
    {
        public int DepartmentNumber { get; set; }
        
        public string DeskLocation{ get; set; }
        
        public int EmployeeNumber { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
    
    private IEnumerable<CurrentAuction> GenerateMockCurrentAuctionTable()
    {
        var currentAuction = new List<CurrentAuction>()
        {
            new CurrentAuction()
            {
                FirstName = "Pat", 
                LastName = "Patterson", 
                EmployeeNumber = 1010,
                DepartmentNumber = 100, 
                DeskLocation = "B3F3R5T7"
            },
            new CurrentAuction()
            {
                FirstName = "Jean", 
                LastName = "Jones", 
                EmployeeNumber = 973,
                DepartmentNumber = 200, 
                DeskLocation = "B1F1R2T3"
            },
            new CurrentAuction()
            {
                FirstName = "Terry", 
                LastName = "Tompson", 
                EmployeeNumber = 300,
                DepartmentNumber = 100, 
                DeskLocation = "B3F2R10T1"
            }
        };

        return currentAuction;
    }
}