namespace CalculatorClassLibrary
{
    public class Operator
    {
        public string OperatorSymbol { get; set; }
        public OperatorInfo operatorInfo;

        public Operator(string operatorSymbol, OperatorInfo operatorInfo)
        {
            OperatorSymbol = operatorSymbol;
            this.operatorInfo = operatorInfo;
        }
    }
}
