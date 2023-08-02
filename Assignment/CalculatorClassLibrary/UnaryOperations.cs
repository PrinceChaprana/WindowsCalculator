using System;

namespace UnaryOperations
{
    public abstract class UnaryOperation : IOperations
    {
        public abstract double evaluate();

        public UnaryOperation() { }
    }
}
