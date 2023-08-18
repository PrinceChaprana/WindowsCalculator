using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary.TrignometricOperations
{
    internal class SecOperation : TrignometricOperation
    {
        protected override double EvaluateAnswer(double radian)
        {
            return (double)(1 / Math.Cos(radian));
            //throw DivideByZeroException
        }
    }
}
