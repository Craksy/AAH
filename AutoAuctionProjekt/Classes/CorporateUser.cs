using System;

namespace AutoAuctionProjekt.Classes
{
    public class CorporateUser : User
    {
        public CorporateUser(string userName, string password, uint zipCode, uint cvrNummer, decimal credit) : base(userName, password, zipCode)
        {
            //TODO: U7 - Set constructor
            //TODO: U8 - Add to database and set ID
            CVRNumber = cvrNummer;
            Credit = credit;
        }
        public uint CVRNumber { get; set; }
        public decimal Credit { get; set; }
    }
}
