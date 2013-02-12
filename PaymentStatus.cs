using System;

namespace Elopayments
{
	public enum PaymentStatus
	{
		/// <summary>
		/// The payment request was received; funds will be transferred once the payment is approved.
		/// </summary>
		Created,

		/// <summary>
		/// The payment was successful
		/// </summary>
		Completed,

		/// <summary>
		/// Some transfers succeeded and some failed for a parallel payment or, for a delayed chained payment, secondary receivers have not been paid.
		/// </summary>
		Incomplete,

		/// <summary>
		/// The payment failed and all attempted transfers failed or all completed transfers were successfully reversed.
		/// </summary>
		Error,

		/// <summary>
		/// One or more transfers failed when attempting to reverse a payment.
		/// </summary>
		ReversalError,

		/// <summary>
		/// The payment is in progress.
		/// </summary>
		Processing,

		/// <summary>
		/// The payment is awaiting processing.
		/// </summary>
		Pending,

		Refunded
	}
}
