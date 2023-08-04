using System;

namespace CalculatorClassLibrary.UnaryOperations
{
    internal class ReciprocalOperation : UnaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            return (double)(1/ numbers[0]);
            //throws DivideByZeroExcepition
        }
    }
}
