using System;
using System.Collections.Generic;

namespace EloPayPal.Adaptive
{
	public class GetVerifiedStatusResponse : IAdaptiveResponse
	{
		public ResponseEnvelope responseEnvelope { get; set; }

        /// <summary>
        /// This field is set to VERIFIED or UNVERIFIED to indicate the account status.
        /// </summary>
		public string accountStatus { get; set; }

		public UserInfoType userInfo { get; set; }

        public IList<ErrorData> error { get; set; }
	}
}

