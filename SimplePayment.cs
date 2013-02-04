using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Elopayments;

namespace Elopayments.PayPal
{
	public class SimplePayment : Payment
	{
		private PaypalReceiver Receiver;
		public void SetReceiver(PaypalReceiver receiver)
		{
			if (receiver.primary != null)
				throw new InvalidOperationException("A simple payment's receiver must not be primary or secondary, i.e. it has to have primary == null");

			if (Receiver != null)
				throw new InvalidOperationException("A receiver for this payment has already been specified.");

			Receiver = receiver;
		}

		protected override object GetPaymentObject()
		{
			return new {
				actionType = "PAY",
				currencyCode = PaymentConfiguration.Currency,
				receiverList = new {
					receiver = new[] { Receiver }
				},
				ipnNotificationUrl = PaymentConfiguration.IPNNotificationUrl,
				returnUrl = PaymentConfiguration.PaymentSuccessUrl,
				cancelUrl = PaymentConfiguration.PaymentErrorUrl,
				requestEnvelope = new {
					errorLanguage = "en_US", // Only en_US is supported
					detailLevel = "ReturnAll"
				}
			};
		}
		
		public SimplePayment(PaypalConfiguration conf)
		{
			this.PaymentConfiguration = conf;
			this.Phase = PaymentPhase.NothingDone;
		}
		
		public SimplePayment() : this(Configuration.Current) { }
	}
}
