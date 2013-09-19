using System;

namespace EloPayPal
{
	public class FeesCalculator
	{
		#region Rates
		private decimal? PayPalProportionalRate;
		private decimal? PayPalFixedRate;
		public void SetRates(decimal paypalProportionalRate, decimal paypalFixedRate)
		{
			PayPalProportionalRate = paypalProportionalRate;
			PayPalFixedRate = paypalFixedRate;
		}
		#endregion

		#region Transaction value
		private decimal transactionValue;
		public decimal TransactionValue {
			get
			{
				return transactionValue;
			}
		}
		public void SetTransactionValue(decimal value) {
			if (value <= 0)
				throw new ArgumentOutOfRangeException("val", value, "The transaction value has to be greater than zero");

			transactionValue = value;
		}
		#endregion

		/// <summary>
		/// The value of the transaction minus PayPal's fees.
		/// </summary>
		public decimal GetValueWithoutFees() {
			return TransactionValue - GetFeesValue();
		}

		public decimal GetTransactionValueFromValueWithoutFees(decimal valueWithoutTaxes) {
			if (PayPalProportionalRate == null || PayPalFixedRate == null)
				throw new InvalidOperationException("Taxes must be defined for this operation");

			decimal attemptValue = MoneyUtils.RoundCentsDown((valueWithoutTaxes + PayPalFixedRate.Value) / (1 - PayPalProportionalRate.Value));

			// Just to be certain
			attemptValue -= .01M;

			while (attemptValue - GetFeesValue(attemptValue) != valueWithoutTaxes)
				attemptValue += .01M;

			return attemptValue;
		}

		private decimal GetFeesValue(decimal transactionValue) {
			return MoneyUtils.RoundCentsHalfUp(transactionValue * PayPalProportionalRate.Value) + PayPalFixedRate.Value;
		}

		/// <summary>
		/// The charges that will be deducted by PayPal for this transaction.
		/// </summary>
		public decimal GetFeesValue() {
			if (PayPalProportionalRate == null || PayPalFixedRate == null)
				throw new InvalidOperationException("Taxes must be defined for this operation");

			return GetFeesValue(TransactionValue);
		}
	}
}
