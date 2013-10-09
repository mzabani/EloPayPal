using System;

namespace EloPayPal
{
	public class ProductionConfiguration : PayPalConfiguration
	{
		public ProductionConfiguration(string APICallerEmail,
                                       string PaymentErrorUrl, string PaymentSuccessUrl,
								       string Currency,
                                       string IPNNotificationUrl,
		                               string UserId, string Password, string Signature, string ApplicationId)
            : base(APICallerEmail, PaymentErrorUrl, PaymentSuccessUrl, "https://www.paypal.com/cgi-bin/webscr?cmd=_ap-payment&paykey={0}", "https://ipnpb.paypal.com/cgi-bin/webscr?cmd=_notify-validate",
			       Currency, "https://svcs.paypal.com/AdaptivePayments/Pay", "https://svcs.paypal.com/AdaptivePayments/Preapproval", "https://svcs.paypal.com/AdaptivePayments/ExecutePayment", "https://svcs.paypal.com/AdaptiveAccounts/GetVerifiedStatus",
            IPNNotificationUrl, UserId, Password, Signature, ApplicationId)
        { }
	}
}
