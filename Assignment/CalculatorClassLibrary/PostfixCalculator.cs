using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CalculatorClassLibrary
{
    internal static class PostfixCalculator
    {
        internal static double Solve(double[] numbers,Token token)
        {
            //using extract the token value
            string operatorValue = token.Value;
            string className = Evaluator.OperatorMap[operatorValue].ClassName;

            Type operatorType = Type.GetType(className);
            object instance = Activator.CreateInstance(operatorType);

            MethodInfo[] methodInfo = operatorType.GetMethods();
            foreach(MethodInfo methods in methodInfo)
            {
                object result = null;
                if(methods.Name == Resources.EvaluatorMethod)
                {
                    object[] parameter = new object[] { numbers };
                    result = methods.Invoke(instance, parameter);
                    double answer = 0;
                    if (double.TryParse(result.ToString(), out answer))
                    {
                        return answer;
                    }
                }
            }
            //object result = null;
            //throw the exception
            
            return 0;
        }
        internal static double Calculate(List<Token> postfixExpression)
        {
            double result;
            double[] numbers;
            Stack<double> stack = new Stack<double>();
            //read the operand count to the following

            foreach (Token token in postfixExpression)
            {
                switch (token.TokenType)
                {
                    case TokenTypeEnum.OPERATOR:
                        {
                            int numberOfOperenad = Evaluator.OperatorMap[token.Value].OperandCount;
                            numbers = new double[numberOfOperenad];

                            for(int index = numberOfOperenad - 1;index >= 0; index--)
                            {
                                numbers[index] = stack.Pop();
                            }
                            stack.Push(Solve(numbers, token));

                            continue;
                        }
                    case TokenTypeEnum.FUNCTION:
                        {
                            numbers = new double[1];
                            numbers[0] = stack.Pop();

                            stack.Push(Solve(numbers,token));
                            continue;
                        }
                    case TokenTypeEnum.OPERAND:
                        {
                            stack.Push(double.Parse(token.Value));
                            continue;
                        }
                }                
            }
            return stack.Peek();
        }
    }
}