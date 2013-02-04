using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Elopayments.PayPal
{
	public enum PaypalConf {
		SandBox, Production
	};

	public static class Configuration
	{
		private static IDictionary<PaypalConf, PaypalConfiguration> Configurations = new Dictionary<PaypalConf, PaypalConfiguration> {
			{ PaypalConf.SandBox,
				new PaypalConfiguration("elomen_1359672509_biz@yahoo.com.br",
				                        "http://elomeno.com/error", "http://elomeno.com/Payments/Return",
				                        "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_ap-payment&paykey={0}",
				                        "ssl://www.sandbox.paypal.com/cgi-bin/webscr",
				                        "USD",
				                        "https://svcs.sandbox.paypal.com/AdaptivePayments/Pay", "https://svcs.sandbox.paypal.com/AdaptivePayments/ExecutePayment",
				                        @"elomen_1359672509_biz_api1.yahoo.com.br", @"1359672567", 
				                        @"AmGdQ6WhDUwWYSzNXl36p6HLpOdUAyacFb0kCoKo5r8cyA8TuSxhgzTU", @"APP-80W284485P519543T")
			},

			{ PaypalConf.Production,
				new PaypalConfiguration("financeiro@elomeno.com",
				                        "http://elomeno.com/error", "http://elomeno.com/Payments/Return",
				                        "https://www.paypal.com/cgi-bin/webscr?cmd=_ap-payment&paykey={0}",
				                        "ssl://ipnpb.paypal.com/cgi-bin/webscr",
				                        "BRL",
				                        "https://svcs.paypal.com/AdaptivePayments/Pay", "https://svcs.paypal.com/AdaptivePayments/ExecutePayment",
				                        @"financeiro_api1.elomeno.com", @"YMYM5NUXH953M2LB", 
				                        @"AFcWxV21C7fd0v3bYYYRCpSSRl31AmXZ0fg.GJBj-5JYnODd1XCUQeTp", @"APP-619555159N4882147")
			}
		};

		private static PaypalConfiguration _currentConfiguration = null;
		public static PaypalConfiguration Current {
			get
			{
				if (_currentConfiguration == null)
				{
					throw new Exception("No configuration has been chosen yet, try using SetConfiguration first.");
				}

				return _currentConfiguration;
			}
		}
		public static void Set(PaypalConf conf)
		{
			_currentConfiguration = Configurations[conf];
		}

		/// <summary>
		/// Creates a POST HttpWebRequest with all the authentication headers and with <paramref name="data"/> as the to-be-posted content.
		/// </summary>
		/// <returns>
		/// The instantiated request.
		/// </returns>
		/// <param name='data'>
		/// The to-be-posted content.
		/// </param>
		public static HttpWebRequest GetBasicHttpRequest(string data, PaypalConfiguration conf) {
			HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(conf.OperationPayEndpoint);

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
		public static HttpWebRequest GetBasicHttpRequest(object jsonData, PaypalConfiguration conf) {
			string data = JsonConvert.SerializeObject(jsonData);

			return GetBasicHttpRequest(data, conf);
		}
	}
}
