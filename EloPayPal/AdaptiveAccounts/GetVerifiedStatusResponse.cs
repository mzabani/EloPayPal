using System;
using System.Collections.Generic;

namespace EloPayPal.Adaptive
{
	public class GetVerifiedStatusResponse
	{
		public ResponseEnvelope responseEnvelope { get; set; }

        /// <summary>
        /// This field is set to VERIFIED or UNVERIFIED to indicate the account status.
        /// </summary>
		public string accountStatus { get; set; }

        public NameType name { get; set; }

        public IList<ErrorData> error { get; set; }
	}
}

