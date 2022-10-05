using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace AutoAuctionProjekt.Classes.Data.Adapters; 

public static class AuctionAdapter {
    
    public static IEnumerable<Auction> GetAuctions(SqlConnection connection) {
        var records = new List<Auction>();
        using (connection) {
            connection.Open();
            using (var command = new SqlCommand(GetAuctionQuery, connection)) {
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
    
    #region SQL Queries
    // REVIEW: Do we want to just unconditionally fetch the related data? or should we multiple views/procs and do it on demand?
    private static string GetAuctionQuery = $@"
SELECT 
    {UserAdapter.UsersCommon("SU")},
    {UserAdapter.UsersCommon("BU")},
    Auctions.ID, 
    Auctions.AuctionStart, 
    Auctions.VehicleID, 
    Auctions.SellerID, 
    Auctions.HighestBidderID, 
    Auctions.StandingBid, 
    Auctions.MinimumBid,
    {VehicleAdapter.VehiclesCommon}
    FROM Auctions
    INNER JOIN Vehicles ON Vehicles.ID = Auctions.VehicleID
    INNER JOIN Users SU ON SU.ID = Auctions.SellerID
    INNER JOIN Users BU ON BU.ID = Auctions.HighestBidderID
    ";

    private const string GetCompletedAuctionQuery = @"
SELECT 
    Auctions.ID, 
    Auctions.AuctionStart, 
    Auctions.VehicleID, 
    Auctions.SellerID, 
    Auctions.HighestBidderID, 
    Auctions.StandingBid, 
    Auctions.MinimumBid,
    CompletedAuctions.ID, 
    CompletedAuctions.AuctionEnd, 
    CompletedAuctions.BuyerID, 
    CompletedAuctions.SoldAt
    FROM Auctions
    INNER JOIN CompletedAuctions ON Auctions.ID = CompletedAuctions.ID
    INNER JOIN Vehicles ON Vehicles.ID = Auctions.VehicleID
    INNER JOIN Users SU ON SU.ID = Auctions.SellerID
    INNER JOIN Users BU ON BU.ID = CompletedAuctions.BuyerID
    ";

    #endregion
    
    public static Auction AuctionFromReader(SqlDataReader reader) {
        var seller = new User((string) reader[1], (string)reader[2], (int) reader[3]){ID = (int)reader[0]};
        var buyer = new User((string) reader[5], (string)reader[6], (int) reader[7]){ID = (int)reader[4]};
        var vehicle = VehicleAdapter.VehicleFromReader(reader);
        return new Auction(
            vehicle,
            seller,
            (decimal)reader["MinimumBid"]
        );
    }
}