DROP PROC IF EXISTS GetAuctionItems; GO;
CREATE PROC GetAuctionItems(
@AuctionId int = NULL,
@VehicleId int = NULL,
@SellerId int = NULL,
@BuyerId int = NULL) AS
BEGIN
    SELECT Auctions.ID,
           Auctions.AuctionStart,
           Auctions.StandingBid,
           Auctions.MinimumBid,
           Vehicles.ID,
           Vehicles.Name,
           Vehicles.Kilometers,
           Vehicles.RegistrationNumber,
           Vehicles.Year,
           Vehicles.NewPrice,
           Vehicles.HasTowbar,
           Vehicles.EngineSize,
           Vehicles.KmPerLiter,
           Vehicles.FuelType,
           Vehicles.EnergyClass,
           SU.Id       as SellerId,
           SU.UserName as SellerUserName,
           SU.ZipCode  as SellerZipCode,
           SU.Balance  as SellerBalance,
           BU.Id       as BuyerId,
           BU.UserName as BuyerUserName,
           BU.ZipCode  as BuyerZipCode,
           BU.Balance  as BuyerBalance
    FROM Auctions
             INNER JOIN Vehicles on Auctions.VehicleID = Vehicles.ID
             INNER JOIN Users SU on Auctions.SellerID = SU.Id
             INNER JOIN USERS BU on Auctions.HighestBidderID = BU.Id
    WHERE (@AuctionId IS NULL OR Auctions.ID = @AuctionId) 
        AND (@VehicleId IS NULL OR Vehicles.ID = @VehicleId)
        AND (@SellerId IS NULL OR Auctions.SellerID = @SellerId)
        AND (@BuyerId IS NULL OR Auctions.HighestBidderID = @BuyerId)
END
GO;

DROP PROC if exists CreateAuction; GO;
CREATE PROC CreateAuction(@vehicleId int, @sellerId int, @minimumBid money) AS
BEGIN
    INSERT INTO Auctions (VehicleID, SellerID, MinimumBid, StandingBid)
    VALUES (@vehicleId, @sellerId, @minimumBid, @minimumBid)
END
GO;

DROP PROC if exists UpdateAuction; GO;
CREATE PROC UpdateAuction(@auctionId int, @standingBid money, @highestBidderId int) AS
BEGIN
    UPDATE Auctions
    SET StandingBid = @standingBid,
        HighestBidderID = @highestBidderId
    WHERE ID = @auctionId
END
GO;

DROP PROC IF EXISTS EndAuction; GO;
CREATE PROC EndAuction(@auctionId int) AS
BEGIN
    INSERT INTO CompletedAuctions (ID, BuyerID, SoldAt) 
    SELECT ID, HighestBidderID, StandingBid
    FROM Auctions
    WHERE ID = @auctionId
END
GO;