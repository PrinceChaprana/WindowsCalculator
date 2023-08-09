using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CalculatorClassLibrary
{
    public class Evaluator
    {
        PostfixCalculator postfixCalculator;
        PostfixConvertor postfixConvertor;
        StringTokenizer tokenizer;
        internal static List<OperatorData> OperatorList;

        public Evaluator()
        {
            if (OperatorList == null || OperatorList.Count <= 0)
            {
                InitilizeOpertorDictionary();
            }
            tokenizer = new StringTokenizer();
            postfixCalculator = new PostfixCalculator();
            postfixConvertor = new PostfixConvertor();
        }

        internal void InitilizeOpertorDictionary()
        {
            OperatorList = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText("Properties/OperatorJson.json"));
        }

        public double Evaluate(string expression)
        {
            double result = 0;
            //Tokenize the string
            List<Token> tokenList = tokenizer.Tokenize(expression);
            List<Token> postfixTokenList = postfixConvertor.Convert(tokenList);
            result = postfixCalculator.Calculate(postfixTokenList);
            return result;
        }

        public void RegisterCustomOperation(string Symbol, string className, int precedence, int operandCount)
        {
            OperatorInfo operatorInfo = new OperatorInfo(className, precedence, operandCount);
            OperatorData operatorData = new OperatorData(Symbol, operatorInfo);

            //var _OperatorList = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText("Properties/OperatorJson.json"));

            OperatorList.Add(operatorData);

            string jsonString = JsonConvert.SerializeObject(OperatorList);
            File.WriteAllText("Properties/OperatorJson.json", jsonString);
        }
    }
}