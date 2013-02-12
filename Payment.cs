using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Elopayments;

namespace Elopayments.PayPal
{
	public abstract class Payment
	{
		protected PaypalConfiguration PaymentConfiguration;
		protected abstract object GetPaymentObject();

		public PaymentPhase Phase { get; protected set; }
		public InstructionAck Execute(out PayResponse payResponse) {
			if (Phase != PaymentPhase.NothingDone)
			{
				throw new InvalidOperationException("Payment has already been created or paid!");
			}
			
			//Console.WriteLine(JsonConvert.SerializeObject(GetPaymentObject()));

			// Accept everything..
			ServicePointManager.ServerCertificateValidationCallback = PaymentConfiguration.CertificateValidator;

			HttpWebRequest wr = Configuration.GetBasicHttpRequest(GetPaymentObject(), PaymentConfiguration);
			try
			{
				using (WebResponse response = wr.GetResponse())
				{
					using (Stream stream = response.GetResponseStream())
					{
						string data;
						using (StreamReader reader = new StreamReader(stream))
						{
							data = reader.ReadToEnd();
						}
						
						//Console.WriteLine("Returned data:\n {0}", data);
						
						payResponse = JsonConvert.DeserializeObject<PayResponse>(data);
						
						//Console.WriteLine("Serialized return data: {0}", JsonConvert.SerializeObject(payResponse));
						if (payResponse.responseEnvelope.ack == "Success")
						{
							this.Phase = PaymentPhase.Created;
							return InstructionAck.Success;
						}
						else if (payResponse.responseEnvelope.ack == "Error" || payResponse.responseEnvelope.ack == "Failure")
							return InstructionAck.Error;
						else
							throw new UnknownServerResponseException(data);
					}
				}
			}
			catch (WebException e) {
				if (e.Status == WebExceptionStatus.Timeout)
					throw new PaymentTimeoutException(e);
				else
					throw;
			}
		}
	}
}
