﻿using System;
using System.Collections.Generic;
using AutoAuctionProjekt.Classes;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            //AuctionHouse objects init
            #region init car objects
            PersonalCar.TrunkDimentionsStruct td = new PersonalCar.TrunkDimentionsStruct(14.0, 10.0, 16.0);
            VehicleDimensionsStruct vd = new(214.0, 2.59, 12.9);

            PrivatePersonalCar privateCar1 = new ( "Some car brand", 300.0, "DF12745", 2009, 10000M, false, 10.0, 20.0, FuelTypeEnum.Diesel, 3, td, true, true);
            PrivatePersonalCar privateCar2 = new ("Another car brand", 300.0, "DF12345", 2020, 12000M, true, 10.0, 20.0, FuelTypeEnum.Benzin, 5, td, false, false);
            ProfessionalPersonalCar professionalCar = new ("Suzuki Swift", 500.0, "XY12345", 2012, 10000M, 10.0, 20.0, FuelTypeEnum.Benzin, 2, td, true, 400.0, true);
            Bus bus = new Bus("City bus", 800.0, "HE24745", 2012, 30000M, true, 10.0, 15.0, FuelTypeEnum.Diesel, vd, 24, 10, true);
            #endregion

            #region init user objects
            User user1 = new PrivateUser("lkri", "password1", 7400, 0000000000);
            user1.Balance = 35000M;
            User user2 = new CorporateUser("fros", "password2", 9000, 99999999, 40000M);
            user2.Balance = 35000M;
            #endregion

            AuctionHouse.SetForSale(privateCar1, user1, 10000M);
            AuctionHouse.SetForSale(privateCar2, user1, 10000M);
            AuctionHouse.SetForSale(professionalCar, user2, 20000M,
                new NodificationDelegate(msg => "delegate message from user - " + msg));
            AuctionHouse.SetForSale(bus, user1, 20000M);

            AuctionHouse.RecieveBid(user2, 0, 30_000M);
            AuctionHouse.RecieveBid(user1, 1, 30_000M);
            AuctionHouse.RecieveBid(user2, 2, 50_500M);

            AuctionHouse.AcceptBid(user1, 1);

            Console.WriteLine("________ Search examples _________");
            Console.WriteLine("________ Search Vehicles by name _________");
            PrintVehicleList(AuctionHouse.FindVehiclesByName("swift").Result);
            Console.WriteLine("________ Search Vehicles by seats and toilet _________");
            PrintVehicleList(AuctionHouse.FindVehiclesByNumberofSeats(10, true).Result);
            Console.WriteLine("________ Search Vehicles by Drivers Lisence and weight _________");
            PrintVehicleList(AuctionHouse.FindVehiclesByDriversLisence(2.60).Result);
            Console.WriteLine("________ Search Vehicles by km and price _________");
            PrintVehicleList(AuctionHouse.FindVehiclesByKmAndPrice(310.0, 12000M).Result);
            Console.WriteLine("________ Search Sellers by range of zipcode _________");
            PrintISellerList(AuctionHouse.FindSellersByZipcodeRange(7000, 500).Result);
        }
        /// <summary>
        /// Makes a string of vehicles from a list.
        /// </summary>
        /// <param name="vehicles"></param>
        /// <returns> A string of vehicle information </returns>
        public static string PrintVehicleList(List<Vehicle> vehicles)
        {
            //TODO: return formatted string with vehicles from list
            throw new NotImplementedException();
        }
        /// <summary>
        /// Makes a string of ISellers from a list.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static string PrintISellerList(List<ISeller> users)
        {
            //TODO: return formatted string with users from list
            throw new NotImplementedException();
        }
    }
}
