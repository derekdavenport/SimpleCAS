using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCAS
{
	class RationalNumber
	{
		private int numberator, denominator;
		public RationalNumber(int numberator, int denominator)
		{
			this.numberator = numberator;
			this.denominator = denominator;
			Simplify();
		}

		void Simplify()
		{
			int gcd = GCD(numberator, denominator);
			if (gcd > 0)
			{
				numberator /= gcd;
				denominator /= gcd;
			}

			if (numberator < 0 && denominator < 0)
			{
				numberator *= -1;
				denominator *= -1;
			}
		}

		// src: http://stackoverflow.com/questions/18541832/c-sharp-find-the-greatest-common-divisor
		static int GCD(int a, int b)
		{
			return b == 0 ? a : GCD(b, a % b);
		}

		public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
		{
			return new RationalNumber(r1.numberator * r2.denominator + r2.numberator * r1.denominator, r1.denominator * r2.denominator);
		}

		public static implicit operator string(RationalNumber r)
		{
			return "(" + r.numberator + "/" + r.denominator + ")";
		}


	}
}
