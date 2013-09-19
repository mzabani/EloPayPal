using System;

namespace EloPayPal
{
	public class IPNTransaction
	{
		public string id { get; set; }
		public string id_for_sender { get; set; }

		public string status { get; set; }
		public string pending_reason { get; set; }
		public string paymentType { get; set; }

		public string status_for_sender_txn { get; set; }

		public string refund_id { get; set; }
		public string refund_amount { get; set; }
		public string refund_account_charged { get; set; }
		public string receiver { get; set; }
		public string invoiceId { get; set; }
		public string amount { get; set; }
		public bool? is_primary_receiver { get; set; }
	}
}

