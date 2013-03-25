using System;

namespace Elopayments
{
	public class MoneyUtils
	{
		public static decimal RoundTo10CentsUp(decimal value) {
			return Math.Ceiling(value * 10) / 10;
		}
		public static decimal RoundCentsHalfUp(decimal value) {
			int last_digit = (int)(value * 1000) % 10;
			
			if (last_digit < 5)
				return RoundCentsDown(value);
			else
				return RoundCentsUp(value);
		}
		public static decimal RoundCentsUp(decimal value) {
			return Math.Ceiling(value * 100) / 100M;
		}
		public static decimal RoundCentsDown(decimal value) {
			return Math.Floor(value * 100) / 100M;
		}
	}
}

