using System;
using System.Collections.Generic;

namespace PostfixEvaluator
{
    public class PostfixEvaluator
    {
        int isOperator(char ch)
        {
            if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^')
                return 1;    //character is an operator
            return -1;   //not an operator
        }

        int isOperand(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return 1;    //character is an operand
            return -1;   //not an operand
        }

        double Operation(double a, double b, char op)
        {
            //Perform operation
            if (op == '+')
                return b + a;
            else if (op == '-')
                return b - a;
            else if (op == '*')
                return b * a;
            else if (op == '/')
                return b / a;
            else if (op == '^')
                return Math.Pow(b, a);    //find b^a
            else
                return double.MinValue;    //return negative infinity
        }

        double ScanNumber(char ch)
        {
            double value;
            value = ch;
            return (value - '0');   //return float from character
        }

        private double PostfixEval(string postfix)
        {
            double a, b;
            Stack<double> stk = new Stack<double>();


            foreach (char it in postfix)
            {
                {
                    //read elements and perform postfix evaluation
                    if (isOperator(it) != -1)
                    {
                        a = stk.Peek();
                        stk.Pop();
                        b = stk.Peek();
                        stk.Pop();
                        stk.Push(Operation(a, b, it));
                    }
                    else if (isOperand(it) > 0)
                    {
                        stk.Push(ScanNumber(it));
                    }
                }
            }
            return stk.Peek();
        }
    }

}