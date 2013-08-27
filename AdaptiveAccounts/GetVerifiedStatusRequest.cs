using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using EloPayPal;
using Newtonsoft.Json;

namespace EloPayPal.Adaptive
{
    public class GetVerifiedStatusRequest
    {
        private PayPalConfiguration RequestConfiguration;

        private string EmailAddress;

        private string FirstName, LastName;

        public GetVerifiedStatusRequest(string accountEmail, string firstName, string lastName)
            : this(accountEmail, firstName, lastName, Configuration.Current)
        {

        }

        public GetVerifiedStatusRequest(string accountEmail, string firstName, string lastName, PayPalConfiguration configuration)
        {
            RequestConfiguration = configuration;
            EmailAddress = accountEmail;
            FirstName = firstName;
            LastName = lastName;
        }

        private object GetRequestObject()
        {
            return new
            {
                emailAddress = EmailAddress,
                firstName = FirstName,
                lastName = LastName,
                matchCriteria = "NAME",
                requestEnvelope = new
                {
                    errorLanguage = "en_US", // Only en_US is supported
                    detailLevel = "ReturnAll"
                }
            };
        }

        public InstructionAck Execute(out GetVerifiedStatusResponse statusResponse)
        {
            HttpWebRequest wr = Configuration.GetBasicHttpRequest(RequestConfiguration.OperationGetVerifiedStatusEndpoint, GetRequestObject(), RequestConfiguration);
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

                        statusResponse = JsonConvert.DeserializeObject<GetVerifiedStatusResponse>(data);

                        //Console.WriteLine("Serialized return data: {0}", JsonConvert.SerializeObject(payResponse));
                        if (statusResponse.responseEnvelope.ack == "Success")
                        {
                            return InstructionAck.Success;
                        }
                        else if (statusResponse.responseEnvelope.ack == "Error" || statusResponse.responseEnvelope.ack == "Failure")
                            return InstructionAck.Error;
                        else
                            throw new UnknownServerResponseException(data);
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                    throw new PaymentTimeoutException(e);
                else
                    throw;
            }
        }
    }
}
