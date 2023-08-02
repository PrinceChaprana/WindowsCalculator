using System;
using OperationInterface;
namespace UnaryOperations
{
    public abstract class UnaryOperation : IOperation
    {
        public int OperandCount { get; set; }
        public int Precedence { get; set; }

        public double Evaluate(double[] numbers)
        {
            if(numbers.Length > 1 || numbers.Length < 1)
            {
                throw new ArgumentException("More or Less than Number of Arguments");
            }
            //TODO: have to return a value for the error
            return EvaluateCore(numbers);
        }
        protected abstract double EvaluateCore(double[] numbers);
    }
}
