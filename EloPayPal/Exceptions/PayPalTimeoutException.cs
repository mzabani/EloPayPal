using System;
using System.Net;

namespace EloPayPal
{
	public class PayPalTimeoutException : TimeoutException
	{
		public WebResponse Response;
		public WebExceptionStatus Status;

		public PayPalTimeoutException(WebException e) : base("A timeout occurred while contacting PayPal's service", e)
		{
			Response = e.Response;
			Status = e.Status;
		}
	}
}

