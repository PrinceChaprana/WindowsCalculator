using CalculatorClassLibrary.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalculatorClassLibrary
{
    public class Evaluator
    {
        PostfixCalculator postfixCalculator;
        PostfixConvertor postfixConvertor;
        List<OperatorData> _jsonData;
        internal static Dictionary<string, OperatorInfo> OperatorMap = new Dictionary<string, OperatorInfo>();
        public Evaluator()
        {
            if(OperatorMap.Count <= 0)
            {
                InitilizeOpertorDictionary();
            }
            postfixCalculator = new PostfixCalculator();
            postfixConvertor = new PostfixConvertor();
        }

        internal void InitilizeOpertorDictionary()
        {
            _jsonData = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText("Properties/OperatorJson.json"));

            foreach ( var jsonOperatorData in _jsonData )
            {
                OperatorData operatorData = new OperatorData(jsonOperatorData.OperatorSymbol, jsonOperatorData.OperatorInfo);
                OperatorMap.Add(operatorData.OperatorSymbol,operatorData.OperatorInfo);                
            }
        }

        //convert to double after testing
        
        public double Evaluate(string expression)
        {
            double result = 0;
            //Tokenize the string
            List<Token> tokenList = TokenizeString(expression);
            List<Token> postfixTokenList = postfixConvertor.Convert(tokenList);
            result = postfixCalculator.Calculate(postfixTokenList);
            return result;
        }

        public void RegisterCustomOperation(string Symbol,string className,int precedence,int operandCount)
        {
            OperatorInfo operatorInfo = new OperatorInfo(className, precedence, operandCount);
            OperatorData operatorData = new OperatorData(Symbol, operatorInfo);

            //var _jsonData = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText("Properties/OperatorJson.json"));
            
            _jsonData.Add(operatorData);

            string jsonString = JsonConvert.SerializeObject(_jsonData);
            File.WriteAllText("Properties/OperatorJson.json", jsonString);
            OperatorMap.Add(Symbol, operatorInfo);

        }

        private List<Token> TokenizeString(string expression)
        {
            List<String> splittedString = SpacedExpression(expression);
            List<Token> tokens = new List<Token>();

            for(int index = 0; index < splittedString.Count; index++)
            {
                Token token;
                string substring = splittedString[index];
                if ((substring[0] >= '0' && substring[0] <= '9') || substring[0] == '.')
                {
                    if (substring.Contains('.'))
                    {
                        //what about 1.2.3
                        if (substring.LastIndexOf(".") != substring.IndexOf(".")){
                            throw new ArgumentException(Resources.InvalidNumber);
                        }

                        int dotIndex = substring.IndexOf('.');
                        string zeroNumber = "0";
                        // .1
                        if (dotIndex == 0)
                        {
                            substring = zeroNumber + substring;
                        }else if(dotIndex == substring.Count() - 1)
                        {
                            // 1.
                            substring = substring + zeroNumber;
                        }
                    }
                    token = new Token(substring, TokenTypeEnum.OPERAND);
                }
                else if (substring[0] >= 'a' && substring[0] <= 'z')
                {
                    token = new Token(substring, TokenTypeEnum.UNARYOPERATOR);
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
                    //if the string is + and - and either is empty or there is no operand before
                    if (substring == "-" && (tokens.Count == 0 || tokens.Last().TokenType != TokenTypeEnum.OPERAND))
                    {
                        token = new Token(substring, TokenTypeEnum.UNARYOPERATOR);
                    }
                    else
                        token = new Token(substring, TokenTypeEnum.BINARYOPERATOR);
                }
                tokens.Add(token);
            }

            return tokens;
        }

        private List<string> SpacedExpression(string expression)
        {
            List<string> result = new List<string>();

            int previousIndex = 0;
            for (int index = 0; index < expression.Length; index++)
            {
                char currentCharacter = expression[index];
                if (currentCharacter == '(' || currentCharacter == ')' || currentCharacter == ' ' 
                    || currentCharacter == '+' || currentCharacter == '-'
                    || currentCharacter == '*' || currentCharacter == '/')
                {
                    string token = expression.Substring(previousIndex, index - previousIndex);
                    //what if its tan32                    
                    result.Add(token);
                    result.Add(currentCharacter.ToString());
                    previousIndex = index + 1;
                }
            }
            if (previousIndex != expression.Length)
                result.Add(expression.Substring(previousIndex));
            List<string> result2 = new List<string>();
            foreach(string s in result)
            {
                if(s.Length != 0 && !s.Contains(' '))
                {
                    result2.Add(s);
                }
            }
            return result2;
        }

        private bool IsNumber(char character)
        {
            if ((character >= '0' && character <= '9') || character == '.')
                return true;
            return false;
        }
    }
}