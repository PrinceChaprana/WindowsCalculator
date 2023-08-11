using CalculatorClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class MainFrame : Form
    {
        public Stack<Control> panelStack = new Stack<Control>();
        //for spaceing between components
        //the gap between the table layout component
        int _tableLayoutColumn = 1; 
        int _tableLayoutRow;

        bool _showScientificButtons = true;

        private string Expression;
        private string InputString = string.Empty;
        private string ResultString = "";

        List<ButtonData> ButtonDataList;
        Evaluator evaluator;
        ButtonTypeEnum _lastPressedButton = ButtonTypeEnum.OPERATION;

        int _parenthesisCount = 0;


        public MainFrame()
        {
            //InitializeComponent();
            string buttonDataJsonString = File.ReadAllText("Properties/ButtonsDataJson.json");
            ButtonDataList = JsonConvert.DeserializeObject<List<ButtonData>>(buttonDataJsonString);
            FetchBasicDataFromJson();
            evaluator = new Evaluator();
            InitilizeUI();
        }

        private void FetchBasicDataFromJson()
        {
            int maxColumn = -1;
            foreach (var buttonData in ButtonDataList)
            {
                maxColumn = Math.Max(maxColumn, buttonData.Column);
            }
            _tableLayoutColumn = maxColumn;
        }


        ////eventargs kya
        //private void Button_click(object sender, EventArgs e)
        //{
        //    Button button = (Button) sender;
        //    string buttonName = button.Text;
        //    ButtonData buttonData = GetButtonData(buttonName);
        //    //modify content based of operation
        //    //make these in background thread
        //    if(_lastPressedButton == ButtonTypeEnum.EQUAL)
        //        ResetCalculator();

        //    switch (buttonData.ButtonType)
        //    {
        //        case ButtonTypeEnum.EQUAL:
        //            {
        //                EvaluateResult();
        //                break;
        //            }
        //        case ButtonTypeEnum.OPERATION:
        //            {
        //                PerformOperation(buttonData.Symbol);
        //                break;
        //            }
        //        case ButtonTypeEnum.OPENPARENTHASIS:
        //            {
        //                if(_lastPressedButton == ButtonTypeEnum.OPERAND)
        //                {
        //                    InputString += "*";
        //                }
        //                _parenthesisCount += 1;
        //                UpdateInputExpression(buttonData.Symbol);
        //                break;
        //            }
        //        case ButtonTypeEnum.CLOSEDPARENTHASIS:
        //            {
        //                if(_parenthesisCount <= 0)
        //                {
        //                    return;
        //                }
        //                _parenthesisCount -= 1;
        //                UpdateInputExpression(buttonData.Symbol);
        //                break;
        //            }
        //        case ButtonTypeEnum.FUNCTION:
        //            {
        //                _parenthesisCount += 1;
        //                UpdateInputExpression(buttonData.Symbol + "(");
        //                break;
        //            }
        //        case ButtonTypeEnum.OPERAND:
        //            {
        //                UpdateInputExpression(buttonData.Symbol);
        //                break;
        //            }
        //        case ButtonTypeEnum.OPERATOR:
        //            {
        //                UpdateInputExpression(buttonData.Symbol); 
        //                break;
        //            }
                
        //    }
        //    UpdateInputTextBox();
        //    _lastPressedButton = buttonData.ButtonType;
            
        ////}

        private void UpdateInputTextBox()
        {
            //all evaluator here
            inputPanelTextBox.Text = InputString;
        }

        private void EvaluateResult()
        {
            Expression += InputString;
            try
            {
                double result = evaluator.Evaluate(Expression);
                outputPanelTextBox.Text = result.ToString();
                Expression = "";
            }catch(Exception ex)
            {
                outputPanelTextBox.Text = ex.Message;
            }
        }

        private void ResetCalculator()
        {
            InputString = "";
            inputPanelTextBox.Text = InputString;
        }

        private void PerformOperation(string Symbol)
        {
            if(Symbol == "del" && InputString.Length > 0)
            {
                InputString = InputString.Substring(0,InputString.Length - 1);
                inputPanelTextBox.Text = InputString;                
            }
            else if(Symbol == "CE")
            {
                ResetCalculator();
                ResultString = "";
                outputPanelTextBox.Text = ResultString;
            }else if(Symbol == "C")
            {
                InputString = "";

            }

        }

        //if operator is clicked
        private void UpdateInputExpression(string opeartorSymbol)
        {
            InputString += opeartorSymbol;
        }

        private ButtonData GetButtonData(string buttonName)
        {
            foreach(var button in ButtonDataList)
            {
                if (buttonName.Equals(button.Name))
                {
                    return button;
                }
            }
            return null;
        }        
    }
}
