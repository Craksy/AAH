﻿namespace AutoAuctionProjekt
{
    public interface IBuyer
    {
        /// <summary>
        /// UserName property
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Balance property
        /// </summary>
        decimal Balance { get; set; }
    }
}
