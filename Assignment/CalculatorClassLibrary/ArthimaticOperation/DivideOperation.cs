using System;

namespace CalculatorClassLibrary.ArthimaticOperations
{
    internal class DivideOperation : BinaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            double number1 = numbers[0];
            double number2 = numbers[1];

            if (number2 == 0)
            {
                throw new DivideByZeroException();
            }

            return number1 / number2;
        }
    }
}
