using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace EloPayPal
{
	public abstract class Payment
	{
		protected PayPalConfiguration PaymentConfiguration;
		protected abstract object GetPaymentObject();

		public string TrackingId { get; set; }

		protected int _PayKeyDuration;

		/// <summary>
		/// Sets or gets the PayKey's duration in minutes. Must be greater than 5 and less than 30 days.
		/// </summary>
		/// <value>The duration of the PayKey in minutes.</value>
		public int PayKeyDuration {
			get
			{
				if (_PayKeyDuration == 0)
					throw new InvalidOperationException("PayKey's duration has not been set yet");

				return _PayKeyDuration;
			}
			set
			{
				if (value < 5 || value > 30 * 24 * 60)
					throw new ArgumentOutOfRangeException("value", "Duration must be between 5 minutes and 30 days");

				_PayKeyDuration = value;
			}
		}

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
					using (Stream responseStream = response.GetResponseStream())
					{
						string data;
						using (StreamReader reader = new StreamReader(responseStream))
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
