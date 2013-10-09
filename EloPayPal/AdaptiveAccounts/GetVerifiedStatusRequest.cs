using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EloPayPal;

namespace EloPayPal.Adaptive
{
	public class GetVerifiedStatusRequest : Request<GetVerifiedStatusResponse>
    {
		public string EmailAddress { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }

        public GetVerifiedStatusRequest(string accountEmail, string firstName, string lastName)
            : this(accountEmail, firstName, lastName, Configuration.Current)
        {
        }

        public GetVerifiedStatusRequest(string accountEmail, string firstName, string lastName, PayPalConfiguration configuration)
			: base(configuration)
        {
            EmailAddress = accountEmail;
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IDictionary<string, object> GetRequestObject()
        {
			var obj = new Dictionary<string, object> {
				{ "emailAddress", EmailAddress },
				{ "firstName", FirstName },
				{ "lastName", LastName },
				{ "matchCriteria", "NAME" }
			};

			return obj;
        }

        public override RequestAck Execute(out GetVerifiedStatusResponse statusResponse)
        {
			return Execute(RequestConfiguration.OperationGetVerifiedStatusEndpoint, out statusResponse);
        }
    }
}
