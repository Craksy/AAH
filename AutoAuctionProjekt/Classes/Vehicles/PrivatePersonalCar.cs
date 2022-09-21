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
            : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeat, trunkDimentions, licenseBE)
        {
            //TODO: V19 - PrivatePersonalCar constructor. DriversLicense should be 'B'
            //TODO: V20 - Add to database and set ID
            NumberOfSeat = numberOfSeat;
            TrunkDimentions = trunkDimentions;
            HasIsofixFittings = hasIsofixFittings;
            DriversLicense = licenseBE ? DriversLisenceEnum.BE : DriversLisenceEnum.B;
        }
        /// <summary>
        /// Isofix Fittings property
        /// </summary>
        public bool HasIsofixFittings { get; set; }
        /// <summary>
        /// Returns the PrivatePersonalCar in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            return "-- PrivatePersonalCar --" +
                   "\nName: " + Name +
                   "\nKm: " + Km +
                   "\nRegistration number: " + RegistrationNumber +
                   "\nYear: " + Year +
                   "\nNew price: " + NewPrice +
                   "\nTowbar: " + HasTowbar +
                   "\nEngine size" + EngineSize +
                   "\nKm per liter: " + KmPerLiter +
                   "\nFueltype: " + FuelType +
                   "\nNumber of seats: " + NumberOfSeat +
                   "\nTrunk dimentions: " + 
                   "\n\tWidth: " + TrunkDimentions.Width +
                   "\n\tHeight: " + TrunkDimentions.Height +
                   "\n\tDepth: " + TrunkDimentions.Depth +
                   "\nIsofix: " + HasIsofixFittings + 
                   "\nLicense: " + DriversLicense;
        }
    }
}
