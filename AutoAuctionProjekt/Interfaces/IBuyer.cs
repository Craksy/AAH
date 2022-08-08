using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAuctionProjekt
{
    public interface IBuyer
    {
        /// <summary>
        /// UserName proberty
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Balance proberty
        /// </summary>
        decimal Balance { get; set; }
    }
}
