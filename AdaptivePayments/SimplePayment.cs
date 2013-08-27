using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace EloPayPal.Adaptive
{
	public class SimplePayment : PayRequest
	{
		private PayPalReceiver Receiver;
		public void SetReceiver(PayPalReceiver receiver)
		{
			if (receiver.primary != null)
				throw new InvalidOperationException("A simple payment's receiver must not be primary or secondary, i.e. it has to have primary == null");

			if (Receiver != null)
				throw new InvalidOperationException("A receiver for this payment has already been specified.");

			Receiver = receiver;
		}

		protected override object GetRequestObject()
		{
			var obj = new Dictionary<string, object>() {
				{ "actionType", "PAY" },
				{ "currencyCode", PaymentConfiguration.Currency },
				{ "receiverList", new { receiver = new[] { Receiver } } },
				{ "ipnNotificationUrl", PaymentConfiguration.IPNNotificationUrl },
				{ "returnUrl", PaymentSuccessUrl ?? PaymentConfiguration.PaymentSuccessUrl },
				{ "cancelUrl", PaymentErrorUrl ?? PaymentConfiguration.PaymentErrorUrl },
				{ "requestEnvelope", new {
						errorLanguage = "en_US",
						detailLevel = "ReturnAll"
					}
				}
			};

			if (TrackingId != null)
				obj.Add("trackingID", TrackingId);

			if (_PayKeyDuration != 0)
				obj.Add("payKeyDuration", "PT" + _PayKeyDuration + "M");

			return obj;
		}
		
		public SimplePayment(PayPalConfiguration conf)
		{
			this.PaymentConfiguration = conf;
			this.Phase = PaymentPhase.NothingDone;
		}
		
		public SimplePayment() : this(Configuration.Current) { }
	}
}
