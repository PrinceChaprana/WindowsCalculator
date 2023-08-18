using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    internal abstract class TrignometricOperation : UnaryOperation
    {
        const double PI = 3.1415926535897931;
        protected override double EvaluateCore(double[] numbers)
        {
            double radian = (numbers[0] * PI) / 180;
            return EvaluateAnswer(radian);
        }

        protected abstract double EvaluateAnswer(double radian);
    }
}
