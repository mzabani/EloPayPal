using System;
using System.Collections.Generic;

namespace EloPayPal
{
	public class SellerIPN
	{
		#region Transaction information
		/// <summary>
		/// This notification's id. Use this field to avoid double-processing of IPNs.
		/// </summary>
		/// <value>The txn_id.</value>
		public string txn_id { get; set; }

		public string trackingId { get; set; }
		public string transaction_subject { get; set; }
		public string txn_type { get; set; }
		public string ipn_track_id { get; set; }
		#endregion

		#region Information about the receiver
		public string business { get; set; }
		public string receiver_email { get; set; }
		public string receiver_id { get; set; }

		/// <summary>
		/// The seller's residence country.
		/// </summary>
		public string residence_country { get; set; }
		#endregion

		#region Buyer information
		public string payer_email { get; set; }
		public string payer_id { get; set; }
		public string payer_status { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string address_city { get; set; }
		public string address_country { get; set; }
		public string address_country_code { get; set; }
		public string address_name { get; set; }
		public string address_state { get; set; }
		public string address_status { get; set; }
		public string address_street { get; set; }
		public string address_zip { get; set; }
		#endregion

		#region Information about the payment
		public string custom { get; set; }
		public decimal? handling_amount { get; set; }
		public string item_name { get; set; }
		public string item_number { get; set; }
		public string mc_currency { get; set; }
		public decimal mc_fee { get; set; }
		public decimal mc_gross { get; set; }

		public string payment_date { get; set; }
		public decimal? payment_fee { get; set; }
		public decimal? payment_gross { get; set; }
		public string payment_status { get; set; }
		public string payment_type { get; set; }
		public string protection_eligibility { get; set; }
		public int? quantity { get; set; }
		public decimal? shipping { get; set; }
		#endregion

		public string notify_version { get; set; }
		public string charset { get; set; }
		public string verify_sign { get; set; }
	}
}
