using System;

namespace Elopayments.PayPal
{
	public class IPNComplete
	{
		/// <summary>
		/// Check email address to make sure that this is not a spoof.
		/// </summary>
		public string receiver_email { get; set; }
		public string receiver_id { get; set; }
		public string residence_country { get; set; }

		public int test_ipn { get; set; }
		public string transaction_subject { get; set; }
		/// <summary>
		/// Keep this ID to avoid processing the transaction twice.
		/// </summary>
		public string txn_id { get; set; }
		/// <summary>
		/// Type of transaction.
		/// </summary>
		public string txn_type { get; set; }

		public string pay_key { get; set; }

		public string payer_email { get; set; }
		public string payer_id { get; set; }
		public string payer_status { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string address_city { get; set; }
		public string address_country { get; set; }
		public string address_country_code { get; set; }
		public string address_name { get; set; }
		public string address_status { get; set; }
		public string address_street { get; set; }
		public string address_zip { get; set; }

		/// <summary>
		/// Your custom field.
		/// </summary>
		public string custom { get; set; }
		public string handling_amount { get; set; }
		public string item_name { get; set; }
		public string item_number { get; set; }
		public string mc_currency { get; set; }
		public string mc_fee { get; set; }
		public string mc_gross { get; set; }
		public DateTime payment_date { get; set; }
		public string payment_fee { get; set; }
		public string payment_gross { get; set; }
		/// <summary>
		/// Status, which determines whether the transaction is complete.
		/// </summary>
		public string payment_status { get; set; }

		public PaymentStatus PaymentStatus {
			get
			{
				//TODO: Fix this
				return PaymentStatus.Completed;
			}
		}

		/// <summary>
		/// Kind of payment.
		/// </summary>
		public string payment_type { get; set; }
		public string protection_eligibility { get; set; }

		public int quantity { get; set; }
		public string shipping { get; set; }
		public string tax { get; set; }
	}
}
