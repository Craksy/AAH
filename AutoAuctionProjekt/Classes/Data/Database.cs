using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using AutoAuctionProjekt.Classes.Data.Adapters;
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
    
    private User? CurrentUser { get; set; }

    private Database()
    {
	    string connectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=sa;
            Password=H2PD081122_Gruppe3;
			MultipleActiveResultSets=True;";
	    conn = new SqlConnection(connectionString);
    }
    
	public void DBLogIn(string userName, string password)
    {
	    try {
			var connectionString = $@"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id={userName};
	        Password={password};";
	    conn = new SqlConnection(connectionString);
	    conn.Open();
   
	    // Console.WriteLine(GetLoggedInUser(userName));
	    } catch (Exception e) {
		     Debug.WriteLine($"Failed to connect to database: {e.Message}");
		     throw;
	    }
    }    
	
	public User GetLoggedInUser(string userName) {
		SqlCommand cmd = new(@"SELECT Users.Id,
							    Users.UserName,
							    Users.ZipCode,
							    Users.Balance,
							    sys.server_principals.name
							    FROM Users
							    LEFT JOIN sys.server_principals ON Users.UserName = sys.server_principals.name
								WHERE UserName = @userName"
		    , conn);
	    cmd.Parameters.AddWithValue("@userName", userName);
	    SqlDataReader reader = cmd.ExecuteReader();

		if (reader.HasRows)
		{
			while (reader.Read())
			{
				return new User(reader["UserName"].ToString()!, 
					reader["ZipCode"].ToString()!, 
					decimal.Parse(reader["Balance"].ToString()!));
			}
		}
		return null;
	}


	public User CreateUser(string Username, string Password, string ZipCode, string RegistrationNumber, UserType userType) {
		using (conn) {
			conn.Open();
			var cmd = new SqlCommand("CreateAuctionUser", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Username", Username);
			cmd.Parameters.AddWithValue("Password", Password);
			cmd.Parameters.AddWithValue("ZipCode", ZipCode);
			cmd.Parameters.AddWithValue(userType == UserType.PrivateUser ? "CprNumber" : "CvrNumber", RegistrationNumber);
			using (cmd)
			{
				using (var reader = cmd.ExecuteReader())
				{
					if (!reader.HasRows) throw new Exception("Failed to create user");
					reader.Read();
					return userType == UserType.PrivateUser 
						? UserAdapter.PrivateUserFromReader(reader) 
						: UserAdapter.CorporateUserFromReader(reader);

				}
			}
		}
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
    public string GetVehicleByID(int ID)
    {
	    SqlCommand cmd = new(@"SELECT Vehicles.ID,
    										Vehicles.Name,
	                                        Vehicles.Kilometers,
	                                        Vehicles.RegistrationNumber,
	                                        Vehicles.Year,
	                                        Vehicles.NewPrice,
	                                        Vehicles.HasTowbar,
	                                        Vehicles.KmPerLiter,
	                                        Vehicles.FuelType
	                                        FROM Vehicles
	                                        WHERE Vehicles.ID = @id"
		    , conn);
	    cmd.Parameters.AddWithValue("@id", ID);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.HasRows)
	    {
		    while (reader.Read())
		    {
				return reader.GetString(1);
		    }
	    }
	    return "No vehicles found.";
    }
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
												Trucks.LoadCapacity
											    FROM Vehicles
											    INNER JOIN HeavyVehicles ON Vehicles.ID = HeavyVehicles.VehicleID
											    INNER JOIN Trucks ON HeavyVehicles.ID = Trucks.HeavyVehicleID"
            , conn);
        SqlDataReader reader = cmd.ExecuteReader();
        
        List<Truck> trucks = new();
        if (reader.HasRows)
        {
	        while (reader.Read())
	        {
		        VehicleDimensionsStruct vd = new VehicleDimensionsStruct(
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
    
    //Users
    public string GetAllUsers()
    {
	    SqlCommand cmd = new(@"SELECT Users.Id,
										       Users.UserName,
										       Users.ZipCode,
										       Users.Balance,
										       PrivateUsers.Id,
										       PrivateUsers.CprNumber,
										       CorporateUsers.Id,
										       CorporateUsers.CvrNumber,
										       CorporateUsers.CreditScore
										       FROM Users
											LEFT JOIN PrivateUsers ON Users.Id = PrivateUsers.Id
										    LEFT JOIN CorporateUsers ON Users.Id = CorporateUsers.Id"
		    , conn);
	    SqlDataReader reader = cmd.ExecuteReader();
	    
	    List<User> users = new ();
	    if (reader.HasRows)
	    {
		    while (reader.Read())
		    {
			    
		    }
	    }
	    return "No users found.";
    }

    public string GetUser(int id)
    {
	    return UserAdapter.GetUserById(conn, id).ToString();
    }
    
    // Auctions
    public List<Auction> GetCurrentAuctions() {
	    return AuctionAdapter.GetAuctions(conn).ToList();
    }
    
    public List<Auction> GetYourAuctions(int id) {
	    return AuctionAdapter.GetAuctions(conn, null, null, id, null).ToList();
    }
    
}