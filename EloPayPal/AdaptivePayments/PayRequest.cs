using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace EloPayPal.Adaptive
{
	public abstract class PayRequest : Request<PayResponse>
	{
		public PayRequest (PayPalConfiguration conf)
			: base(conf)
		{
		}

		public PayRequest ()
			: base()
		{
		}

		public string TrackingId { get; set; }

		protected int _PayKeyDuration;

		/// <summary>
		/// Sets or gets the PayKey's duration in minutes. Must be greater than 5 and less than 30 days.
		/// </summary>
		/// <value>The duration of the PayKey in minutes.</value>
		public int PayKeyDuration {
			get
			{
				if (_PayKeyDuration == 0)
					throw new InvalidOperationException("PayKey's duration has not been set yet");

				return _PayKeyDuration;
			}
			set
			{
				if (value < 5 || value > 30 * 24 * 60)
					throw new ArgumentOutOfRangeException("value", "Duration must be between 5 minutes and 30 days");

				_PayKeyDuration = value;
			}
		}
	
		public override RequestAck Execute (out PayResponse response)
		{
			return Execute(RequestConfiguration.OperationPayEndpoint, out response);
		}
	}
}
