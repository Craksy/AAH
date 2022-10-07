namespace AutoAuctionProjekt.Classes
{
    public class PrivateUser : User
    {

        public PrivateUser(string userName, string zipCode, decimal balance, string cprNumber) 
            : base(userName, zipCode, balance)
        {
            CPRNumber = cprNumber;
        }
        
        //TODO: We might want to do some validation on this.
        public string CPRNumber { get; set; } 
    }
}
