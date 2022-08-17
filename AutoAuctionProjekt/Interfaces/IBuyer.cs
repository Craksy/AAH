using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt
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
