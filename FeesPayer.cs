using System;

namespace Elopayments.PayPal
{
	public enum FeesPayer
	{
		/// <summary>
		/// Sender pays all fees (for personal, implicit simple/parallel payments; do not use for chained or unilateral payments).
		/// </summary>
		Sender,

		/// <summary>
		/// Primary receiver pays all fees (chained payments only).
		/// </summary>
		PrimaryReceiver,

		/// <summary>
		/// Each receiver pays their own fee (default, personal and unilateral payments).
		/// </summary>
		EachReceiver,

		/// <summary>
		/// Secondary receivers pay all fees (use only for chained payments with one secondary receiver).
		/// </summary>
		SecondaryOnly
	}

	internal static class FeesPayerExtensionMethods {
		public static string ToInstructionString(this FeesPayer fp) {
			if (fp == FeesPayer.Sender)
				return "SENDER";
			else if (fp == FeesPayer.PrimaryReceiver)
				return "PRIMARYRECEIVER";
			else if (fp == FeesPayer.SecondaryOnly)
				return "SECONDARYONLY";
			else if (fp == FeesPayer.EachReceiver)
				return "EACHRECEIVER";

			else
				throw new Exception("Invalid FeesPayer enum!");
		}
	}
}

