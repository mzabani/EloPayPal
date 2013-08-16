using System;
using System.Collections.Generic;

namespace EloPayPal.AdaptivePayments
{
	public class PayResponse
	{
		public ResponseEnvelope responseEnvelope { get; set; }

		public string payKey { get; set; }

		public string paymentExecStatus { get; set; }

        public IList<ErrorData> error { get; set; }
	}
}

