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
            this.EnergyClass = GetEnergyClass();

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
            Benzin,
            Electric,
            Hydrogen
        }

        /// <summary>
        /// Engery class Enum, field and proberty
        /// </summary>
        public EnergyClassEnum EnergyClass
        {
            get { return GetEnergyClass(); }
            set {  }
        }
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
            if(Year < 2010)
            {
                if (FuelType == FuelTypeEnum.Electric || FuelType == FuelTypeEnum.Hydrogen)
                {
                    EnergyClass = EnergyClassEnum.A;
                } else if (FuelType == FuelTypeEnum.Diesel)
                {
                    if (KmPerLiter >= 23)
                    {
                        EnergyClass = EnergyClassEnum.A;
                    } else if (KmPerLiter >= 18 && KmPerLiter < 23)
                    {
                        EnergyClass = EnergyClassEnum.B;
                    } else if (KmPerLiter >= 13 && KmPerLiter < 18)
                    {
                        EnergyClass = EnergyClassEnum.C;
                    } else if (KmPerLiter < 13)
                    {
                        EnergyClass = EnergyClassEnum.D;
                    }
                } else if (FuelType == FuelTypeEnum.Benzin)
                {
                    if (KmPerLiter >= 18)
                    {
                        EnergyClass = EnergyClassEnum.A;
                    } else if (KmPerLiter >= 14 && KmPerLiter < 18)
                    {
                        EnergyClass = EnergyClassEnum.B;
                    } else if (KmPerLiter >= 10 && KmPerLiter < 14)
                    {
                         EnergyClass = EnergyClassEnum.C;
                    } else if (KmPerLiter < 10)
                    {
                        EnergyClass = EnergyClassEnum.D;
                    }
                }
            }
            else
            { 
                if (FuelType == FuelTypeEnum.Diesel) {
                    if (KmPerLiter >= 25)
                    {
                        EnergyClass = EnergyClassEnum.A;
                    } else if (KmPerLiter >= 20 && KmPerLiter < 25)
                    {
                        EnergyClass = EnergyClassEnum.B;
                    } else if (KmPerLiter >= 15 && KmPerLiter < 20)
                    {
                        EnergyClass = EnergyClassEnum.C;
                    } else if (KmPerLiter < 15)
                    {
                        EnergyClass = EnergyClassEnum.D;
                    }
                } else if (FuelType == FuelTypeEnum.Benzin)
                {
                    if (KmPerLiter >= 20)
                    {
                        EnergyClass = EnergyClassEnum.A;
                    } else if (KmPerLiter >= 16 && KmPerLiter < 20)
                    {
                        EnergyClass = EnergyClassEnum.B;
                    } else if (KmPerLiter >= 12 && KmPerLiter < 16)
                    {
                        EnergyClass = EnergyClassEnum.C;
                    } else if (KmPerLiter < 12)
                    {
                        EnergyClass = EnergyClassEnum.D;
                    }
                }
            }

            return EnergyClassEnum.B;
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
