using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AutoAuctionProjekt.Classes
{
    
public enum UserType
{
    PrivateUser,
    CorporateUser
}

/*
 * Domæne model
interface polymorfi via interface
interface til at kunne køde og sælge til

køber og sælger som interfaces

privat og company som klasser
 */

    /// <summary>
    /// The base User class. Implements the ISeller and IBuyer interfaces,
    /// and holds properties common to both <see cref="PrivateUser"/> and <see cref="CorporateUser"/>.
    /// </summary>
    public class User : ISeller, IBuyer
    {
        public User(string userName, string zipCode, decimal balance = 0)
        {
            UserName = userName;
            Zipcode = zipCode;
            Balance = balance;
        }

        public string ReceiveBidNodification(string message)
        {
            //TODO: This method could be used in the frontend for a notification area.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Users name
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Users account balance
        /// </summary>
        public Decimal Balance { get; set; }
        
        /// <summary>
        /// Zip code of the users address
        /// </summary>
        public string Zipcode { get; set; }
        
        /// <summary>
        /// ID property
        /// </summary>
        public int ID { get; init; }
        
        /// <summary>
        /// PasswordHash property
        /// </summary>
        private byte[] PasswordHash { get; set; }
        
        
        /// <summary>
        /// A method that ...
        /// </summary>
        /// <returns>Whether login is valid</returns>
        private bool ValidateLogin(string loginUserName, string loginPassword)
        {
            //TODO: U5 - Implement the rest of validation for password and user name

            HashAlgorithm sha = SHA256.Create(); //Make a HashAlgorithm object for makeing hash computations.
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(loginPassword)); //Encodes the password into a hash in a Byte array.

            return PasswordHash == result;

            throw new NotImplementedException();
        }

        //TODO: U4 - Implement interface proberties and methods.

        /// <summary>
        /// Returns the User in a string with relivant information.
        /// </summary>
        /// <returns>...</returns>
        public override string ToString() => $"User: {UserName} - Balance: {Balance} - Zipcode: {Zipcode}";
    }
}
