using System;
using System.Collections.Generic;
using AutoAuctionProjekt.Classes;
using AutoAuctionProjekt.Classes.Data;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = Database.Instance;
            
            db.GetAllVehicles();
        }
    }
}
