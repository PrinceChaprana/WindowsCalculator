using System;

namespace CalculatorClassLibrary.UnaryOperations
{
    internal class NaturalLogOperation : UnaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            return Math.Log(numbers[0]);
        }
    }
}
