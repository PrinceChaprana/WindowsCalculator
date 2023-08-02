using BinaryOperations;
using System;

public class DivideOperation : BinaryOperation
{
    protected override double EvaluateCore(double[] numbers)
    {
        double number1 = numbers[0];
        double number2 = numbers[1];

        if(number2 == 0)
        {
            throw new DivideByZeroException();
        }

        double result = number1 / number2;
        return result;
    }
}
