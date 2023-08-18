using CalculatorClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class MainForm : Form
    {
        Stack<Control> _panelStack = new Stack<Control>();
        int _tableLayoutColumn = 1;
        bool _showScientificButtons = true;
        int _totalScientificButtons = 0;

        private List<string> _expression = new List<string>();
        string _expressionString = string.Empty;
        private string _inputString = string.Empty;

        List<ButtonData> _buttonDataList;
        List<ButtonData> _memoryButtonDataList = new List<ButtonData>();
        List<ButtonData> _standardButtonDataList = new List<ButtonData>();
        Evaluator evaluator;
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

        private void UpdateInputTextBox()
        {
            //all evaluator here
            _expressionString = string.Empty;
            foreach (var text in _expressionItemList)
            {
                if (text.Value.Contains("="))
                    continue;
                _expressionString += text.Value;
            }
            _inputPanelTextBox.Text = _expressionString;
            _outputPanelTextBox.Text = _inputString;
        }

        private void EvaluateResult()
        {
            UpdateInputTextBox();
            try
            {
                if (_openParenthesisCount > 0)
                {
                    _expression.Add(")");
                }
                double result = evaluator.Evaluate(_expressionString + _inputString);
                _outputPanelTextBox.Text = result.ToString();
                _inputString = result.ToString();
                _expression.Clear();
            }
            catch (Exception ex)
            {
                _outputPanelTextBox.Text = ex.Message;
            }
        }

    }
}
