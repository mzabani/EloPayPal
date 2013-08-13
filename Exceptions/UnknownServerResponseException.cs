using System;
using System.Net;

namespace EloPayPal
{
	public class UnknownServerResponseException : Exception
	{
		public string ServerResponse;

		public UnknownServerResponseException(string response) : base()
		{
			ServerResponse = response;
		}

		public override string ToString ()
		{
			return base.ToString() + "\n\n\nServer's response:\n" + ServerResponse;
		}
	}
}

