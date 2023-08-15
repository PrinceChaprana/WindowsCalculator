using System;

namespace CalculatorClassLibrary.UnaryOperations
{
    internal class LogOperation : UnaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            return Math.Log10(numbers[0]);
        }
    }
}
