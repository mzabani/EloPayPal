using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace EloPayPal.Adaptive
{
	public class ChainedPayment : PayRequest
	{
		private IList<PayPalReceiver> receiverList;
		public void AddReceiver(PayPalReceiver receiver)
		{
			if (receiver.Primary == null)
				throw new InvalidOperationException("Whether the receiver is a primary receiver or not must be specified in chained payments");

			if (receiver.Primary.Value && receiverList.Any(x => x.Primary.Value))
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

		protected override IDictionary<string, object> GetRequestObject()
		{
			var obj = new Dictionary<string, object>() {
				{ "actionType", "PAY" },
				{ "feesPayer", FeesPayer.ToInstructionString() },
				{ "receiverList", new { 
						receiver = GetReceiverSerializableList()
					}
				}
			};
			
			if (TrackingId != null)
				obj.Add("trackingID", TrackingId);
			
			if (_PayKeyDuration != 0)
				obj.Add("payKeyDuration", "PT" + _PayKeyDuration + "M");
			
			return obj;
		}

		public override RequestAck Execute (out PayResponse response)
		{
			// There must be a primary receiver and at least one secondary receiver
			if (receiverList.Any (r => r.Primary == true) == false)
				throw new InvalidOperationException("You have to add a primary receiver to execute a chained payment");
			if (receiverList.Any (r => r.Primary == false) == false)
				throw new InvalidOperationException("You have to add at least one secondary receiver to execute a chained payment");

			// Checks for total amount for the primary receiver, which must be >= sum of all other receivers' amounts
			decimal primaryReceiverAmount = receiverList.First(r => r.Primary == true).Amount;
			decimal otherReceiversAmount = receiverList.Where(r => r.Primary == false).Sum (r => r.Amount);
			if (primaryReceiverAmount < otherReceiversAmount)
				throw new InvalidOperationException("The primary receiver must receive more than all other receivers together");

			return base.Execute (out response);
		}

		public ChainedPayment(PayPalConfiguration conf)
			: base(conf)
		{
			this.receiverList = new List<PayPalReceiver>(2);
			SetFeesPayer(FeesPayer.PrimaryReceiver);
		}
		
		public ChainedPayment()
			: this(Configuration.Current)
		{
		}
	}
}
