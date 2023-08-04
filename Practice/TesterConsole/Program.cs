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
            Console.WriteLine(new Evaluator().Evaluate("rec ( 25 ) * 100"));
            //try catch operator
            Console.ReadLine();
        }
    }
}
