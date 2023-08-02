using OperationInterface;
using System;
using System.Collections.Generic;

public class Evaluator
{
    public class OperatorInfo
    {
        public string className;
        public int operandCount;
        public int precedence;

        public OperatorInfo(string className,int operandCount,int precedence)
        {
            this.className = className;
            this.operandCount = operandCount;
            this.precedence = precedence;
        }
    }
    public static Dictionary<string, OperatorInfo> operatorMap = new Dictionary<string, OperatorInfo>();
    public Evaluator() {
        InitilizeOpertorDictionary();
    }

    private void InitilizeOpertorDictionary()
    {
        OperatorInfo add = new OperatorInfo("SumOperation", 2, 1);
        OperatorInfo subtract = new OperatorInfo("SubtractOperation", 2, 1);
        OperatorInfo product = new OperatorInfo("ProductOperation", 2, 1);
        OperatorInfo divide = new OperatorInfo("DivideOperation", 2, 1);
        operatorMap.Add("+", add);
        operatorMap.Add("-", subtract);
        operatorMap.Add("*", product);
        operatorMap.Add("/", divide);
    }

    public double Evaluate(string expression)
    {
        List<string> tokenList = TokenizeString(expression);
        //Solve All trigno Operator BeforeHand
        //replace there value in place of their value
        List<string> solvedTokens = ReplaceUnaryOperations(tokenList);
        string postifixExpression = PostfixConvertor.InfixToPostfix(expression);
        double result = PostfixEvaluator.;
    }

    private List<string> ReplaceUnaryOperations(List<string> tokenList)
    {
        foreach (string token in tokenList)
        {
            if (token[0] >='a' && token[0] <= 'z')
            {
                int indexOfOpenBracket = token.IndexOf('(');
                string operand = token.Substring(0, indexOfOpenBracket);
                string value = token.Substring(indexOfOpenBracket + 1,token.Length - indexOfOpenBracket - 2);

                Type type = Type.GetType(String.Concat(operatorMap[operand].className,",","Unary"));

                double result = instance.evaluate()
            }
        }
    }

    private List<string> TokenizeString(string expression)
    {
        //tokenize the string based of spaces it had
        //sin(30) + 40 returns sin(30), +, 40
        List<string> tokens = new List<string>();
        int spaceIndex = -1;
        string token = "";
        for (int i = 0; i < expression.Length; i++)
        {
            if (i == (expression.Length - 1))
            {
                tokens.Add(token + expression[i]);
                break;
            }
            if (expression[i] == ' ' && i != spaceIndex)
            {
                spaceIndex = i;
                tokens.Add(token);
                token = "";
                continue;
            }
            token += expression[i];
        }
        return tokens;
    }
}
