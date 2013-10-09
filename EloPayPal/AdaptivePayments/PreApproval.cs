using System;
using System.Collections.Generic;
using EloPayPal;

namespace EloPayPal.Adaptive
{
	public class PreApproval : Request<PreApprovalResponse>
	{
		private DateTime? endingDate = null;
		public void SetEndingDate(DateTime endingDate)
		{
			if (endingDate.Kind != DateTimeKind.Local && endingDate.Kind != DateTimeKind.Utc)
			{
				throw new ArgumentException("The DateTime has to be of kind Local or Utc. The kind of the passed date is Unspecified");
			}

			this.endingDate = endingDate;
		}

		private DateTime? startingDate = null;
		public void SetStartingDate(DateTime startingDate)
		{
			if (startingDate.Kind != DateTimeKind.Local && startingDate.Kind != DateTimeKind.Utc)
			{
				throw new ArgumentException("The DateTime has to be of kind Local or Utc. The kind of the passed date is Unspecified");
			}
			
			this.startingDate = startingDate;
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

		private decimal maxTotalAmountOfAllPayments;
		public decimal MaxTotalAmountOfAllPayments {
			get {
				return maxTotalAmountOfAllPayments;
			}
			set {
				if (value <= 0)
					throw new ArgumentOutOfRangeException("You must provide a positive value");
				
				maxTotalAmountOfAllPayments = value;
			}
		}

		public int MaxNumberOfPayments = 0;

		protected override IDictionary<string, object> GetRequestObject()
		{
			var obj = new Dictionary<string, object>() {
				{ "startingDate", startingDate.Value.ToIsoDateAndTimeString() }
			};

			if (endingDate != null)
				obj.Add("endingDate", endingDate.Value.ToIsoDateAndTimeString());

			if (maxAmountPerPayment != 0)
				obj.Add("maxAmountPerPayment", maxAmountPerPayment);

			if (maxTotalAmountOfAllPayments != 0)
				obj.Add("maxTotalAmountOfAllPayments", maxTotalAmountOfAllPayments);

			if (MaxNumberOfPayments != 0)
				obj.Add("maxNumberOfPayments", MaxNumberOfPayments);

			return obj;
		}

		public override RequestAck Execute(out PreApprovalResponse response)
		{
			if (startingDate == null)
				throw new InvalidOperationException("You have to define a starting date first");

			return Execute (RequestConfiguration.OperationPreApprovalEndpoint, out response);
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
