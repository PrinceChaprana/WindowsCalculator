using BinaryOperations;
using System;

public class SumOperation : BinaryOperation
{
    protected override double EvaluateCore(double[] numbers)
    {
        double sum = 0;
        double number1 = numbers[0];
        double number2 = numbers[1];
        sum = number1 + number2;
        return sum;
    }
}
