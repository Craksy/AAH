using System;
using Xunit;
using AutoAuctionProjekt.Classes;

namespace TestAutoAuctionProject
{
    /// <summary>
    /// Unit test for the functionalities of Auction House
    /// </summary>
    public class UnitTestAuctionHouse // This is not part of the assignment, but can be used to help qualifing the project.
    {
        #region Init Vehicle objects
        PrivatePersonalCar privateCar = new PrivatePersonalCar(
            "car brand", 
            500.0, 
            "DF12745", 
            2018, 
            10000M, 
            false, 
            10.0, 
            20.0, 
            Vehicle.FuelTypeEnum.Diesel, 
            2,
            new PersonalCar.TrunkDimentionsStruct(14.0, 10.0, 16.0), 
            true);

        ProfessionalPersonalCar professionalCar = new ProfessionalPersonalCar(
            "Suzuki Swift",
            500.0, 
            "XY12345", 
            2012, 
            10000M, 
            10.0, 
            20.0, 
            Vehicle.FuelTypeEnum.Benzin, 
            2,
            new PersonalCar.TrunkDimentionsStruct(14.0, 10.0, 16.0), 
            true, 
            400.0);
        #endregion
        #region Init User objects
        User user1 = new PrivateUser("lkri", "password1", 9000, 0000000000);
        User user2 = new CorporateUser("fros", "password2", 9400, 99999999, 0M);
        #endregion
        [Fact]
        public void TestSetForSale()
        {
            const decimal minPrice = 10000M;
            uint ID = AuctionHouse.SetForSale(privateCar, user1, minPrice);
            Assert.NotNull(AuctionHouse.FindAuctionByID(ID));
        }
        [Fact]
        public void TestSetForSaleWithNodification()
        {
            const decimal minPrice = 20000M;
            uint ID = AuctionHouse.SetForSale(privateCar, user1, minPrice, new NodificationDelegate((string x) => "Delegate Test, " + x));
            Assert.NotNull(AuctionHouse.FindAuctionByID(ID));
        }
        [Fact]
        public void TestRecieveBid()
        {
            uint ID = (uint)AuctionHouse.Auctions.Count;
            const decimal bid = 2000M;
            const decimal price = 1000M;
            AuctionHouse.Auctions.Add(new Auction(privateCar, user1, price));
            Assert.False(AuctionHouse.RecieveBid(user2, ID, 2000000M));
            Assert.True(AuctionHouse.RecieveBid(user2, ID, bid));
        }
        [Fact]
        public void TestAcceptBid()
        {
            uint ID = (uint)AuctionHouse.Auctions.Count;
            const decimal price = 1000M;
            AuctionHouse.Auctions.Add(new Auction(privateCar, user1, price));
            Assert.False(AuctionHouse.AcceptBid(user1, ID)); // There is no buyer on the auction
            AuctionHouse.RecieveBid(user2, ID, 3000M);
            Assert.False(AuctionHouse.AcceptBid(user2, ID)); // The wrong seller is trying to accept the bid
            user2.Balance = 0M;
            Assert.False(AuctionHouse.AcceptBid(user1, ID)); // Not enough money or credit
            user2.Balance = 4000M;
            Assert.True(AuctionHouse.AcceptBid(user1, ID)); // Can user2 pay without credit
        }
        [Fact]
        public void TestFindByID()
        {
            const decimal price = 1000M;
            Auction auction = new Auction(privateCar, user1, price);
            AuctionHouse.Auctions.Add(auction);
            uint ID = auction.ID;
            Assert.NotNull(AuctionHouse.FindAuctionByID(ID).Result);
            Assert.Null(AuctionHouse.FindAuctionByID(ID+1).Result);
        }
    }
}
