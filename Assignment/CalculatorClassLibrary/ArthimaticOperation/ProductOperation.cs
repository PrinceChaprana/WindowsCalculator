using System;

namespace CalculatorClassLibrary.ArthimaticOperation
{
    public class ProductOperation : BinaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            double number1 = numbers[0];
            double number2 = numbers[1];
            return number1 * number2;
        }
    }
}

