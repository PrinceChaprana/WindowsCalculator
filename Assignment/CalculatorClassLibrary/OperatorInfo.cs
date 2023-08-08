using CalculatorClassLibrary.Properties;
using System;

namespace CalculatorClassLibrary
{
    public class OperatorInfo
    {
        public string ClassName {get; set;}
        public int OperandCount{get; set;}
        public int Precedence {get; set;}

        internal Type OperatorType {get; set;}
        internal object OperatorInstance {get; set;}

        public OperatorInfo(string className, int operandCount, int precedence)
        {
            this.ClassName = className;
            this.OperandCount = operandCount;
            this.Precedence = precedence;
            CreateInstance();
        }
        void CreateInstance()
        {
            
            OperatorType = Type.GetType(ClassName);
            if(OperatorType == null)
            {
                throw new ArgumentException(Resources.ClassNotFound);
            }
            OperatorInstance = Activator.CreateInstance(OperatorType);
        }
    }
}