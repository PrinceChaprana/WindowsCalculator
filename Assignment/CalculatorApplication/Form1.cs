using CalculatorClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class MainForm : Form
    {
        Stack<Control> panelStack = new Stack<Control>();
        //for spaceing between components
        //the gap between the table layout component
        int _tableLayoutColumn = 1;

        bool _showScientificButtons = true;
        int _totalScientificButtons = 0;

        private List<string> Expression = new List<string>();
        private string InputString = string.Empty;
        private string ResultString = string.Empty;

        List<ButtonData> _buttonDataList;
        List<ButtonData> _memoryButtonDataList = new List<ButtonData>();
        List<ButtonData> scientificButtonDataList = new List<ButtonData>();
        List<ButtonData> _standardButtonDataList = new List<ButtonData>();
        Evaluator evaluator;
        //ButtonTypeEnum _lastPressedButton = ButtonTypeEnum.OPERATION;

        int _parenthesisCount = 0;


        public MainForm()
        {
            //InitializeComponent();
            string buttonDataJsonString = File.ReadAllText("Properties/ButtonsDataJson.json");
            _buttonDataList = JsonConvert.DeserializeObject<List<ButtonData>>(buttonDataJsonString);
            FetchBasicDataFromJson();
            evaluator = new Evaluator();
            InitilizeUI();
        }

        private void FetchBasicDataFromJson()
        {
            int maxColumn = -1;
            foreach (var buttonData in _buttonDataList)
            {
                if (buttonData.GroupType != GroupTypeEnum.MEMORY)
                    maxColumn = Math.Max(maxColumn, buttonData.Column);
                switch (buttonData.GroupType)
                {
                    case GroupTypeEnum.MEMORY:
                        {
                            _memoryButtonDataList.Add(buttonData);
                            break;
                        }
					case GroupTypeEnum.SCIENTIFIC:
						{
							_totalScientificButtons++; 
                            _standardButtonDataList.Add(buttonData);
							break;
						}
                    default:
                        {
                            _standardButtonDataList.Add(buttonData);
                            break;
                        }
                }
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
            string expression = string.Empty;
            foreach (string text in Expression)
                expression += text;
            inputPanelTextBox.Text = expression + InputString;
            outputPanelTextBox.Text = ResultString;
        }

        private void EvaluateResult()
        {
            UpdateInputTextBox();
            try
            {
				string expression = string.Empty;
				foreach (string text in Expression)
					expression += text;
				double result = evaluator.Evaluate(expression + InputString);
                outputPanelTextBox.Text = result.ToString();
                Expression.Clear();
            }
            catch (Exception ex)
            {
                outputPanelTextBox.Text = ex.Message;
            }
        }

    }
}
