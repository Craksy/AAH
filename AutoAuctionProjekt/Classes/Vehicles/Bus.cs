using System;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    public class Bus : HeavyVehicle
    {
        public Bus(
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
            ushort numberOfSeats,
            ushort numberOfSleepingSpaces,
            bool hasToilet) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
            fuelType, vehicleDimentions)
        {
            NumberOfSeats = numberOfSeats;
            NumberOfSleepingSpaces = numberOfSleepingSpaces;
            HasToilet = hasToilet;
            DriversLicense = hasTowbar ? DriversLisenceEnum.DE : DriversLisenceEnum.D;
        }
        
        public Bus(
            VehicleProps vehicleProps,
            VehicleDimensionsStruct vehicleDimentions,
            ushort numberOfSeats,
            ushort numberOfSleepingSpaces,
            bool hasToilet) : base(vehicleProps, vehicleDimentions)
        {
            NumberOfSeats = numberOfSeats;
            NumberOfSleepingSpaces = numberOfSleepingSpaces;
            HasToilet = hasToilet;
            DriversLicense = vehicleProps.HasTowbar ? DriversLisenceEnum.DE : DriversLisenceEnum.D;
            
            //TODO: V8 - Add to database and set ID
        }
        
        /// <summary>
        /// Number of seats
        /// </summary>
        public ushort NumberOfSeats { get; set; }


        /// <summary>
        /// Number of sleeping spaces
        /// </summary>
        public ushort NumberOfSleepingSpaces { get; set; }

        /// <summary>
        /// Does the bus have a toilet?
        /// </summary>
        public bool HasToilet { get; set; }

    }
}
