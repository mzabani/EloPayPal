using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EloPayPal.Adaptive
{
    public class UserInfoType
    {
        /// <summary>
        /// The type of account. Allowable values are: Personal – Personal account, Premier – Premier account, Business – Business account.
        /// </summary>
        public string accountType { get; set; }

        /// <summary>
        /// Business name of the PayPal account holder.
        /// </summary>
        public string businessName { get; set; }

        /// <summary>
        /// Identifies the PayPal account.
        /// </summary>
        public string accountId { get; set; }

        /// <summary>
        /// Email address associated with the PayPal account: one of the unique identifiers of the account.
        /// </summary>
        public string emailAddress { get; set; }
    }
}
