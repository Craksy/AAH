using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    public class ProfessionalPersonalCar : PersonalCar
    {
        private double _loadCapacity;

        public ProfessionalPersonalCar(
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType,
            ushort numberOfSeat,
            TrunkDimentionsStruct trunkDimentions,
            bool hasSafetyBar,
            double loadCapacity,
            bool licenseBE)
            : base(name, km, registrationNumber, year, newPrice, true, engineSize, kmPerLiter, fuelType, numberOfSeat,
                trunkDimentions, licenseBE)
        {
            LoadCapacity = loadCapacity;
            HasSafetyBar = hasSafetyBar;
        }

        /// <summary>
        /// Safety Bar property
        /// </summary>
        public bool HasSafetyBar { get; set; }

        /// <summary>
        /// Load Capacity property
        /// </summary>
        public double LoadCapacity
        {
            get => _loadCapacity;
            set
            {
                _loadCapacity = value;
                DriversLisence = value >= 750 ? DriversLisenceEnum.BE : DriversLisenceEnum.B;
            }
        }
    }
}
