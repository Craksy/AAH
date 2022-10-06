using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt.Classes.Data.Adapters; 

public class VehicleAdapter  {

    public static IEnumerable<Bus> GetAllBuses(SqlConnection connection) => Database.GetAll(connection, GetBussesQuery, BusFromReader);
    public static IEnumerable<Vehicle> GetAllVehicles(SqlConnection connection) => Database.GetAll(connection, GetBussesQuery, VehicleFromReader);
    public static IEnumerable<Truck> GetAllTrucks(SqlConnection connection) => Database.GetAll(connection, GetTrucksQuery, TruckFromReader);
    public static IEnumerable<PrivatePersonalCar> GetAllPrivatePersonalCars(SqlConnection connection) => Database.GetAll(connection, PrivatePersonalCarsQuery, GetPrivatePersonalCarFromReader);
    public static IEnumerable<ProfessionalPersonalCar> GetAllProfessionalPersonalCars(SqlConnection connection) => Database.GetAll(connection, ProfessionalPersonalCarsQuery, GetProfessionalPersonalCarFromReader);

    
    
    #region SQL Queries

    public const string VehiclesCommon = @"
Vehicles.ID,
Vehicles.Name,
Vehicles.Kilometers,
Vehicles.RegistrationNumber,
Vehicles.NewPrice,
Vehicles.Year,
Vehicles.HasTowbar,
Vehicles.EngineSize,
Vehicles.KmPerLiter,
Vehicles.FuelType,
Vehicles.EnergyClass
";

    public const string HeavyVehiclesCommon = @"
HeavyVehicles.Height,
HeavyVehicles.Weight,
HeavyVehicles.Length
";

    public const string PersonalCarsCommon = @"
PersonalCar.EngineSize,
PersonalCar.Seats,
PersonalCar.TrunkHeight,
PersonalCar.TrunkWidth,
PersonalCar.TrunkDepth,
PersonalCar.DriversLicense
";
    
    public const string GetBussesQuery = $@"SELECT 
{VehiclesCommon}, {HeavyVehiclesCommon}, Busses.Seats, Busses.SleepingSpaces, Busses.HasToilet, Busses.DriversLicense
FROM Vehicles 
INNER JOIN HeavyVehicles ON Vehicles.ID = HeavyVehicles.VehicleID
INNER JOIN Busses ON HeavyVehicles.ID = Busses.HeavyVehicleID
";
    
    public const string GetTrucksQuery = $@"
SELECT {VehiclesCommon}, {HeavyVehiclesCommon}, Trucks.LoadCapacity
FROM Vehicles 
INNER JOIN HeavyVehicles ON Vehicles.ID = HeavyVehicles.VehicleID
INNER JOIN Trucks ON HeavyVehicles.ID = Trucks.HeavyVehicleID
";

    public const string PrivatePersonalCarsQuery = $@"
SELECT {VehiclesCommon}, {PersonalCarsCommon}, PrivatePersonalCar.HasIsofix
FROM Vehicles
INNER JOIN PersonalCar ON Vehicles.ID = PersonalCar.VehicleID
INNER JOIN PrivatePersonalCar ON PersonalCar.ID = PrivatePersonalCar.PersonalCarID
";
    
    public const string ProfessionalPersonalCarsQuery = $@"SELECT 
{VehiclesCommon}, {PersonalCarsCommon}, ProfessionalPersonCar.LoadCapacity, ProfessionalPersonCar.SafetyBar, ProfessionalPersonCar.PersonCarID
FROM Vehicles
INNER JOIN PersonalCar ON Vehicles.ID = PersonalCar.VehicleID
INNER JOIN ProfessionalPersonCar ON PersonalCar.ID = ProfessionalPersonCar.PersonCarID
";
    #endregion

    /// <summary>
    /// Get common properties for all vehicles
    /// </summary>
    /// <param name="reader">data reader </param>
    /// <returns></returns>
    private static VehicleProps VehiclePropsFromReader(SqlDataReader reader) => new() {
            Name = (string) reader["Name"],
            Km = (double) reader["Kilometers"],
            RegistrationNumber = (string)reader["RegistrationNumber"],
            Year = (short) reader["Year"],
            NewPrice = (int) reader["NewPrice"],
            HasTowbar = (bool) reader["HasTowbar"],
            EngineSize = (double) reader["EngineSize"],
            KmPerLiter = (double) reader["KmPerLiter"],
            FuelType = Enum.Parse<FuelTypeEnum>((string)reader["FuelType"])
        };

    public static Vehicle VehicleFromReader(SqlDataReader reader)
    {
        return new(
            VehiclePropsFromReader(reader)){ID = Convert.ToUInt32(reader["ID"])};
    }

    /// <summary>
    /// Construct a Bus from a reader object
    /// </summary>
    public static Bus BusFromReader(SqlDataReader reader) {
        return new(
            VehiclePropsFromReader(reader),
            new VehicleDimensionsStruct {
                Length = (double) reader["Length"],
                Height = (double) reader["Height"],
                Weight = (double) reader["Weight"]
            },
            (ushort) reader["NumberOfSeats"],
            (ushort) reader["NumberOfSleepingPlaces"],
            (bool) reader["HasToilet"]
        ) {ID = (uint) reader["ID"]};
    }

    /// <summary>
    /// Construct a Truck from a reader object
    /// </summary>
    public static Truck TruckFromReader(SqlDataReader reader) {
        return new(
            VehiclePropsFromReader(reader),
            new VehicleDimensionsStruct {
                Length = (double) reader["Length"],
                Height = (double) reader["Height"],
                Weight = (double) reader["Weight"]
            },
            (double) reader["LoadCapacity"]
        ) {ID = (uint) reader["ID"]};
    }

    /// <summary>
    /// Construct a PrivatePersonalCar from a reader object
    /// </summary>
    public static PrivatePersonalCar GetPrivatePersonalCarFromReader(SqlDataReader reader) {
        
        return new(
            VehiclePropsFromReader(reader),
            (ushort) reader["NumberOfSeats"],
            new PersonalCar.TrunkDimentionsStruct() {
                Depth = (double) reader["TrunkDepth"],
                Height = (double) reader["TrunkHeight"],
                Width = (double) reader["TrunkWidth"]
            },
            (bool) reader["HasIsofix"],
            (bool) reader["LicenseBE"] // TODO: this is actually a varchar in the database.
        ) {ID = (uint) reader["ID"]};
    }
    
    /// <summary>
    /// Construct a ProfessionalPersonalCar from a reader object
    /// </summary>
    public static ProfessionalPersonalCar GetProfessionalPersonalCarFromReader(SqlDataReader reader) {
        return new(
            VehiclePropsFromReader(reader),
            (ushort) reader["NumberOfSeats"],
            new PersonalCar.TrunkDimentionsStruct() {
                Depth = (double) reader["TrunkDepth"],
                Height = (double) reader["TrunkHeight"],
                Width = (double) reader["TrunkWidth"]
            },
            (bool) reader["LicenseBE"], // TODO: this is actually a varchar in the database.
            (double) reader["LoadCapacity"],
            (bool) reader["HasSafetyBar"]
        ) {ID = (uint) reader["ID"]};
    }
}