using System;
using System.Collections.Generic;
using SslPolicyErrors = System.Net.Security.SslPolicyErrors;
using X509Chain = System.Security.Cryptography.X509Certificates.X509Chain;
using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;

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
            Currency, "https://svcs.paypal.com/AdaptivePayments/Pay", "https://svcs.paypal.com/AdaptivePayments/ExecutePayment", "https://svcs.paypal.com/AdaptiveAccounts/GetVerifiedStatus",
            IPNNotificationUrl, UserId, Password, Signature, ApplicationId)
        { }
	}
}
