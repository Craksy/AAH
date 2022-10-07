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
            HasIsofixFittings = hasIsofixFittings;
        }
        
        public PrivatePersonalCar(VehicleProps vehicleProps, ushort numberOfSeats, TrunkDimentionsStruct trunkDims, bool hasIsofixFittings, bool licenseBE)
            : base(vehicleProps, numberOfSeats, trunkDims, licenseBE)
        {
            HasIsofixFittings = hasIsofixFittings;
        }

        /// <summary>
        /// Isofix Fittings property
        /// </summary>
        public bool HasIsofixFittings { get; set; }
    }
}
