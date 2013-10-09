using System;

namespace EloPayPal.Adaptive
{
	/// <summary>
	/// Response envelope. It must stored for every response, since it contains necessary information for support.
	/// </summary>
	public class ResponseEnvelope
	{
		/// <summary>
		/// Date on which the response was sent, must be logged/stored for every response.
		/// </summary>
		public DateTime timestamp { get; set; }
 	
		/// <summary>
		/// The ack string, can be Success, Failure, SuccessWithWarning or FailureWithWarning.
		/// </summary>
		public string ack { get; set; }

		/// <summary>
		/// Correlation identifier. It is a 13-character, alphanumeric string (for example, db87c705a910e) that is used only by PayPal Merchant Technical Support.
		/// You must log and store this data for every response you receive. PayPal Technical Support uses the information to assist with reported issues.
		/// </summary>
		public string correlationId { get; set; }

		/// <summary>
		/// Build number. Must store it for every response as well in case you need it for support.
		/// </summary>
		public string build { get; set; }
	}
}