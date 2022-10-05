using System;

namespace AutoAuctionProjekt.Classes
{
    
    /// <summary>
    /// Corporate user class. Inherits from <see cref="User"/>
    /// and additionally contain a CVR number and credit score.
    /// </summary>
    public class CorporateUser : User
    {
        public CorporateUser(string userName,  uint zipCode, decimal balance, string cvrNummer, decimal credit) 
            : base(userName, zipCode, balance)
        {
            CVRNumber = cvrNummer;
            Credit = credit;
        }
        public string CVRNumber { get; set; }
        public decimal Credit { get; set; }
    }
}
