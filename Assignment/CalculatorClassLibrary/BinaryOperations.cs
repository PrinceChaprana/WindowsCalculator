using System;

namespace BinaryOperations
{
    public abstract class BinaryOperation : IOperations
    {
        public BinaryOperation() { }

        public double Evaluate(double[] numbers)
        {
            if (numbers.Length >= 2)
            {
                return EvaluateCore(numbers);
            }
            //Issue : What should we return if the numbers are less than zero
            return -1;
        }

        public abstract double EvaluateCore(double[] numbers);
        
    }
}