using System;
using System.Collections.Generic;

namespace CalculatorClassLibrary
{
    internal static class PostfixConvertor
    {
        internal static int GetPrecedence(Token token)
        {
            if(Evaluator.OperatorMap.Count == 0)
            {
                Evaluator.InitilizeOpertorDictionary();
            }

            if (Evaluator.OperatorMap.ContainsKey(token.Value))
            {
                return Evaluator.OperatorMap[token.Value].Precedence;
            }

            return -1;
        }

        internal static List<Token> Convert(List<Token> infixExpression)
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
                            operatorStack.Pop();

                            //check if the stack contains the Functions
                            if (token.TokenType == TokenTypeEnum.FUNCTION)
                                outputList.Add(operatorStack.Pop());
                            continue;
                        }
                    default:
                        {
                            while (operatorStack.Count > 0
                                && GetPrecedence(token) <= GetPrecedence(operatorStack.Peek()))
                            {
                                outputList.Add(operatorStack.Pop());
                            }
                            operatorStack.Push(token);
                            continue;
                        }
                }
            }
                /*
                //if its a number
                else if (token.TokenType == TokenTypeEnum.CLOSEDPARENTHESIS)
                {
                    while(operatorStack.Count > 0 
                        && !(operatorStack.Peek().TokenType == TokenTypeEnum.OPENPARENTHESIS))
                    {
                        outputList.Add(operatorStack.Pop());
                    }
                    //delete extra (
                    operatorStack.Pop();
                    //check for function to return the call like trignometric
                    try
                    {
                        if (token.TokenType == TokenTypeEnum.FUNCTION)
                            outputList.Add(operatorStack.Pop());
                    }catch(ArgumentException e)
                    {
                        //Unhandled Exception
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    while(operatorStack.Count > 0 
                        && GetPrecedence(token) <= GetPrecedence(operatorStack.Peek()))
                    {
                        outputList.Add(operatorStack.Pop());
                    }
                    operatorStack.Push (token);
                }
            }
*/
            while(operatorStack.Count > 0)
            {
                outputList.Add(operatorStack.Pop());
            }

            return outputList;
        }
    }
}