using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes
{
    public abstract class Vehicle
    {
        protected Vehicle(string name,
            double km,
            string registrationNumber,
            int year,
            decimal newPrice,
            bool hasTowbar,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType)
        {
            this.Name = name;
            this.Km = km;
            this.RegistrationNumber = registrationNumber;
            this.Year = year;
            this.NewPrice = newPrice;
            this.HasTowbar = hasTowbar;
            this.EngineSize = engineSize;
            this.KmPerLiter = kmPerLiter;
            this.FuelType = fuelType;
            //TODO: V1 - Constructor for Vehicle
            //TODO: V2 - Add to database and set ID
        }
        /// <summary>
        /// ID field and proberty
        /// </summary>
        public uint ID { get; private set; }
        /// <summary>
        /// Name field and proberty
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Km field and proberty
        /// </summary>
        public double Km { get; set; }
        /// <summary>
        /// Registration number field and proberty
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Year field and proberty
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// New price field and proberty
        /// </summary>
        public decimal NewPrice { get; set; }
        /// <summary>
        /// Towbar field and proberty
        /// </summary>
        public bool HasTowbar { get; set; }
        /// <summary>
        /// Engine size field and proberty
        /// </summary>
        public virtual double EngineSize { get; set; }
        /// <summary>
        /// Km per liter field and proberty
        /// </summary>
        public double KmPerLiter { get; set; }
        /// <summary>
        /// Drivers lisence Enum, field and proberty
        /// </summary>
        public DriversLisenceEnum DriversLisence { get; set; }
        public enum DriversLisenceEnum
        {
            A,
            B,
            C,
            D,
            BE,
            CE,
            DE
        }
        /// <summary>
        /// NFuel type Enum, field and proberty
        /// </summary>
        public FuelTypeEnum FuelType { get; set; }
        public enum FuelTypeEnum
        {
            Diesel,
            Benzin
        }
        /// <summary>
        /// Engery class Enum, field and proberty
        /// </summary>
        public EnergyClassEnum EnergyClass { get { return EnergyClass; } set => GetEnergyClass(); }
        public enum EnergyClassEnum
        {
            A,
            B,
            C,
            D
        }
        /// <summary>
        /// Engery class is calculated bassed on year of the car and the efficiancy in km/L.
        /// </summary>
        /// <returns>
        /// Returns the energy class in EnergyClassEnum (A,B,C,D)
        /// </returns>
        private EnergyClassEnum GetEnergyClass()
        {
            //TODO: V4 - Implement GetEnergyClass
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the vehicle in a string with relivant information.
        /// </summary>
        /// <returns>The Veihcle as a string</returns>
        public new virtual string ToString()
        {
            //TODO: V3 - Vehicle tostring
            throw new NotImplementedException();
        }
    }
}
