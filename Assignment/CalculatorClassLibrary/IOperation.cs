namespace OperationInterface
{
    interface IOperation
    {
        int OperandCount { get; set; }
        int Precedence { get; set; }
        double Evaluate(double[] numbers);
    }
}
