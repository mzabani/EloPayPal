using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace EloPayPal
{
	public enum PaymentPhase {
		NothingDone, Created, Executed
	};
}
