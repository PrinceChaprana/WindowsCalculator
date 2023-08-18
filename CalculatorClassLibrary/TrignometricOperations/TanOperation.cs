using CalculatorClassLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary.TrignometricOperations
{
    internal class TanOperation : TrignometricOperation
    {
        protected override double EvaluateAnswer(double radian)
        {
            //radian = Math.Round(radian, 3);
            return Math.Tan(radian);
        }
    }
}
