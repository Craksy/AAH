using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    class Truck : HeavyVehicle
    {
        public Truck(
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            bool hasTowbar,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType,
            VehicleDimensionsStruct vehicleDimentions,
            double loadCapacity) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, vehicleDimentions)
        {
            LoadCapacity = loadCapacity;
            DriversLisence = hasTowbar ? DriversLisenceEnum.CE : DriversLisenceEnum.C;
            
            //TODO: V11 - Add to database and set ID
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load Capacity field and property
        /// </summary>
        public double LoadCapacity { get; set; }
    }
}
