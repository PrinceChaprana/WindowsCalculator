using System.Collections.Generic;

public static class PostfixConvertor
{
    //return the precedence 
    //TODO: need to put in a xml or Json file
    internal static int Prec(char ch)
    {
        if (Evaluator.operatorMap.ContainsKey(""+ch))
        {
            return Evaluator.operatorMap["" + ch].precedence;
        }
        return -1;
    }

    public static string InfixToPostfix(string exp)
    {
        // Initializing empty String for result
        string result = "";

        // Initializing empty stack
        Stack<char> stack = new Stack<char>();

        for (int i = 0; i < exp.Length; ++i)
        {
            char c = exp[i];

            // If the scanned character is an
            // operand, add it to output.
            if (char.IsLetterOrDigit(c))
            {
                result += c;
            }

            // If the scanned character is an '(',
            // push it to the stack.
            else if (c == '(')
            {
                stack.Push(c);
            }

            // If the scanned character is an ')',
            // pop and output from the stack
            // until an '(' is encountered.
            else if (c == ')')
            {
                while (stack.Count > 0
                       && stack.Peek() != '(')
                {
                    result += stack.Pop();
                }

                if (stack.Count > 0
                    && stack.Peek() != '(')
                {
                    return "Invalid Expression";
                }
                else
                {
                    stack.Pop();
                }
            }

            // An operator is encountered
            else
            {
                while (stack.Count > 0
                       && Prec(c) <= Prec(stack.Peek()))
                {
                    result += stack.Pop();
                }
                stack.Push(c);
            }
        }

        // Pop all the operators from the stack
        while (stack.Count > 0)
        {
            result += stack.Pop();
        }

        return result;
    }
}