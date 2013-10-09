using System;

namespace EloPayPal.Adaptive
{
	public class PayPalReceiver
	{
		public string Email { get; private set; }
		public decimal Amount { get; private set; }

		/// <summary>
		/// Whether this is a primary receiver or not. If null, this receiver can't be used in chained payments. If not null, this
		/// receiver can't be used in simple payments.
		/// </summary>
		public bool? Primary { get; private set; }

		internal object GetSerializableObject() {
			if (Primary == null)
			{
				return new {
					email = Email,
					amount = Amount
				};
			}
			else
			{
				return new {
					email = Email,
					amount = Amount,
					primary = Primary.Value ? "true" : "false"
				};
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EloPayPal.PayPalReceiver"/> class.
		/// </summary>
		/// <param name="email">The email of the PayPal account of this receiver.</param>
		/// <param name="amount">The amount this receiver will receive in a payment.</param>
		/// <param name="primary">Whether this is a primary receiver or not. If null, this receiver can't be used in chained payments. If not null, this receiver can't be used in simple payments.</param>
		public PayPalReceiver(string email, decimal amount, bool? primary) {
			this.Email = email;
			this.Amount = amount;
			this.Primary = primary;
		}
	}
}
