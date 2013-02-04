using System;

namespace Elopayments.PayPal
{
	public static class ElomenoReceiver
	{
		public static PaypalReceiver GetElomenoReceiver(decimal amount, bool primary) {
			return new PaypalReceiver(Configuration.Current.APICallerEmail, amount, primary);
		}
	}
}
