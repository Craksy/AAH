using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AutoAuctionProjekt.Classes.Data.Adapters; 

public static class AuctionAdapter {
    
    public static IEnumerable<Auction> GetAuctions(SqlConnection connection, int? auctionId = null, int? vehicleId = null, int? sellerId = null, int? buyerId = null) {
        var records = new List<Auction>();
        using (connection) {
            connection.Open();
            var command = new SqlCommand("GetAuctionItems", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@AuctionId", auctionId);
            command.Parameters.AddWithValue("@VehicleId", vehicleId);
            command.Parameters.AddWithValue("@SellerId", sellerId);
            command.Parameters.AddWithValue("@BuyerId", buyerId);
            using (command) {
                Debug.WriteLine($"Running Query:\n{command.CommandText}");
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        records.Add(AuctionFromReader(reader));
                    }
                }
            }
        }

        return records;
    }
    
    public static Auction AuctionFromReader(SqlDataReader reader) {
        var seller = new User(
            (string)reader["SellerUserName"], 
            (string)reader["SellerZipCode"], 
            (int)reader["SellerBalance"]
        ){ID = (int)reader["SellerId"]};
        var buyer = new User(
            (string)reader["BuyerUserName"], 
            (string)reader["BuyerZipCode"], 
            (int)reader["BuyerBalance"]
        ){ID = (int)reader["BuyerId"]};
        var vehicle = VehicleAdapter.VehicleFromReader(reader);
        return new Auction(
            vehicle,
            seller,
            (decimal)reader["MinimumBid"]
        ) {
            StandingBid = (decimal)reader["StandingBid"],
            Buyer = buyer,
            ID = Convert.ToUInt32(reader["ID"]),
            StartDate = (DateTime)reader["AuctionStart"],
        };
    }
}