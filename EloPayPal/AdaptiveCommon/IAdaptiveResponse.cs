using System;
using System.Collections.Generic;

namespace EloPayPal.Adaptive
{
	/// <summary>
	/// An interface that represents a response from PayPal to an Adaptive Payments operation.
	/// </summary>
	public interface IAdaptiveResponse
	{
		ResponseEnvelope responseEnvelope { get; set; }

		IList<ErrorData> error { get; set; }
	}
}
