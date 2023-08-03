using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace CalculatorClassLibrary
{
    public class Evaluator
    {
        public static Dictionary<string, OperatorInfo> OperatorMap = new Dictionary<string, OperatorInfo>();
        public Evaluator()
        {
            InitilizeOpertorDictionary();
        }

        public static void InitilizeOpertorDictionary()
        {
            OperatorInfo add = new OperatorInfo("CalculatorClassLibrary.SumOperation", 2, 1);
            OperatorInfo subtract = new OperatorInfo("CalculatorClassLibrary.SubtractOperation", 2, 1);
            OperatorInfo product = new OperatorInfo("CalculatorClassLibrary.ProductOperation", 2, 2);
            OperatorInfo divide = new OperatorInfo("CalculatorClassLibrary.DivideOperation", 2, 2);
            OperatorInfo tan = new OperatorInfo("CalculatorClassLibrary.DivideOperation", 1, 3);
            OperatorInfo sin = new OperatorInfo("CalculatorClassLibrary.DivideOperation", 1, 3);
            OperatorInfo cos = new OperatorInfo("CalculatorClassLibrary.DivideOperation", 1, 3);

            OperatorMap.Add("+", add);
            OperatorMap.Add("-", subtract);
            OperatorMap.Add("*", product);
            OperatorMap.Add("/", divide);
            OperatorMap.Add("sin", sin);
            OperatorMap.Add("tan", tan);
            OperatorMap.Add("cos", cos);
        }

        //convert to double after testing
        public List<Token> Evaluate(string expression)
        {
            double result = 0;
            //Tokenize the string
            List<Token> tokenList = TokenizeString(expression);
            List<Token> postfixTokenList = PostfixConvertor.Convertor(tokenList);
            return postfixTokenList;
        }

        private List<Token> TokenizeString(string expression)
        {
            //tokenize the string based of spaces it had
            //sin(30) + 40 returns sin(30), +, 40
            List<string> splittedString = expression.Split(' ').ToList();
            //need to make this list<string> to list<token> with all the properties of the tokens
            List<Token> tokens = new List<Token>();

            foreach (string substring in splittedString)
            {
                Token token;

                if (substring[0] >= '0' && substring[0] <= '9')
                {
                    token = new Token(substring, TokenTypeEnum.OPERAND);
                }
                else if (substring[0] >= 'a' && substring[0] <= 'z')
                {
                    token = new Token(substring, TokenTypeEnum.FUNCTION);
                }
                else if (substring.Contains("("))
                {
                    token = new Token(substring, TokenTypeEnum.OPENPARENTHESIS);
                }
                else if (substring.Contains(")"))
                {
                    token = new Token(substring, TokenTypeEnum.CLOSEDPARENTHESIS);
                }
                else
                {
                    token = new Token(substring,TokenTypeEnum.OPERATOR);
                }
                tokens.Add(token);
            }
            return tokens;
        }

    }
}