using CalculatorClassLibrary.Properties;
using System;

namespace CalculatorClassLibrary.UnaryOperations
{
    internal class SquareRootOperation : UnaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            if (numbers[0] < 0)
            {
                throw new ArgumentException(Resources.NotDefinedValue);
            }
            return Math.Sqrt(numbers[0]);
        }
    }
}

