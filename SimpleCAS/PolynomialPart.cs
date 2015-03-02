using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCAS
{
	public class PolynomialPart : IComparable<PolynomialPart>
	{
		private string part;
		private bool positive;
		private float coefficient;
		private int power;

		/**
		 * constructor
		 */
		public PolynomialPart(string part)
		{
			this.part = part;

			// used to point to what we're looking at in the string
			int index;

			// search for coefficient
			if (part.IndexOf('+') == 0)
			{
				this.positive = true;
				index = 1;
			}
			else if (part.IndexOf('-') == 0)
			{
				this.positive = false;
				index = 1;
			}
			else
			{
				this.positive = true;
				index = 0;
			}

			int start = index;
			char[] decimals = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

			while (index < part.Length && part.IndexOfAny(decimals, index, 1) == index)
			{
				index++;
			}

			// parse the coefficient from string to int if one was found, otherwise 1
			int length = index - start;
			this.coefficient = length > 0 ? Convert.ToSingle(part.Substring(start, length)) : 1;
			if (!this.positive)
				this.coefficient *= -1;

			// look for an x
			if (index >= part.Length || part.IndexOf('x', index, 1) == -1)
			{
				// no x found
				this.power = 0;
			}
			else
			{
				// x found
				// look at the next char
				index++;
				// search for a caret
				if (index >= part.Length || part.IndexOf('^', index, 1) == -1)
				{
					// no ^ found
					this.power = 1;
				}
				else
				{
					// found a ^ 
					// look for the power
					index++;
					this.power = Convert.ToInt32(part.Substring(index));
				}
			}

		}

		public int Power
		{
			get { return power; }
		}

		public float Coefficient
		{
			get { return coefficient; }
			set { coefficient = value; }
		}

		// used to sort. Sort in descending order of power
		public int CompareTo(PolynomialPart otherPart)
		{
			return otherPart.power - this.power;
		}

		/**
		 * 
		 */
		public string Differentiate()
		{
			string derivative;

			if (this.power == 0)
			{
				// the derivative of constant is 0
				derivative = "";
			}
			else if (this.power == 1)
			{
				//the derivative of coefficient*x^1 is coefficient
				derivative = this.coefficient.ToString("+#;-#");
			}
			else
			{
				// the coefficient of derivative is (power-1)*coefficient
				derivative = (this.power * this.coefficient).ToString("+#;-#") + "x^" + (this.power - 1);
			}

			return derivative;
		}

		public string Integrate()
		{
			string integral;

			if (this.power == 0)
			{
				// power 0 --> 1, coefficient keep same
				integral = this.coefficient.ToString("+#;-#");
				integral += "x^" + (this.power + 1);
			}
			else
			{
				this.power = this.power + 1;
				integral = (this.coefficient / (this.power)).ToString("+#;-#");
				integral += "x^" + (this.power);
			}
			return integral;
		}

		public override string ToString()
		{
			return this.coefficient.ToString("+#.#;-#.#") + "x^" + this.power;
		}
	}
}
