using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    public class PrivatePersonalCar : PersonalCar
    {
        public PrivatePersonalCar(
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            bool hasTowbar,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType,
            ushort numberOfSeat,
            TrunkDimentionsStruct trunkDimentions,
            bool hasIsofixFittings,
            bool licenseBE)
            : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType,
                numberOfSeat, trunkDimentions, licenseBE)
        {
            //TODO: V19 - PrivatePersonalCar constructor. DriversLicense should be 'B'
            //TODO: V20 - Add to database and set ID
            throw new NotImplementedException();
        }

        /// <summary>
        /// Isofix Fittings property
        /// </summary>
        public bool HasIsofixFittings { get; set; }
    }
}
