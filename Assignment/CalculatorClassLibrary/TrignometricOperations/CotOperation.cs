using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary.TrignometricOperations
{
    internal class CotOperation : TrignometricOperation
    {
        protected override double EvaluateAnswer(double radian)
        {
            //throw new ArgumentException(Resources.NotDefinedValue);

            return (Math.Sin(radian) / Math.Cos(radian));
        }
    }
}
