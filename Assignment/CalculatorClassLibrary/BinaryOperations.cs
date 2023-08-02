using System;

namespace BinaryOperations
{
    public abstract class BinaryOperation : IOperations
    {
        public BinaryOperation()
        {

        }
        public abstract double evaluate();
    }
}