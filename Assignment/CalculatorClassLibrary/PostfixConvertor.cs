using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;

namespace CalculatorClassLibrary
{
    internal class PostfixConvertor
    {
        internal int GetPrecedence(Token token)
        {
            foreach(var item in Evaluator.OperatorList)
            {
                if (item.OperatorSymbol.Equals(token.Value))
                    return item.OperatorInfo.Precedence;
            }
            return -1;
        }
        internal bool IsLeftAssocativity(Token token)
        {
            foreach (var item in Evaluator.OperatorList)
            {
                if (item.OperatorSymbol.Equals(token.Value))
                    return item.OperatorInfo.IsLeftAssociative;
            }
            return true;
        }

        internal List<Token> Convert(List<Token> infixExpression)
        {
            Stack<Token> operatorStack = new Stack<Token> ();
            List<Token> outputList = new List<Token> ();

            foreach (var token in infixExpression)
            {
                switch (token.TokenType)
                {
                    case TokenTypeEnum.OPERAND:
                        {
                            outputList.Add(token);
                            continue;
                        }
                    case TokenTypeEnum.OPENPARENTHESIS:
                        {
                            operatorStack.Push(token);
                            continue;
                        }
                    case TokenTypeEnum.CLOSEDPARENTHESIS:
                        {
                            while (operatorStack.Count > 0
                                && !(operatorStack.Peek().TokenType == TokenTypeEnum.OPENPARENTHESIS))
                            {
                                outputList.Add(operatorStack.Pop());
                            }
                            //delete extra (
                            if(operatorStack.Count > 0 &&  operatorStack.Peek().TokenType == TokenTypeEnum.OPENPARENTHESIS)
                                operatorStack.Pop();
                            else
                            {
                                throw new ArgumentException(Resources.InvalidExpression);
                            }

                            //check if the stack contains the Functions
                            //if (token.TokenType == TokenTypeEnum.FUNCTION)
                            //    outputList.Add(operatorStack.Pop());
                            continue;
                        }
                    default:
                        {
                            // -- 10 = 10
                            if(token.TokenType == TokenTypeEnum.UNARYOPERATOR ) 
                            {
                                operatorStack.Push(token);
                                continue;
                            }
                            while (operatorStack.Count > 0
                                && GetPrecedence(token) <= GetPrecedence(operatorStack.Peek())
                                && IsLeftAssocativity(token))
                            {
                                outputList.Add(operatorStack.Pop());
                            }
                            operatorStack.Push(token);
                            continue;
                        }
                }
            }

            while(operatorStack.Count > 0)
            {
                outputList.Add(operatorStack.Pop());
            }

            return outputList;
        }
    }
}