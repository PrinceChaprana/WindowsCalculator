using System;
using System.Dynamic;

namespace CalculatorClassLibrary
{
    public class OperatorData
    {
        public string OperatorSymbol { get; set; }
        public OperatorInfo OperatorInfo{get; set;}      

        public OperatorData(string operatorSymbol, OperatorInfo operatorInfo)
        {
            OperatorSymbol = operatorSymbol;
            OperatorInfo = operatorInfo;
        }       
    }
}
