using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace EloPayPal.AdaptivePayments
{
	public class ChainedPayment : Payment
	{
		private IList<PayPalReceiver> receiverList;
		public void AddReceiver(PayPalReceiver receiver)
		{
			if (receiver.primary == null)
				throw new InvalidOperationException("Whether the receiver is a primary receiver or not must be specified in chained payments");

			if (receiver.primary.Value && receiverList.Any(x => x.primary.Value))
				throw new InvalidOperationException("There already is a primary receiver for this payment!");

			receiverList.Add(receiver);
		}
		private IEnumerable<object> GetReceiverSerializableList() {
			return receiverList.Select(x => x.GetSerializableObject());
		}

		public FeesPayer FeesPayer { get; private set; }
		public void SetFeesPayer(FeesPayer feesPayer) {
			this.FeesPayer = feesPayer;
		}

		protected override object GetPaymentObject()
		{
			return new {
				actionType = "PAY",
				currencyCode = PaymentConfiguration.Currency,
				feesPayer = FeesPayer.ToInstructionString(),
				receiverList = new {
					receiver = GetReceiverSerializableList()
				},
				ipnNotificationUrl = PaymentConfiguration.IPNNotificationUrl,
				returnUrl = PaymentSuccessUrl ?? PaymentConfiguration.PaymentSuccessUrl,
				cancelUrl = PaymentErrorUrl ?? PaymentConfiguration.PaymentErrorUrl,
				requestEnvelope = new {
					errorLanguage = "en_US", // Only en_US is supported
					detailLevel = "ReturnAll"
				},
				payKeyDuration = "PT30M"
			};
		}

		public ChainedPayment(PayPalConfiguration conf)
		{
			this.PaymentConfiguration = conf;
			this.Phase = PaymentPhase.NothingDone;
			this.receiverList = new List<PayPalReceiver>(2);
			SetFeesPayer(FeesPayer.PrimaryReceiver);
		}
		
		public ChainedPayment() : this(Configuration.Current) { }
	}
}
