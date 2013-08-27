using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EloPayPal.Adaptive
{
    public class ErrorData
    {
        /// <summary>
        /// A 6-digit number that uniquely identifies a particular error.
        /// </summary>
        public long errorId { get; set; }

        /// <summary>
        /// The location where the error occurred. Possible values are System, Application and Request.
        /// </summary>
        public string category { get; set; }
        
        /// <summary>
        /// The domain to which this service belongs.
        /// </summary>
        public string domain { get; set; }

        public string subdomain { get; set; }

        public string severity { get; set; }

        /// <summary>
        /// A description of the error.
        /// </summary>
        public string message { get; set; }
    }
}
