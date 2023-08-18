using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary.TrignometricOperations
{
    internal class CosOperation : TrignometricOperation
    {
        protected override double EvaluateAnswer(double radian)
        {
            return Math.Cos(radian);
        }
    }
}
