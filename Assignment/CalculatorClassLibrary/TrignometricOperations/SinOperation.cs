using System;
namespace CalculatorClassLibrary.TrignometricOperations
{
    internal class SinOperation : TrignometricOperation
    {
        protected override double EvaluateAnswer(double radian)
        {
           
            return Math.Sin(radian);
        }
    }
}
