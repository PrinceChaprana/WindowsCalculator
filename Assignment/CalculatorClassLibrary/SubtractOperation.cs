using BinaryOperations;
using System;

public class SubtractOperation : BinaryOperation
{
    public override double EvaluateCore(double[] numbers)
    {
        double result = 0;
        foreach (var number in numbers)
        {
            result -= number;
        }
        return result;
    }
}
