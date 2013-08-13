using System;

namespace EloPayPal
{
	public static class ElomenoReceiver
	{
		public static PayPalReceiver GetElomenoReceiver(decimal amount, bool? primary) {
			return new PayPalReceiver(Configuration.Current.APICallerEmail, amount, primary);
		}
	}
}
