using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace EloPayPal
{
	public class NotificationReceived
	{
		private PayPalConfiguration configuration;

		public bool Execute(string ipnContent, out string paypalAnswer) {
			ServicePointManager.ServerCertificateValidationCallback = configuration.CertificateValidator;

			WebRequest wr = WebRequest.Create(configuration.IPNReceivedUrl);
			wr.Method = "POST";
			//wr.ContentLength = ipnContent.Length;

			using (Stream wrContentStream = wr.GetRequestStream())
			{
				using (StreamWriter wrContentWriter = new StreamWriter(wrContentStream))
				{
					wrContentWriter.Write(ipnContent);
				}
			}

			using (WebResponse response = wr.GetResponse())
			{
				using (Stream str = response.GetResponseStream())
				{
					using (StreamReader reader = new StreamReader(str))
					{
						paypalAnswer = reader.ReadToEnd();
						if (paypalAnswer == "VERIFIED")
							return true;
						else
							return false;
					}
				}
			}
		}

		public NotificationReceived(PayPalConfiguration conf)
		{
			this.configuration = conf;
		}
		
		public NotificationReceived() : this(Configuration.Current) { }
	}
}
