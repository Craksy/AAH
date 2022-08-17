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
            double LoadCapacity) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, vehicleDimentions)
        {
            //TODO: V10 - Constructor for Truck, DriversLisence should be CE if the truck has a towbar, otherwise it should be C
            //TODO: V11 - Add to database and set ID
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load Capacity field and property
        /// </summary>
        public double LoadCapacity { get; set; }
        /// <summary>
        /// Returns the Truck in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            //TODO: V12 - ToString for Truck 
            throw new NotImplementedException();
        }
    }
}
