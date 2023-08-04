using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using static System.Net.Mime.MediaTypeNames;

namespace CalculatorClassLibrary
{
    public class Evaluator
    {
        internal static Dictionary<string, OperatorInfo> OperatorMap = new Dictionary<string, OperatorInfo>();
        
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