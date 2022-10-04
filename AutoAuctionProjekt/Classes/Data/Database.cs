using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt.Classes.Data;

public class Database
{
    private static Database? _instance;
    private SqlConnection conn;

    // private Database()
    // {
    //     string connectionString = @"
    //         Server=docker.data.techcollege.dk,20003;
    //         Database=Auction_House;
    //         User Id=sa;
    //         Password=H2PD081122_Gruppe3;";
    //     conn = new SqlConnection(connectionString);
    //     conn.Open();
    // }

    public void DBLogIn(string userName, string passWord)
    {
	    string connectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=" + userName + "; " +
	        "Password= " + passWord + ";";
	    conn = new SqlConnection(connectionString);
	    conn.Open();
    }

    public static Database Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Database();
            return _instance;
        }
    }
    
    // Vehicles
    public IEnumerable<PrivatePersonalCar> GetAllPrivatePersonalCars()
    {
        SqlCommand cmd = new(@"SELECT Vehicles.Name,
	                                        Vehicles.Kilometers,
	                                        Vehicles.RegistrationNumber,
	                                        Vehicles.Year,
	                                        Vehicles.NewPrice,
	                                        Vehicles.HasTowbar,
	                                        PersonalCar.EngineSize,
	                                        Vehicles.KmPerLiter,
	                                        Vehicles.FuelType,
	                                        PersonalCar.Seats,
	                                        PersonalCar.TrunkHeight,
	                                        PersonalCar.TrunkWidth,
	                                        PersonalCar.TrunkDepth,
	                                        PersonalCar.DriversLicense,
	                                        PrivatePersonalCar.HasIsofix
	                                        FROM Vehicles
	                                        INNER JOIN PersonalCar ON Vehicles.ID = PersonalCar.VehicleID
	                                        INNER JOIN PrivatePersonalCar ON PersonalCar.ID = PrivatePersonalCar.PersonalCarID"
            , conn);
        SqlDataReader reader = cmd.ExecuteReader();

        List<PrivatePersonalCar> vehicles = new();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                PersonalCar.TrunkDimentionsStruct td = new PersonalCar.TrunkDimentionsStruct(
                    Double.Parse(reader.GetValue(10).ToString()!),
                    Double.Parse(reader.GetValue(11).ToString()!),
                    Double.Parse(reader.GetValue(12).ToString()!)
                );

                vehicles.Add(new PrivatePersonalCar(
                        reader.GetValue(0).ToString(),
                        Double.Parse(reader.GetValue(1).ToString()!),
                        reader.GetValue(2).ToString(),
                        ushort.Parse(reader.GetValue(3).ToString()!),
                        Int32.Parse(reader.GetValue(4).ToString()!),
                        Boolean.Parse(reader.GetValue(5).ToString()!),
                        Double.Parse(reader.GetValue(6).ToString()!),
                        Double.Parse(reader.GetValue(7).ToString()!),
                        (FuelTypeEnum)Enum.Parse(typeof(FuelTypeEnum), reader.GetValue(8).ToString()!),
                        ushort.Parse(reader.GetValue(9).ToString()!),
                        td,
                        Boolean.Parse(reader.GetValue(14).ToString()!),
                        Boolean.Parse(reader.GetValue(14).ToString()!)
                    ));
                Console.WriteLine(vehicles[0].Name);
            }
        }
        else
        {
            Console.WriteLine("Couldn't find any vehicles.");
        }
        reader.Close();
        return vehicles;
    }

    public string GetCar()
    {
	    SqlCommand cmd = new(@"SELECT Vehicles.Name,
	                                        Vehicles.Kilometers,
	                                        Vehicles.RegistrationNumber,
	                                        Vehicles.Year,
	                                        Vehicles.NewPrice,
	                                        Vehicles.HasTowbar,
	                                        PersonalCar.EngineSize,
	                                        Vehicles.KmPerLiter,
	                                        Vehicles.FuelType,
	                                        PersonalCar.Seats,
	                                        PersonalCar.TrunkHeight,
	                                        PersonalCar.TrunkWidth,
	                                        PersonalCar.TrunkDepth,
	                                        PersonalCar.DriversLicense,
	                                        PrivatePersonalCar.HasIsofix
	                                        FROM Vehicles
	                                        INNER JOIN PersonalCar ON Vehicles.ID = PersonalCar.VehicleID
	                                        INNER JOIN PrivatePersonalCar ON PersonalCar.ID = PrivatePersonalCar.PersonalCarID"
            , conn);
        SqlDataReader reader = cmd.ExecuteReader();

        
        if (reader.HasRows)
        {
	        while (reader.Read())
            {
	            // foreach (IDataRecord rd in reader)
	            // {
		            return reader["Name"].ToString();
	            // }
            }
        }
        else
        {
            Console.WriteLine("Couldn't find any vehicles.");
        }
        reader.Close();
        return "Not found";
    }

    public IEnumerable<Bus> GetAllBusses()
    {
        SqlCommand cmd = new(@"SELECT Vehicles.Name,
												Vehicles.Kilometers,
												Vehicles.RegistrationNumber,
												Vehicles.Year,
												Vehicles.NewPrice,
												Vehicles.HasTowbar,
												Vehicles.EngineSize,
												Vehicles.KmPerLiter,
												Vehicles.FuelType,
												HeavyVehicles.Height,
												HeavyVehicles.Weight,
												HeavyVehicles.Length,
												Busses.Seats,
												Busses.SleepingSpaces,
												Busses.HasToilet
												FROM Vehicles
												INNER JOIN HeavyVehicles ON Vehicles.ID = HeavyVehicles.VehicleID
												INNER JOIN Busses ON HeavyVehicles.ID = Busses.HeavyVehicleID"
            , conn);
        SqlDataReader reader = cmd.ExecuteReader();
        
        List<Bus> busses = new();
        if (reader.HasRows)
        {
	        while (reader.Read())
	        {
		        HeavyVehicle.VehicleDimensionsStruct vd = new HeavyVehicle.VehicleDimensionsStruct(
			        Double.Parse(reader.GetValue(9).ToString()!),
			        Double.Parse(reader.GetValue(10).ToString()!),
			        Double.Parse(reader.GetValue(11).ToString()!)
		        );

		        busses.Add(new Bus(
			        reader.GetValue(0).ToString(),
			        Double.Parse(reader.GetValue(1).ToString()!),
			        reader.GetValue(2).ToString(),
			        ushort.Parse(reader.GetValue(3).ToString()!),
			        Int32.Parse(reader.GetValue(4).ToString()!),
			        Boolean.Parse(reader.GetValue(5).ToString()!),
			        Double.Parse(reader.GetValue(6).ToString()!),
			        Double.Parse(reader.GetValue(7).ToString()!),
			        (FuelTypeEnum)Enum.Parse(typeof(FuelTypeEnum), reader.GetValue(8).ToString()!),
			        vd,
			        ushort.Parse(reader.GetValue(12).ToString()!),
			        ushort.Parse(reader.GetValue(13).ToString()!),
			        Boolean.Parse(reader.GetValue(14).ToString()!)
		        ));
	        }
        }
        else
        {
	        Console.WriteLine("Couldn't find any vehicles.");
        }
        reader.Close();
        return busses;
    }
    
    public IEnumerable<Truck> GetAllTrucks()
    {
        SqlCommand cmd = new(@"SELECT Vehicles.Name,
											    Vehicles.Kilometers,
											    Vehicles.RegistrationNumber,
											    Vehicles.Year,
											    Vehicles.NewPrice,
											    Vehicles.HasTowbar,
											    Vehicles.EngineSize,
											    Vehicles.KmPerLiter,
											    Vehicles.FuelType,
												HeavyVehicles.Height,
												HeavyVehicles.Weight,
												HeavyVehicles.Length,
												Truck.LoadCapacity
											    FROM Vehicles
											    INNER JOIN HeavyVehicles ON Vehicles.ID = HeavyVehicles.VehicleID
											    INNER JOIN Truck ON HeavyVehicles.ID = Truck.HeavyVehicleID"
            , conn);
        SqlDataReader reader = cmd.ExecuteReader();
        
        List<Truck> trucks = new();
        if (reader.HasRows)
        {
	        while (reader.Read())
	        {
		        HeavyVehicle.VehicleDimensionsStruct vd = new HeavyVehicle.VehicleDimensionsStruct(
			        Double.Parse(reader.GetValue(9).ToString()!),
			        Double.Parse(reader.GetValue(10).ToString()!),
			        Double.Parse(reader.GetValue(11).ToString()!)
		        );

		        trucks.Add(new Truck(
			        reader.GetValue(0).ToString(),
			        Double.Parse(reader.GetValue(1).ToString()!),
			        reader.GetValue(2).ToString(),
			        ushort.Parse(reader.GetValue(3).ToString()!),
			        Int32.Parse(reader.GetValue(4).ToString()!),
			        Boolean.Parse(reader.GetValue(5).ToString()!),
			        Double.Parse(reader.GetValue(6).ToString()!),
			        Double.Parse(reader.GetValue(7).ToString()!),
			        (FuelTypeEnum)Enum.Parse(typeof(FuelTypeEnum), reader.GetValue(8).ToString()!),
			        vd,
			        Double.Parse(reader.GetValue(12).ToString()!)
		        ));
	        }
        }
        else
        {
	        Console.WriteLine("Couldn't find any vehicles.");
        }
        reader.Close();
        return trucks;
    }
}