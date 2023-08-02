using OperationInterface;
using System;

namespace BinaryOperations
{
    public abstract class BinaryOperation : IOperation
    {
        public BinaryOperation() { }

        public int OperandCount { get; set; }
        public int Precedence { get; set; }

        public double Evaluate(double[] numbers)
        {
           
            if (numbers.Length < 2 || numbers.Length > 2)
            {
                throw new ArgumentException("More or Less than sufficent number of Arguments");
            }
            //Issue : What should we return if the numbers are less than zero
            return EvaluateCore(numbers);
        }

        protected abstract double EvaluateCore(double[] numbers);
        
    }
}