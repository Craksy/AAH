using System;
using System.Collections.Generic;
using System.Text;
using AutoAuctionProjekt.Classes;

namespace AutoAuctionProjekt
{
    public interface ISeller
    {
        /// <summary>
        /// UserName proberty
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Balance proberty
        /// </summary>
        decimal Balance { get; set; }
        /// <summary>
        /// Zipcode proberty
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
