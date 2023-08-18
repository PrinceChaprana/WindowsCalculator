using System;

namespace CalculatorClassLibrary.ArthimaticOperations
{
    internal class PowerOperation : BinaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            return Math.Pow(numbers[0], numbers[1]);
        }
    }
}
