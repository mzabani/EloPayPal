using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace EloPayPal.Adaptive
{
	public class PreApproval : Request<PreApprovalResponse>
	{
		private DateTime? endingDate;
		public DateTime? EndingDate {
			get {
				return endingDate;
			}
			set {
				endingDate = value;
			}
		}

		private DateTime startingDate;
		public DateTime StartingDate {
			get {
				return startingDate;
			}
			set {
				startingDate = value;
			}
		}

		private decimal maxAmountPerPayment;
		public decimal MaxAmountPerPayment {
			get {
				return maxAmountPerPayment;
			}
			set {
				if (value <= 0)
					throw new ArgumentOutOfRangeException("You must provide a positive value");

				maxAmountPerPayment = value;
			}
		}

		public int MaxNumberOfPayments = 0;

		protected override IDictionary<string, object> GetRequestObject()
		{
			var obj = new Dictionary<string, object>() {
				{ "actionType", "PAY" },
				{ "startingDate", startingDate }
			};

			if (endingDate != null)
				obj.Add("endingDate", endingDate.Value);

			if (maxAmountPerPayment != 0)
				obj.Add("maxAmountPerPayment", maxAmountPerPayment);

			return obj;
		}

		public override RequestAck Execute (out PreApprovalResponse response)
		{
			return Execute(RequestConfiguration.OperationPreApprovalEndpoint, out response);
		}
		
		public PreApproval(PayPalConfiguration conf)
			: base(conf)
		{
		}
		
		public PreApproval() : this(Configuration.Current)
		{
		}
	}
}
