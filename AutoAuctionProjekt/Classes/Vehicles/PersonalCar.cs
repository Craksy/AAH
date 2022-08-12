using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    public abstract class PersonalCar : Vehicle
    {
        protected PersonalCar(
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
            TrunkDimentionsStruct trunkDimentions)
            : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType)
        {
            this.NumberOfSeat = numberOfSeat;
            this.TrunkDimentions = trunkDimentions;
        }
        /// <summary>
        /// Number of seat property
        /// </summary>
        public ushort NumberOfSeat { get; set; }
        /// <summary>
        /// Trunk dimentions property and struct
        /// </summary>
        public TrunkDimentionsStruct TrunkDimentions { get; set; }
        public readonly struct TrunkDimentionsStruct
        {
            public TrunkDimentionsStruct(double height, double width, double depth)
            {
                Height = height;
                Width = width;
                Depth = depth;
            }
            public double Height { get; }
            public double Width { get; }
            public double Depth { get; }
            public override string ToString() => $"(Height: {Height}, Width: {Width}, Depth: {Depth})";
        }
        
        private double _engineSize;
        /// <summary>
        /// Engine size property
        /// must be between 0.7 and 10.0 L or cast an out of range exection.
        /// </summary>
        public override double EngineSize
        {
            get => _engineSize;
            set
            {
                //TODO: V13 - EngineSize: must be between 0.7 and 10.0 L or cast an out of range exection.
                if (value is >= 0.7 and <= 10.0)
                    _engineSize = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(EngineSize), "Engine size must be between 7.0 and 10.0 L");
            }
        }
        /// <summary>
        /// Returns the PersonalCar in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            //TODO: V15 - ToString for PersonalCar
            throw new NotImplementedException();
        }
    }
}