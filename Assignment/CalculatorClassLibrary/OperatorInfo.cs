using CalculatorClassLibrary.Properties;
using System;
using System.Reflection;

namespace CalculatorClassLibrary
{
    public class OperatorInfo
    {
        internal string ClassName {get; set;}
        internal int OperandCount{get; set;}
        internal int Precedence {get; set;}
        internal bool IsLeftAssociative { get; set;}
        internal MethodInfo EvaluateMethodInfo {get; set;}
        internal object OperatorInstance {get; set;}

        public OperatorInfo(string className, int operandCount, int precedence, bool isLeftAssociative)
        {
            this.ClassName = className;
            this.OperandCount = operandCount;
            this.Precedence = precedence;
            this.IsLeftAssociative = isLeftAssociative;
            CreateInstance();
        }

        void CreateInstance()
        {            
            Type OperatorType = Type.GetType(ClassName);
            if(OperatorType == null)
            {
                throw new ArgumentException(Resources.ClassNotFound);
            }
            this.OperatorInstance = Activator.CreateInstance(OperatorType);
            EvaluateMethodInfo = OperatorType.GetMethod(Resources.EvaluatorMethod);
        }
    }
}