using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    public class Truck : HeavyVehicle
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
            DriversLicense = hasTowbar ? DriversLisenceEnum.CE : DriversLisenceEnum.C;
        }

        public Truck(VehicleProps vehicleProps, VehicleDimensionsStruct vehicleDimensions, double loadCapacity)
            : base(vehicleProps, vehicleDimensions) {
            LoadCapacity = loadCapacity;
            DriversLicense = vehicleProps.HasTowbar ? DriversLisenceEnum.CE : DriversLisenceEnum.C;
        }

        /// <summary>
        /// Load Capacity field and property
        /// </summary>
        public double LoadCapacity { get; set; }
    }
}
