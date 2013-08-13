using System;
using System.Net;

namespace EloPayPal
{
	public class PaymentTimeoutException : TimeoutException
	{
		public WebResponse Response;
		public WebExceptionStatus Status;

		public PaymentTimeoutException(WebException e) : base("A timeout occurred while contacting the payment service", e)
		{
			Response = e.Response;
			Status = e.Status;
		}
	}
}

