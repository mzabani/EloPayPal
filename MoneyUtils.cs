using System;

namespace EloPayPal
{
	public class MoneyUtils
	{
        public static decimal RoundTo50CentsUp(decimal value)
        {
            decimal valueX100 = value * 100M;

            // The last two digits may in fact not be an integer, but the algorithm still works fine like his
            decimal lastTwoDigits = valueX100 % 100M;
            if (lastTwoDigits == 0 || lastTwoDigits == 50)
                return value;
            else if (lastTwoDigits < 50)
                return Math.Floor(value) + .50M;
            else
                return Math.Ceiling(value);
        }

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
