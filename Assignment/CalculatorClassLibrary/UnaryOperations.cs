using System;

namespace UnaryOperations
{
    public abstract class UnaryOperation : IOperations
    {
        public double Evaluate(double[] numbers)
        {
            if(numbers.Length >= 1)
            {
                return EvaluateCore(numbers);
            }
            //TODO: have to return a value for the error
            return -1;
        }
        public abstract double EvaluateCore(double[] numbers);

        public UnaryOperation() { }
    }
}
