begin
    DROP TABLE IF EXISTS CompletedAuctions
    DROP TABLE IF EXISTS Auctions
    
    CREATE TABLE Auctions (
        ID int IDENTITY(1,1) PRIMARY KEY,
        AuctionStart datetime NOT NULL DEFAULT GETDATE(),
        VehicleID int NOT NULL FOREIGN KEY REFERENCES Vehicles(ID),
        SellerID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
        HighestBidderID int DEFAULT NULL FOREIGN KEY REFERENCES Users(ID),
        StandingBid MONEY NOT NULL,
        MinimumBid MONEY NOT NULL)
    
    CREATE TABLE CompletedAuctions (
        ID int PRIMARY KEY FOREIGN KEY REFERENCES Auctions(ID),
        AuctionEnd datetime NOT NULL DEFAULT GETDATE(),
        BuyerID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
        SoldAt MONEY NOT NULL)
end