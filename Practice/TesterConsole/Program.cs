using CalculatorClassLibrary;
using Newtonsoft.Json;
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
            //Console.WriteLine(new Evaluator().Evaluate("10+(1)"));
            Console.WriteLine(new Evaluator().Evaluate("sin(1-tan(45))"));
            //try catch operator
            Console.ReadLine();
        }
    }
}
