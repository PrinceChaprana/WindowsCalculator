using CalculatorClassLibrary.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorClassLibrary
{
    public class StringTokenizer
    {
        internal List<Token> Tokenize(string expression)
        {
            List<String> splittedString = SpacedExpression(expression);
            List<Token> tokens = new List<Token>();

            for (int index = 0; index < splittedString.Count; index++)
            {
                Token token;
                string substring = splittedString[index];
                if ((substring[0] >= '0' && substring[0] <= '9') || substring[0] == '.')
                {
                    if (substring.Contains('.'))
                    {
                        //what about 1.2.3
                        if (substring.LastIndexOf(".") != substring.IndexOf("."))
                        {
                            throw new ArgumentException(Resources.InvalidNumber);
                        }

                        int dotIndex = substring.IndexOf('.');
                        string zeroNumber = "0";
                        // .1
                        if (dotIndex == 0)
                        {
                            substring = zeroNumber + substring;
                        }
                        else if (dotIndex == substring.Count() - 1)
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
            var result = new List<string>();
            int previousIndex = -1;
            char previousCharacter = expression[0];
            for (int index = 0; index < expression.Length; index++)
            {
                char currentCharacter = expression[index];
                if (IsNumber(currentCharacter))
                {
                    //.3
                    if (IsNumber(previousCharacter) || previousCharacter == '.')
                    {
                        previousCharacter = currentCharacter;
                        continue;
                    }
                }
                else if (IsAlphabet(currentCharacter))
                {
                    if (IsAlphabet(previousCharacter))
                    {
                        previousCharacter = currentCharacter;
                        continue;
                    }
                }
                if (currentCharacter == '.' && IsNumber(previousCharacter))
                    continue;
                if (previousIndex == -1)
                    result.Add(expression.Substring(previousIndex + 1, index - previousIndex - 1));
                else
                    result.Add(expression.Substring(previousIndex, index - previousIndex));
                //result.Add(expression[index].ToString());
                previousIndex = index;
                previousCharacter = currentCharacter;
            }
            if (previousIndex != expression.Length)
            {
                result.Add(expression.Substring(previousIndex));
            }
            List<string> errorFreeList = new List<string>();
            foreach (var token in result)
            {
                if (token.Length != 0 && !token.Contains(" "))
                {
                    errorFreeList.Add(token);
                }
            }
            return errorFreeList;
        }

        public bool IsAlphabet(char currentCharacter)
        {
            if ((currentCharacter >= 'a' && currentCharacter <= 'z')
                || (currentCharacter >= 'A' && currentCharacter <= 'Z'))
            {
                return true;
            }
            return false;
        }

        public bool IsNumber(char currentCharacter)
        {
            if (currentCharacter >= '0' && currentCharacter <= '9')
                return true;
            return false;
        }
    }
}