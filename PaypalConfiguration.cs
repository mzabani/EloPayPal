using System;
using System.Collections.Generic;
using SslPolicyErrors = System.Net.Security.SslPolicyErrors;
using X509Chain = System.Security.Cryptography.X509Certificates.X509Chain;
using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;

namespace EloPayPal
{
	public class PayPalConfiguration
	{
		public string APICallerEmail { get; private set; }

		public string PaymentErrorUrl { get; private set; }
		public string PaymentSuccessUrl { get; private set; }
		public string FinishPaymentUrlFormat { get; private set; }
		public string IPNReceivedUrl { get; private set; }
        public string IPNNotificationUrl { get; private set; }

		public string Currency { get; private set; }

		public string OperationPayEndpoint { get; private set; }
		public string OperationExecutePaymentEndpoint { get; private set; }

		// API Caller Data
		public string UserId { get; private set; }
		public string Password { get; private set; }
		public string Signature { get; private set; }
		public string ApplicationId { get; private set; }

        public string GetFinishPaymentUrl(string payKey)
        {
            return string.Format(FinishPaymentUrlFormat, payKey);
        }

		public bool CertificateValidator (object sender, X509Certificate certificate, X509Chain chain, 
		                                         SslPolicyErrors sslPolicyErrors) {
			return true;
		}

		public PayPalConfiguration(string APICallerEmail,
								   string PaymentErrorUrl, string PaymentSuccessUrl,
		                           string FinishPaymentUrlFormat, string IPNReceivedUrl, string Currency,
		                           string OperationPayEndpoint, string OperationExecutePaymentEndpoint,
                                   string IPNNotificationUrl,
		                           string UserId, string Password, string Signature, string ApplicationId) {
			this.APICallerEmail = APICallerEmail;

			this.PaymentErrorUrl = PaymentErrorUrl;
			this.PaymentSuccessUrl = PaymentSuccessUrl;
			this.FinishPaymentUrlFormat = FinishPaymentUrlFormat;
			this.IPNReceivedUrl = IPNReceivedUrl;
            this.IPNNotificationUrl = IPNNotificationUrl;

			this.Currency = Currency;

			this.OperationPayEndpoint = OperationPayEndpoint;
			this.OperationExecutePaymentEndpoint = OperationExecutePaymentEndpoint;

			this.UserId = UserId;
			this.Password = Password;
			this.Signature = Signature;
			this.ApplicationId = ApplicationId;
		}
	}
}
