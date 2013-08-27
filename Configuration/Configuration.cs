using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using EloPayPal.Serialization;

namespace EloPayPal
{
	public static class Configuration
	{
		private static PayPalConfiguration _currentConfiguration = null;
		public static PayPalConfiguration Current {
			get
			{
				if (_currentConfiguration == null)
				{
					throw new Exception("No configuration has been chosen yet, try using SetConfiguration first.");
				}

				return _currentConfiguration;
			}
		}

		/// <summary>
		/// Sets the PayPal App and settings to be used as the default. Don't forget to set serializers as well.
		/// </summary>
		/// <param name="conf">The configuration with the App's settings and other things.</param>
        public static void Set(PayPalConfiguration conf)
        {
            _currentConfiguration = conf;
        }

		public static IJsonSerializer JsonSerializer;

		/// <summary>
		/// Creates a POST HttpWebRequest with all the authentication headers and with <paramref name="data"/> as the to-be-posted content.
		/// </summary>
		/// <returns>
		/// The instantiated request.
		/// </returns>
		/// <param name='data'>
		/// The to-be-posted content.
		/// </param>
		public static HttpWebRequest GetBasicHttpRequest(string endpointUrl, string data, PayPalConfiguration conf) {
			HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(endpointUrl);

			wr.Method = "POST";
			wr.ContentType = "application/x-www-form-urlencoded";

			wr.Headers.Add("X-PAYPAL-SECURITY-USERID", conf.UserId);
			wr.Headers.Add("X-PAYPAL-SECURITY-PASSWORD", conf.Password);
			wr.Headers.Add("X-PAYPAL-SECURITY-SIGNATURE", conf.Signature);

			wr.Headers.Add("X-PAYPAL-APPLICATION-ID", conf.ApplicationId);

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
		/// Creates a POST HttpWebRequest with all the authentication headers and with a json encoded object <paramref name="jsonData"/>.
		/// </summary>
		/// <returns>
		/// The instantiated request.
		/// </returns>
		/// <param name='jsonData'>
		/// The object to be json encoded that will serve as the request's data.
		/// </param>
		public static HttpWebRequest GetBasicHttpRequest(string endpointUrl, object jsonData, PayPalConfiguration conf) {
			string data = Configuration.JsonSerializer.Serialize(jsonData);

			return GetBasicHttpRequest(endpointUrl, data, conf);
		}
	}
}
