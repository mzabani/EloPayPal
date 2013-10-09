using System;
using System.Collections.Generic;

namespace EloPayPal
{
	public class PayIPN
	{
		/// <summary>
		/// If set, this is not a PayIPN, but probably a <see cref="SellerIPN"/>.
		/// </summary>
		public string txn_id { get; set; }

		/// <summary>
		/// If this IPN is related to a payment created in PayPal's Sandbox, this is equal to 1.
		/// </summary>
		public int? test_ipn { get; set; }

		public DateTime? payment_request_date { get; set; }
		public string return_url { get; set; }
		public string fees_payer { get; set; }
		public string ipn_notification_url { get; set; }
		public string cancel_url { get; set; }
		public string sender_email { get; set; }
		public string verify_sign { get; set; }
		public string pay_key { get; set; }
		public string action_type { get; set; }

		public string status { get; set; }
		public PaymentStatus GetStatus() {
			if (status == "CREATED")
				return PaymentStatus.Created;
			else if (status == "COMPLETED")
				return PaymentStatus.Completed;
			else if (status == "INCOMPLETE")
				return PaymentStatus.Incomplete;
			else if (status == "ERROR")
				return PaymentStatus.Error;
			else if (status == "REVERSALERROR")
				return PaymentStatus.ReversalError;
			else if (status == "PROCESSING")
				return PaymentStatus.Processing;
			else if (status == "PENDING")
				return PaymentStatus.Pending;
			
			throw new Exception("Unknown payment status " + status + " in IPN");
		}

		public string transaction_type { get; set; }
		public string memo { get; set; }
		public string trackingId { get; set; }
		public string preapproval_key { get; set; }
		public string reason_code { get; set; }

		public IList<IPNTransaction> transaction { get; set; }
		
		public string charset { get; set; }
		public string notify_version { get; set; }
		public bool? log_default_shipping_address_in_transaction { get; set; }
		public bool? reverse_all_parallel_payments_on_error { get; set; }
	}
}
