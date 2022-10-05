using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt.Classes
{
    public class PrivateUser : User
    {
        public PrivateUser(string userName, string password, uint zipCode, uint cprNummer) 
            : base(userName, password, zipCode)
        {
            //TODO: U10 - Set constructor
            //TODO: U11 - Add to database and set ID
            CPRNumber = cprNummer;
            
        }
        public uint CPRNumber { get; set; }
    }
}
