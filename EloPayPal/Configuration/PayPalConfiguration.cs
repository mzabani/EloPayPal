using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
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
		public string OperationPreApprovalEndpoint { get; private set; }
		public string OperationExecutePaymentEndpoint { get; private set; }
        public string OperationGetVerifiedStatusEndpoint { get; private set; }

		// API Caller Data
		public string UserId { get; private set; }
		public string Password { get; private set; }
		public string Signature { get; private set; }
		public string ApplicationId { get; private set; }

        public string GetFinishPaymentUrl(string payKey)
        {
            return string.Format(FinishPaymentUrlFormat, payKey);
        }

		#region HTTP related methods
		/// <summary>
		/// Creates a POST HttpWebRequest with all the authentication headers and with <paramref name="data"/> as the to-be-posted content.
		/// </summary>
		/// <returns>
		/// The instantiated request.
		/// </returns>
		/// <param name='data'>
		/// The to-be-posted content.
		/// </param>
		internal virtual HttpWebRequest GetBasicHttpRequest(string endpointUrl, string data) {
			HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(endpointUrl);
			
			wr.Method = "POST";
			wr.ContentType = "application/x-www-form-urlencoded";
			
			wr.Headers.Add("X-PAYPAL-SECURITY-USERID", this.UserId);
			wr.Headers.Add("X-PAYPAL-SECURITY-PASSWORD", this.Password);
			wr.Headers.Add("X-PAYPAL-SECURITY-SIGNATURE", this.Signature);
			
			wr.Headers.Add("X-PAYPAL-APPLICATION-ID", this.ApplicationId);
			
			wr.Headers.Add("X-PAYPAL-REQUEST-DATA-FORMAT", "JSON");
			wr.Headers.Add("X-PAYPAL-RESPONSE-DATA-FORMAT", "JSON");
			
			using (Stream wrContentStream = wr.GetRequestStream())
			{
				using (StreamWriter wrContentWriter = new StreamWriter(wrContentStream))
				{
					wrContentWriter.Write(data);
				}
			}
			
			return wr;
		}
		
		/// <summary>
		/// Creates a POST HttpWebRequest with all the authentication headers and with a to-be-json-encoded object <paramref name="jsonData"/>.
		/// </summary>
		/// <returns>
		/// The instantiated request.
		/// </returns>
		/// <param name='jsonData'>
		/// The object to be json encoded that will serve as the request's data.
		/// </param>
		internal virtual HttpWebRequest GetBasicHttpRequest(string endpointUrl, IDictionary<string, object> jsonData) {
			string data = Configuration.JsonSerializer.Serialize(jsonData);
			
			return GetBasicHttpRequest(endpointUrl, data);
		}
		#endregion

		public PayPalConfiguration(string APICallerEmail,
								   string PaymentErrorUrl, string PaymentSuccessUrl,
		                           string FinishPaymentUrlFormat, string IPNReceivedUrl, string Currency,
		                           string OperationPayEndpoint, string OperationPreApprovalEndpoint, string OperationExecutePaymentEndpoint, string OperationGetVerifiedStatusEndpoint,
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
			this.OperationPreApprovalEndpoint = OperationPreApprovalEndpoint;
			this.OperationExecutePaymentEndpoint = OperationExecutePaymentEndpoint;
            this.OperationGetVerifiedStatusEndpoint = OperationGetVerifiedStatusEndpoint;

			this.UserId = UserId;
			this.Password = Password;
			this.Signature = Signature;
			this.ApplicationId = ApplicationId;
		}
	}
}
