using System;

namespace Elopayments.PayPal
{
	public class PayResponse
	{
		public ResponseEnvelope responseEnvelope { get; set; }

		public string payKey { get; set; }

		public string paymentExecStatus { get; set; }
	}
}

