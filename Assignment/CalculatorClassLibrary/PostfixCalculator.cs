using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CalculatorClassLibrary
{
    internal class PostfixCalculator
    {
        internal double Solve(double[] numbers,Token token)
        {
            //using extract the token value
            if (Evaluator.OperatorMap.ContainsKey(token.Value)){
                OperatorInfo operatorData = Evaluator.OperatorMap[token.Value];
                object[] parameters = new object[] { numbers };
                object result = operatorData.OperatorType.GetMethod(Resources.EvaluatorMethod).Invoke(operatorData.OperatorInstance, parameters);
                if(double.TryParse(result.ToString() ,out double answer))
                {
                    return answer;
                }
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
                    case TokenTypeEnum.BINARYOPERATOR:
                        {
                            int numberOfOperenad = Evaluator.OperatorMap[token.Value].OperandCount;
                            numbers = new double[numberOfOperenad];

                            for(int index = numberOfOperenad - 1;index >= 0; index--)
                            {
                                if(operandStack.Count == 0 && !(token.Value == "-" || token.Value == "+"))
                                    throw new Exception(Resources.MoreOrLessOperands);

                                
                                if (operandStack.Count != 0)
                                {
                                    numbers[index] = operandStack.Peek();
                                    operandStack.Pop();
                                }
                            }
                            operandStack.Push(Solve(numbers, token));

                            continue;
                        }
                    case TokenTypeEnum.UNARYOPERATOR:
                        {
                            int noOfOperand = Evaluator.OperatorMap[token.Value].OperandCount;
                            numbers = new double[noOfOperand];
                            numbers[noOfOperand-1] = operandStack.Pop();

                            operandStack.Push(Solve(numbers,token));
                            continue;
                        }
                    case TokenTypeEnum.OPERAND:
                        {
                            operandStack.Push(double.Parse(token.Value));
                            continue;
                        }
                }
            }
            return operandStack.Peek();
        }
    }
}