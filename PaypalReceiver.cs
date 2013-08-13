using System;

namespace EloPayPal
{
	public class PayPalReceiver
	{
		public string email { get; private set; }
		public decimal amount { get; private set; }

		/// <summary>
		/// Whether this is a primary receiver or not. If null, this receiver can't be used in chained payments. If not null, this
		/// receiver can't be used in simple payments.
		/// </summary>
		public bool? primary { get; private set; }

		internal object GetSerializableObject() {
			if (primary == null)
			{
				return new {
					email, amount
				};
			}
			else
			{
				return new {
					email, amount,
					primary = primary.Value ? "true" : "false"
				};
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Elopayments.PayPalReceiver"/> class.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="amount">Amount.</param>
		/// <param name="primary"> Whether this is a primary receiver or not. If null, this receiver can't be used in chained payments. If not null, this receiver can't be used in simple payments.</param>
		public PayPalReceiver(string email, decimal amount, bool? primary) {
			this.email = email;
			this.amount = amount;
			this.primary = primary;
		}
	}
}
