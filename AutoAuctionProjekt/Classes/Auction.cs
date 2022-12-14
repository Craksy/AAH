using System;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt.Classes
{
    public class Auction
    {
        /// <summary>
        /// Constructor for Auction
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="seller"></param>
        /// <param name="minimumPrice"></param>
        
        private Vehicle _vehicle;
        private ISeller _seller;
        private decimal _minimumPrice;
        public Auction(Vehicle vehicle, ISeller seller, decimal minimumPrice)
        {
            Vehicle = vehicle;
            Seller = seller;
            MinimumPrice = minimumPrice;
        }

        public Auction(uint id) {
            //TODO: Constructor for loading from database
            //TODO: A1 - Set constructor
            //TODO: A2 - Add to database and set ID
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// ID of the auction
        /// </summary>
        public uint ID { get; init; }
        
        /// <summary>
        /// The minimum price of the auction
        /// </summary>
        public decimal MinimumPrice { get; set; }
        
        /// <summary>
        /// The standing bid of the auction
        /// </summary>
        public decimal StandingBid { get; set; }
        
        /// <summary>
        /// The vehicle of the auction
        /// </summary>
        public Vehicle Vehicle { get; set; }
        
        /// <summary>
        /// The seller of the auction
        /// </summary>
        public ISeller Seller { get; set; }
        
        /// <summary>
        /// The buyer or potential buyer of the auction
        /// </summary>
        public IBuyer Buyer { get; set; }
        
        /// <summary>
        /// The date the auction was created
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"Auction for {Vehicle} by {Seller}, starting at {MinimumPrice}";
    }
}