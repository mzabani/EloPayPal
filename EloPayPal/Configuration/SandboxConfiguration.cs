using System;

namespace EloPayPal
{
	public class SandboxConfiguration : PayPalConfiguration
	{
		public SandboxConfiguration(string APICallerEmail,
                                       string PaymentErrorUrl, string PaymentSuccessUrl,
								       string Currency,
                                       string IPNNotificationUrl,
		                               string UserId, string Password, string Signature)
			: base(APICallerEmail, PaymentErrorUrl, PaymentSuccessUrl, "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_ap-payment&paykey={0}", "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_notify-validate",
			       Currency, "https://svcs.sandbox.paypal.com/AdaptivePayments/Pay", "https://svcs.sandbox.paypal.com/AdaptivePayments/Preapproval", "https://svcs.sandbox.paypal.com/AdaptivePayments/ExecutePayment", "https://svcs.sandbox.paypal.com/AdaptiveAccounts/GetVerifiedStatus",
			       IPNNotificationUrl, UserId, Password, Signature, "APP-80W284485P519543T")
        { }
	}
}
