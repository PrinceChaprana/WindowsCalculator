using CalculatorClassLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;

namespace TesterConsole
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Evaluator evaluator = new Evaluator();
            //SpacedExpression("1.2 + .1");
                Console.WriteLine(evaluator.Evaluate("rec(2+1)"));
                //Console.WriteLine(evaluator.Evaluate("2*5%"));
                Console.WriteLine(evaluator.Evaluate("1.2 + .1"));
                Console.WriteLine(evaluator.Evaluate("2^(3^2)"));

                Console.WriteLine(evaluator.Evaluate("-(1+1)+-1"));
                Console.WriteLine(evaluator.Evaluate("-1+-tan45"));
                Console.WriteLine(evaluator.Evaluate("8-2*3"));
                //Console.WriteLine(evaluator.Evaluate("2*3!"));
                //Console.WriteLine(evaluator.Evaluate("2.1%1.2"));
                Console.WriteLine(evaluator.Evaluate("-1--3"));
                Console.WriteLine(evaluator.Evaluate("4+-(2)"));
                //Console.WriteLine(evaluator.Evaluate("3+2^3^sqr(rec(1/4))"));
                Console.WriteLine(evaluator.Evaluate("-(sin30+-cos(30))"));
                Console.WriteLine(evaluator.Evaluate("+2+4"));
                Console.WriteLine(evaluator.Evaluate("144^(1/2)"));
                Console.WriteLine(evaluator.Evaluate("2+2*sqr4"));
                //Evaluator.RegisterCustomOperation("prince", "Custom", 2, 1);
           
            //TASKs
            // add sqr30
            //Capital case
            //try catch operator
            Console.ReadLine();
        }


        public static bool IsAlphabet(char currentCharacter)
        {
            if((currentCharacter >= 'a' && currentCharacter <= 'z') 
                || (currentCharacter >='A' && currentCharacter <= 'Z'))
            {
                return true;
            }
            return false;
        }

        public static bool IsNumber(char currentCharacter)
        {
            if (currentCharacter >= '0' && currentCharacter <= '9')
                return true;
            return false;
        }
    }
}
