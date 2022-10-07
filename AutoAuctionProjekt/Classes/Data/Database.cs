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
    private SqlConnection _conn;
    private const string DefaultConnectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=God;
            Password=Generous;
			MultipleActiveResultSets=True;
";
    

    /// <summary>
    /// Instance of the Database singleton
    /// </summary>
    public static Database Instance => _instance ??= new Database();
    
    public User? CurrentUser { get; private set; }

    private Database() {
	    const string connectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=sa;
            Password=H2PD081122_Gruppe3;
			MultipleActiveResultSets=True;";
	    _conn = new SqlConnection(connectionString);
    }
    
	public void DBLogIn(string userName, string password)
    {
	    var connectionString = $@"
				Server=docker.data.techcollege.dk,20003;
				Database=Auction_House;
				User Id={userName};
				Password={password};";
	    try {
			using var loginConnection = new SqlConnection(connectionString);
			loginConnection.Open();
			using var command = new SqlCommand(@"
SELECT * FROM Users 
LEFT JOIN sys.server_principals ON Users.UserName = sys.server_principals.name 
WHERE Users.UserName = @userName", loginConnection);
			command.Parameters.AddWithValue("@userName", userName);
			using var reader = command.ExecuteReader();
			if (reader.Read()) {
				CurrentUser = UserAdapter.UserFromReader(reader);
				_conn = new SqlConnection(connectionString);
				Debug.WriteLine("Login successful");
				return true;
			}
			Debug.WriteLine("Failed to get corresponding user");
			return false;
	    } catch (Exception e) {
		     Debug.WriteLine($"Login failed: {e.Message}");
		     return false;
	    }
    }    
	
	public User CreateUser(string Username, string Password, string ZipCode, string RegistrationNumber, UserType userType) {
		Debug.WriteLine("connection string: " + _conn.ConnectionString);
		using var connection = _conn;
		connection.Open();
		using var cmd = new SqlCommand("CreateAuctionUser", connection);
		cmd.CommandType = CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("Username", Username);
		cmd.Parameters.AddWithValue("Password", Password);
		cmd.Parameters.AddWithValue("ZipCode", ZipCode);
		cmd.Parameters.AddWithValue(userType == UserType.PrivateUser ? "CprNumber" : "CvrNumber", RegistrationNumber);
		
		using var reader = cmd.ExecuteReader();
		if (!reader.HasRows) throw new Exception("Failed to create user");
		reader.Read();
		return userType == UserType.PrivateUser 
			? UserAdapter.PrivateUserFromReader(reader) 
			: UserAdapter.CorporateUserFromReader(reader);
	}

	public IEnumerable<Auction> GetYourAuctions() {
		if (CurrentUser != null) return AuctionAdapter.GetAuctions(_conn, sellerId: CurrentUser.ID);
		throw new Exception("No user logged in");
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
		    , _conn);
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
	    return UserAdapter.GetUserById(_conn, id).ToString();
    }
    
    // Auctions
    public List<Auction> GetCurrentAuctions() {
	    return AuctionAdapter.GetAuctions(_conn).ToList();
    }
}