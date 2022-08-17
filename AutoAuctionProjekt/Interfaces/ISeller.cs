using System;
using System.Collections.Generic;
using System.Text;
using AutoAuctionProjekt.Classes;

namespace AutoAuctionProjekt
{
    public interface ISeller
    {
        /// <summary>
        /// UserName property
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Balance property
        /// </summary>
        decimal Balance { get; set; }
        /// <summary>
        /// Zipcode property
        /// </summary>
        uint Zipcode { get; set; }
        /// <summary>
        /// Receives a message for the user
        /// </summary>
        /// <param name="message"></param>
        /// <returns> The message </returns>
        string ReceiveBidNodification(string message);
    }
}
