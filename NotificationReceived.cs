using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Elopayments;

namespace Elopayments.PayPal
{
	public class NotificationReceived
	{
		private PaypalConfiguration configuration;

		public bool Execute(string ipnContent, out string paypalAnswer) {
			WebRequest wr = WebRequest.Create(configuration.IPNReceivedUrl);
			wr.Method = "POST";

			using (Stream wrContentStream = wr.GetRequestStream())
			{
				using (StreamWriter wrContentWriter = new StreamWriter(wrContentStream))
				{
					wrContentWriter.Write("cmd=_notify-validate&");
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

		public NotificationReceived(PaypalConfiguration conf)
		{
			this.configuration = conf;
		}
		
		public NotificationReceived() : this(Configuration.Current) { }
	}
}
