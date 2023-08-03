using System;

namespace CalculatorClassLibrary
{
    public class OperatorInfo
    {
        public string ClassName {get; set;}
        public int OperandCount{get; set;}
        public int Precedence {get; set;}

        public OperatorInfo(string className, int operandCount, int precedence)
        {
            this.ClassName = className;
            this.OperandCount = operandCount;
            this.Precedence = precedence;
        }
    }
}