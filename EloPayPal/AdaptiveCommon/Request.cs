using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace EloPayPal.Adaptive
{
	/// <summary>
	/// An abstract class to derive from to more easily implement Adaptive Payments operations.
	/// </summary>
	public abstract class Request<T>
		where T : IAdaptiveResponse
	{
		protected readonly PayPalConfiguration RequestConfiguration;
		protected abstract IDictionary<string, object> GetRequestObject();

		public Request(PayPalConfiguration conf)
		{
			RequestConfiguration = conf;
		}

		public Request()
			: this(Configuration.Current)
		{
		}

        /// <summary>
        /// Set this to overwrite the Url PayPal redirects the user to after a successful operation.
        /// </summary>
        public string ReturnUrl { get; set; }
		
        /// <summary>
        /// Set this to overwrite the Url PayPal redirects the user to after the user cancels an operation.
        /// </summary>
        public string CancelUrl { get; set; }

		/// <summary>
		/// Sets properties common to all adaptive payment requests: returnUrl, cancelUrl, ipnNotificationUrl, currencyCode and requestEnvelope as long
		/// as they haven't been set.
		/// </summary>
		/// <param name="requestObject">The request parameters.</param>
		private void SetCommonRequestProperties(IDictionary<string, object> requestObject) {
			if (requestObject == null)
				throw new ArgumentNullException("requestObject");

			if (requestObject.ContainsKey("currencyCode") == false)
				requestObject.Add("currencyCode", RequestConfiguration.Currency);

			if (requestObject.ContainsKey("ipnNotificationUrl") == false)
				requestObject.Add("ipnNotificationUrl", RequestConfiguration.IPNNotificationUrl);

			if (requestObject.ContainsKey("returnUrl") == false)
				requestObject.Add("returnUrl", ReturnUrl ?? RequestConfiguration.PaymentSuccessUrl);

			if (requestObject.ContainsKey("cancelUrl") == false)
				requestObject.Add("cancelUrl", CancelUrl ?? RequestConfiguration.PaymentErrorUrl);

			if (requestObject.ContainsKey("requestEnvelope") == false)
				requestObject.Add("requestEnvelope", new {
					errorLanguage = "en_US",
					detailLevel = "ReturnAll"
				});
		}

		protected RequestAck Execute(string remoteEndpoint, out T response)
		{
			//Console.WriteLine(JsonConvert.SerializeObject(GetPaymentObject()));

			IDictionary<string, object> requestParameters = GetRequestObject();
			SetCommonRequestProperties(requestParameters);

			HttpWebRequest wr = RequestConfiguration.GetBasicHttpRequest(remoteEndpoint, requestParameters);
			try
			{
				using (WebResponse webResponse = wr.GetResponse())
				{
					using (Stream responseStream = webResponse.GetResponseStream())
					{
						string data;
						using (StreamReader reader = new StreamReader(responseStream))
						{
							data = reader.ReadToEnd();
						}
						
						//Console.WriteLine("Returned data:\n {0}", data);
						
						response = Configuration.JsonSerializer.Deserialize<T>(data);
						
						//Console.WriteLine("Serialized return data: {0}", JsonConvert.SerializeObject(payResponse));
						if (response.responseEnvelope.ack == "Success")
						{
							return RequestAck.Success;
						}
						else if (response.responseEnvelope.ack == "Error" || response.responseEnvelope.ack == "Failure")
							return RequestAck.Error;
						else
							throw new UnknownServerResponseException(data);
					}
				}
			}
			catch (WebException e) {
				if (e.Status == WebExceptionStatus.Timeout)
					throw new PayPalTimeoutException(e);
				else
					throw;
			}
		}
	
		public abstract RequestAck Execute(out T response);
	}
}
