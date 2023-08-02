﻿using BinaryOperations;
using System;

public class SubtractOperation : BinaryOperation
{
    protected override double EvaluateCore(double[] numbers)
    {
        double result = 0;
        double number1 = numbers[0];
        double number2 = numbers[1];

        result = number1 - number2;

        return result;
    }
}