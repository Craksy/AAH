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
            // PersonalCar.TrunkDimentionsStruct td = new PersonalCar.TrunkDimentionsStruct(
            //     14.0, 10.0, 16.0);
            //
            // PrivatePersonalCar privateCar2 = new ("Another car brand", 300.0,
            //     "DF12345", 2009, 12000M, true, 
            //     10.0, 9.0, FuelTypeEnum.Benzin, 5, td,
            //     false, false);
            //
            // Console.WriteLine("Fueltype: " + privateCar2.FuelType
            //                                + "\nEnergy class: " + privateCar2.EnergyClass 
            //                                + "\nYear: " + privateCar2.Year);

            var auctions = db.GetCurrentAuctions();
            foreach (var auction in auctions)
            {
                Console.WriteLine(auction);
            }
        }
    }
}
