using CalculatorClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            Console.WriteLine("-".Contains("-"));
            Evaluator evaluator = new Evaluator();
            List<Token> result = evaluator.Evaluate("sin ( cos ( tan ( 90 ) + 120 ) )");
            foreach (var item in result)
            {
                Console.WriteLine(item.Value);
            }
            Console.ReadKey();
        }
    }
}
