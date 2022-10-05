using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AutoAuctionProjekt.Classes
{
/*
 * Domæne model
interface polymorfi via interface
interface til at kunne køde og sælge til

køber og sælger som interfaces

privat og company som klasser
 */

    public abstract class User : ISeller, IBuyer //TODO: U4 - Implement interfaces
    {
        
        protected User(string userName, string password, uint zipCode)
        {
            //TODO: U1 - Set constructor and field

            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            PasswordHash = result;

            UserName = userName;
            Zipcode = zipCode;
        }

        public string ReceiveBidNodification(string message)
        {
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
        public uint Zipcode { get; set; }
        
        /// <summary>
        /// ID property
        /// </summary>
        public uint ID { get; private set; }
        
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
        public override string ToString()
        {
            //TODO: U3 - ToString for User
            throw new NotImplementedException();
        }
    }
}
