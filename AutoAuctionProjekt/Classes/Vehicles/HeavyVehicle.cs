using System;

namespace AutoAuctionProjekt.Classes.Vehicles
{
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
            VehicleDimensions = vehicleDimentions;
        }
        
        /// <summary>
        /// Physical properties of the vehicle
        /// </summary>
        public VehicleDimensionsStruct VehicleDimensions { get; set; }
        
        private double _engineSize;
        
        /// <summary>
        /// Engine size property
        /// must be between 4.2 and 15.0 L or cast an out of range exection.
        /// </summary>
        public override double EngineSize
        {
            get => _engineSize;
            set
            {
                if (value is not (>= 4.2 and <= 15.0))
                    throw new ArgumentOutOfRangeException(nameof(EngineSize), "EngineSize must be between 4.2 and 15.0 L");
                _engineSize = value;
            }
        }
    }
}
