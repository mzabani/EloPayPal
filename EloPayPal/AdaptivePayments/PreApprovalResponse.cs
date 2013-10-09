using System;
using System.Collections.Generic;

namespace EloPayPal.Adaptive
{
	public class PreApprovalResponse : IAdaptiveResponse
	{
		public ResponseEnvelope responseEnvelope { get; set; }

		public string preapprovalKey { get; set; }

		public string paymentExecStatus { get; set; }

        public IList<ErrorData> error { get; set; }
	}
}