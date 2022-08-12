using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes
{
    class Bus : HeavyVehicle
    {
        public Bus (
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
            bool hasToilet) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, vehicleDimentions)
        {
            this.NumberOfSeats = numberOfSeats;
            this.NumberOfSleepingSpaces = numberOfSleepingSpaces;
            this.HasToilet = hasToilet;
            //TODO: V7 - set contructor and DriversLisence to DE if the car has a towbar or D if not.
            //TODO: V8 - Add to database and set ID
            throw new NotImplementedException();
        }


        // private double _engineSize;
        //
        // /// <summary>
        // /// Engine size proberty
        // /// must be between 4.2 and 15.0 L or cast an out of range exection.
        // /// </summary>
        // public override double EngineSize
        // {
        //     get => _engineSize;
        //     set
        //     {
        //         //V7 - TODO value must be between 4.2 and 15.0 L or cast an out of range exection.
        //         if (value is >= 4.2 and <= 15.0)
        //             _engineSize = value;
        //         throw new ArgumentOutOfRangeException(nameof(EngineSize), "EngineSize must be between 4.2 and 15.0 L");
        //     }
        // }
        //
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
        /// <summary>
        /// Returns the Bus in a string with relevant information.
        /// </summary>
        public override string ToString()
        {
            //TODO: V9 - Tostring for bus
            throw new NotImplementedException();
        }
    }
}
