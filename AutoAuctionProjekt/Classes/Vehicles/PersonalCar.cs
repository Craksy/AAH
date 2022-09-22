using System;

namespace AutoAuctionProjekt.Classes.Vehicles
{
    public abstract class PersonalCar : Vehicle
    {
        private ushort _numberOfSeat;
        private TrunkDimentionsStruct _trunkDimentions;

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
            TrunkDimentionsStruct trunkDimentions,
            bool licenceBE)
            : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType)
        {
            NumberOfSeat = numberOfSeat;
            TrunkDimentions = trunkDimentions;
            DriversLisence = licenceBE ? DriversLisenceEnum.BE : DriversLisenceEnum.B;
        }

        /// <summary>
        /// Number of seat property
        /// </summary>
        public ushort NumberOfSeat
        {
            get => _numberOfSeat;
            set
            {
                if (value >= 2 && value <= 7)
                {
                    _numberOfSeat = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(NumberOfSeat),
                        "Number of seats must be equal to or between 2 and 7");
                }
            }
        }

        /// <summary>
        /// Trunk dimentions property and struct
        /// </summary>
        public TrunkDimentionsStruct TrunkDimentions
        {
            get => _trunkDimentions;
            set { _trunkDimentions = value; }
        }

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
                    throw new ArgumentOutOfRangeException(nameof(EngineSize),
                        "Engine size must be between 7.0 and 10.0 L");
            }
        }
    }
}