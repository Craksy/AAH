using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt.Classes.Data;

public class Database
{
    private static Database? _instance;
    private SqlConnection conn;
    
    /// <summary>
    /// Instance of the Database singleton
    /// </summary>
    public static Database Instance => _instance ??= new Database();

    private Database()
    {
        const string connectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=sa;
            Password=H2PD081122_Gruppe3;";
        conn = new SqlConnection(connectionString);
    }
    
    
	public void DBLogIn(string userName, string passWord)
    {
	    try {
			var connectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=" + userName + "; " +
	                           "Password= " + passWord + ";";
	    conn = new SqlConnection(connectionString);
	    conn.Open();
	    } catch (Exception e) {
		     Debug.WriteLine($"Failed to connect to database: {e.Message}");
	    }
    }    
	
	public string GetLoggedInUser(string userName) {
	    SqlCommand cmd = new(@"SELECT * FROM dbo.Users 
	    						WHERE UserName = @userName"
		    , conn);
	    
	    cmd.Parameters.AddWithValue("userName", userName);
	    SqlDataReader reader = cmd.ExecuteReader();

	    return reader["name"].ToString();
    }
	
	    
	
    // Just a helper method to avoid code duplication. Doesn't really belong here though.
    // Perhaps Adapter should be in interface instead of passing a function as a parameter?
    public static IEnumerable<T> GetAll<T>(SqlConnection connection, string query, Func<SqlDataReader, T> readerMethod) {
        var records = new List<T>();
        using (connection) {
            connection.Open();
            using (var command = new SqlCommand(query, connection)) {
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        records.Add(readerMethod(reader));
                    }
                }
            }
        }
        return records;
    }

    
    // Vehicles
    public IEnumerable<Vehicle> GetAllVehicles()
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

        List<Vehicle> vehicles = new();
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
            }
        }
        else
        {
            Console.WriteLine("Couldn't find any vehicles.");
        }
        reader.Close();
        return vehicles;
    }

    public IEnumerable<Bus> GetAllBusses()
    {
        SqlCommand cmd = new(
	        @"SELECT           
			Vehicles.ID,
			Vehicles.Name,
			Vehicles.Kilometers,
			Vehicles.RegistrationNumber,
			Vehicles.Year,
			Vehicles.HasTowbar,
			Vehicles.EngineSize,
			Vehicles.KmPerLiter,
			Vehicles.FuelType,
			Vehicles.EnergyClass,
			HeavyVehicles.Height,
			HeavyVehicles.Weight,
			HeavyVehicles.Length,
			Busses.Seats,
			Busses.SleepingSpaces,
			Busses.HasToilet,
			Busses.DriversLicense
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
			}
		}
        
		else
		{
			Console.WriteLine("Couldn't find any busses.");
		}


        return busses;
    }
}