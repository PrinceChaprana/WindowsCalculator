using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary.ArthimaticOperations
{
	internal class PercentageOperation : BinaryOperation
	{
		protected override double EvaluateCore(double[] numbers)
		{
			return (numbers[0] * numbers[1] / 100F);
		}
	}
}
