using CalculatorClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TesterConsoleWindow
{
    internal class Program
    {
        static void jj()
        {
            string className = "Add";
            
            //load the class
            Type t = Type.GetType("AddOperation." +  className);
            //create its instance
            object methodInstance = Activator.CreateInstance(t);
            //get the method in the class
            MethodInfo methodinfo = t.GetMethod("evaluate");
            object result = null;
            if (methodinfo != null)
            {
                double[] numbers = { 32, 23 };
                object[] parameter = new object[] {  numbers  };
                result = methodinfo.Invoke(methodInstance, parameter);
            }
            
            Console.WriteLine(result);
        }

        static int Main(string[] args)
        {
            Evaluator evaluator = new Evaluator();
            List<string> result = evaluator.Evaluate("1");
            Console.ReadLine();
            return 0;
        }
    }
}

//https://en.wikipedia.org/wiki/Order_of_operations
//preceedence table https://pythongeeks.org/python-operator-precedence/
