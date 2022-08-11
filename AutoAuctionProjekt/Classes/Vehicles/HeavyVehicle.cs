using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes
{
    public abstract class HeavyVehicle : Vehicle
    {
        public HeavyVehicle(
         string name,
         double km,
         string registrationNumber,
         ushort year,
         decimal newPrice,
         bool hasTowbar,
         double engineSize,
         double kmPerLiter,
         FuelTypeEnum fuelType,
         VehicleDimensionsStruct vehicleDimentions) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType)
        {
            this.VehicleDimensions = vehicleDimentions;
        }
        /// <summary>
        /// Vehicle dimentions proberty and struct
        /// </summary>
        public VehicleDimensionsStruct VehicleDimensions { get; set; }
        /// <summary>
        /// The dimensions of the vehicle i meters.
        /// </summary>
        public struct VehicleDimensionsStruct
        {
            public VehicleDimensionsStruct(double height, double weight, double length)
            {
                Height = height;
                Weight = weight;
                Length = length;
            }
            public double Height { get; }
            public double Weight { get; }
            public double Length { get; }
            public override string ToString() => $"(Height: {Height}, Weight: {Weight}, Depth: {Length})";
        }
        
        private double _engineSize;
        
        /// <summary>
        /// Engine size proberty
        /// must be between 4.2 and 15.0 L or cast an out of range exection.
        /// </summary>
        public override double EngineSize
        {
            get => _engineSize;
            set
            {
                //V7 - TODO value must be between 4.2 and 15.0 L or cast an out of range exection.
                if (value is >= 4.2 and <= 15.0)
                    _engineSize = value;
                throw new ArgumentOutOfRangeException(nameof(EngineSize), "EngineSize must be between 4.2 and 15.0 L");
            }
        }
        
        
        /// <summary>
        /// Returns the HeavyVehicle in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            //TODO: V6 - ToString for HeavyVehicle
            throw new NotImplementedException();
        }
    }
}
