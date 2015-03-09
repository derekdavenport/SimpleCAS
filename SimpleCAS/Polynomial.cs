using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCAS
{
	public class Polynomial : List<PolynomialPart>
	{
		public Polynomial(string input)
		{
			input = input.Replace("*", "");
			input = input.Replace(" ", "");
			input = input.Replace("+", " +");
			input = input.Replace("-", " -");


			string[] splitInput = input.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string part in splitInput)
			{
				this.Add(new PolynomialPart(part));
			}
		}
		/**
		 * sorts and combines parts of the same power
		 */
		public bool Normalize()
		{
			bool success = false;

			// sort required for simplification loop
			this.Sort();


			Console.WriteLine(this.ToString());

			// asynch idea: split into groups of pairs. Merge each if able, then marge results

			// foreach cannot modify the list, so must use for
			// search in reverse so we don't mess up the index
			for (int i = this.Count - 2; i >= 0; i--)
			{
				PolynomialPart part = this[i];
				PolynomialPart nextPart = this[i + 1];
				Console.WriteLine("{0}+{1}", part, nextPart);

				if (part.Power == nextPart.Power)
				{
					part.Coefficient += nextPart.Coefficient;
					this.Remove(nextPart);

				}
			}
			success = true;

			return success;
		}

		public string Differentiate()
		{
			Task<string>[] tasks = new Task<string>[this.Count];
			var i = 0;
			foreach (PolynomialPart part in this)
			{
				tasks[i++] = Task<string>.Factory.StartNew(() => part.Differentiate());
			}
			return String.Join("", Task.WhenAll(tasks).Result);
		}

		public string Integrate()
		{
			Task<string>[] tasks = new Task<string>[this.Count];
			var i = 0;
			foreach (PolynomialPart part in this)
			{
				tasks[i++] = Task<string>.Factory.StartNew(() => part.Integrate());
			}
			return String.Join("", Task.WhenAll(tasks).Result);
		}

		public override string ToString()
		{
			StringBuilder output = new StringBuilder();
			foreach (PolynomialPart part in this)
			{
				// here I call the new function. You need to move this to step 3
				output.Append(part.ToString());
			}
			return output.ToString();
		}
	}
}
