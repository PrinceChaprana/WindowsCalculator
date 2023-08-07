using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalculatorClassLibrary
{
    public class Evaluator
    {
        internal static Dictionary<string, OperatorInfo> OperatorMap = new Dictionary<string, OperatorInfo>();
        internal static Dictionary<string, object> MethodInfoMap = new Dictionary<string, object>();
        public Evaluator()
        {
            InitilizeOpertorDictionary();
        }

        internal static void InitilizeOpertorDictionary()
        {
            string pathName = Path.Combine(Environment.CurrentDirectory, "Properties/OperatorJson.json");

            var jsonData = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText(pathName));

            foreach ( var opertorData in jsonData )
            {
                OperatorMap.Add(opertorData.OperatorSymbol, opertorData.OperatorInfo);
                string className = opertorData.OperatorInfo.ClassName;

                Type operatorType = Type.GetType(className);
                object instance = Activator.CreateInstance(operatorType);
                //instance is more heavy then gettype method
                MethodInfoMap.Add(opertorData.OperatorSymbol, instance);
            }
        }

        //convert to double after testing
        
        public double Evaluate(string expression)
        {
            double result = 0;
            //Tokenize the string
            List<Token> tokenList = TokenizeString(expression);
            List<Token> postfixTokenList = PostfixConvertor.Convert(tokenList);
            result = PostfixCalculator.Calculate(postfixTokenList);
            return result;
        }

        private List<Token> TokenizeString(string expression)
        {
            //tokenize the string based of spaces it had
            //sin(30) + 40 returns sin(30), +, 40
            List<String> splittedString = SpacedExpression(expression);
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
                else if (substring.Contains('('))
                {
                    token = new Token(substring, TokenTypeEnum.OPENPARENTHESIS);
                }
                else if (substring.Contains(')'))
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

        private List<string> SpacedExpression(string expression)
        {
            List<string> result = new List<string>();

            int prev = 0;
            for (int index = 0; index < expression.Length; index++)
            {
                char current = expression[index];
                if (current == '(' || current == ')' || current == ' ' 
                    || current == '+' || current == '-'
                    || current == '*' || current == '/')
                {
                    result.Add(expression.Substring(prev , index - prev));
                    result.Add(current.ToString());
                    prev = index + 1;
                }
            }
            if (prev != expression.Length)
                result.Add(expression.Substring(prev));
            List<string> result2 = new List<string>();
            foreach(string s in result)
            {
                if(s.Length != 0)
                {
                    result2.Add(s);
                }
            }
            return result2;
        }
    }
}