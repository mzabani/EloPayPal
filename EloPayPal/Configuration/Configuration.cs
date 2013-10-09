using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using EloPayPal.Serialization;

namespace EloPayPal
{
	public static class Configuration
	{
		private static PayPalConfiguration _currentConfiguration = null;
		public static PayPalConfiguration Current {
			get
			{
				if (_currentConfiguration == null)
				{
					throw new Exception("No configuration has been chosen yet, try using SetConfiguration first.");
				}

				return _currentConfiguration;
			}
		}

		/// <summary>
		/// Sets the PayPal App and settings to be used as the default. Don't forget to set serializers as well.
		/// </summary>
		/// <param name="conf">The configuration with the App's settings and other things.</param>
        public static void Set(PayPalConfiguration conf)
        {
            _currentConfiguration = conf;
        }

		/// <summary>
		/// Set the Json serializer and deserializer that will be used internally by EloPayPal. This must be set before making any API calls.
		/// </summary>
		public static IJsonSerializer JsonSerializer;
	}
}
