using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;

namespace CalculatorClassLibrary
{
    internal class PostfixCalculator
    {
        private OperatorData GetOperatorData(Token token)
        {
            foreach(var item in Evaluator.OperatorList)
            {
                if (item.OperatorSymbol.Equals(token.Value))
                    return item;
            }
            return null;
        }

        internal double Solve(double[] numbers,OperatorData operatorData)
        {
            object[] parameters = new object[] { numbers };

            object result = operatorData.OperatorInfo.EvaluateMethodInfo
                .Invoke(operatorData.OperatorInfo.OperatorInstance, parameters);

            if(double.TryParse(result.ToString() ,out double answer))
            {
                return answer;
            }                       
            return 0;
        }

        internal double Calculate(List<Token> postfixExpression)
        {
            double[] numbers;
            Stack<double> operandStack = new Stack<double>();
            foreach (Token token in postfixExpression)
            {
                switch (token.TokenType)
                {
                    case TokenTypeEnum.OPERAND:
                        {
                            operandStack.Push(double.Parse(token.Value));
                            continue;
                        }
                    default:
                        {
                            int numberOfOperenad = GetOperatorData(token).OperatorInfo.OperandCount;
                            numbers = new double[numberOfOperenad];
                            if (token.TokenType == TokenTypeEnum.UNARYOPERATOR && (token.Value == "-" || token.Value == "+"))
                            {
                                numbers[numberOfOperenad - 1] = operandStack.Pop();
                            }
                            else
                            {
                                for (int index = numberOfOperenad - 1; index >= 0; index--)
                                {
                                    if (operandStack.Count == 0 && !(token.Value == "-" || token.Value == "+"))
                                        throw new Exception(Resources.MoreOrLessOperands);


                                    if (operandStack.Count != 0)
                                    {
                                        numbers[index] = operandStack.Peek();
                                        operandStack.Pop();
                                    }
                                }
                            }
                            operandStack.Push(Solve(numbers, GetOperatorData(token)));
                            continue;
                        }

                }
            }
            return operandStack.Peek();
        }
    }
}
