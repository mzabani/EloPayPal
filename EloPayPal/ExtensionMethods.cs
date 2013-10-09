using System;

namespace EloPayPal
{
	static class ExtensionMethods
	{
		public static string ToIsoDateAndTimeString(this DateTime date) {
			if (date.Kind == DateTimeKind.Utc)
				return string.Format(@"{0:yyyy-MM-ddThh\:m\:ss.fZ}", date);
			else if (date.Kind == DateTimeKind.Local)
				return string.Format(@"{0:yyyy-MM-ddThh\:m\:ss.fzzz}", date);

			throw new ArgumentException("The DateTime has to be of kind Local or Utc. The kind of the passed date is Unspecified");
		}
	}
}
