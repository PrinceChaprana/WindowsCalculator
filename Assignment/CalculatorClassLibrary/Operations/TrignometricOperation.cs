using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    internal abstract class TrignometricOperation : UnaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            double radian = (numbers[0] * 22) / (180 * 7);

            return EvaluateAnswer(radian);
        }

        protected abstract double EvaluateAnswer(double radian);
    }
}
