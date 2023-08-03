using System;
using System.Collections.Generic;

namespace CalculatorClassLibrary
{
    public static class PostfixConvertor
    {
        public static int GetPrecedence(Token token)
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
               
        public static List<Token> Convertor(List<Token> infixExpression)
        {
            Stack<Token> operatorStack = new Stack<Token> ();
            List<Token> outputList = new List<Token> ();

            foreach(var token in  infixExpression)
            {
                //if its a number
                if (token.TokenType == TokenTypeEnum.OPERAND)
                {
                    outputList.Add (token);
                }else if (token.TokenType == TokenTypeEnum.OPENPARENTHESIS)
                {
                    //if left parenthisis
                    operatorStack.Push(token);
                }else if (token.TokenType == TokenTypeEnum.CLOSEDPARENTHESIS)
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

            while(operatorStack.Count > 0)
            {
                outputList.Add(operatorStack.Pop());
            }

            return outputList;
        }
    }
}