using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace AutoAuctionProjekt.Classes.Data.Adapters; 

public class UserAdapter {

    public static IEnumerable<User> GetAllUsers(SqlConnection connection) =>
        Database.GetAll(connection, AllUsers, UserFromReader);
    public static User GetUserById(SqlConnection connection, int Id)
        {
            using (connection)
            {
                connection.Open();
                var cmd = new SqlCommand(UserById, connection);
                cmd.Parameters.AddWithValue("@UserId", Id);
                using (cmd)
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return UserFromReader(reader);
                        }
                        throw new Exception("Couldn't find user " + Id);
                    }
                }
            }
        }
    public static IEnumerable<PrivateUser> GetPrivateUsers(SqlConnection connection) =>
        Database.GetAll(connection, PrivateUsersQuery, PrivateUserFromReader);
    
    public static IEnumerable<CorporateUser> GetCorporateUsers(SqlConnection connection) =>
        Database.GetAll(connection, PrivateUsersQuery, CorporateUserFromReader);

    #region SQL Queries
    public static string UsersCommon (string prefix = "Users") => $@"{prefix}.ID, {prefix}.UserName, {prefix}.ZipCode, {prefix}.Balance";

    public static string AllUsers = $@"SELECT {UsersCommon()} FROM Users";
    
    public static string UserById = $@"SELECT {UsersCommon()} FROM Users where ID = @UserId";
    
    
    
    private static string PrivateUsersQuery = $@" 
SELECT {UsersCommon()}, PrivateUsers.CprNumber 
FROM Users INNER JOIN PrivateUsers ON Users.ID = PrivateUsers.ID";
    
    private static string CorporateUsersQuery = $@"
SELECT {UsersCommon()}, CorporateUsers.CvrNumber, CorporateUsers.CreditScore
FROM Users INNER JOIN CorporateUsers ON Users.ID = CorporateUsers.ID";
    #endregion
    
    
    
    // public static User UserFromReader(SqlDataReader reader) {
    //     return UserFromReader(reader, "Users");
    // }

    public static User UserFromReader(SqlDataReader reader) {
        return new User(
            (string) reader["UserName"],
            (string) reader["ZipCode"],
            (int) reader["Balance"]) { ID = (int) reader["ID"] };
    }
    
    public static PrivateUser PrivateUserFromReader(SqlDataReader reader) {
        return new PrivateUser(
            (string) reader["UserName"],
            (string) reader["ZipCode"],
            (int) reader["Balance"],
            (string) reader["CprNumber"]) { ID = (int) reader["ID"] };
    }
    
    public static CorporateUser CorporateUserFromReader(SqlDataReader reader) {
        return new CorporateUser(
            (string) reader["UserName"],
            (string) reader["ZipCode"],
            (int) reader["Balance"],
            (string) reader["CvrNumber"],
            (int) reader["CreditScore"]) { ID = (int) reader["ID"] };
    }
}