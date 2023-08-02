using BinaryOperations;
using System;

public class SumOperation : BinaryOperation
{
    public override double EvaluateCore(double[] numbers)
    {
        double sum = 0;
        foreach (var number in numbers)
        {
            sum+= number;
        }
        return sum;
    }
}
