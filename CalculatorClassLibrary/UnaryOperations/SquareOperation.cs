namespace CalculatorClassLibrary.UnaryOperations
{
    internal class SquareOperation : UnaryOperation
    {
        protected override double EvaluateCore(double[] numbers)
        {
            return numbers[0] * numbers[0];
        }
    }
}
