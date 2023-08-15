using System.Windows.Forms;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CalculatorApplication
{
    public class ExpressionItem
    {
        public string Value { get; set; }
        public ButtonTypeEnum ButtonType { get; set; }

        public ExpressionItem(string value, ButtonTypeEnum buttonType)
        {
            Value = value;
            ButtonType = buttonType;
        }
    }

    public partial class MainForm
    {
        ButtonTypeEnum _lastPressedButtonData = ButtonTypeEnum.NONE;
        int _openParenthesisCount = 0;
        double _memoryData = 0;
        bool _isMemorySaved = false;

        List<ExpressionItem> _expressionItemList = new List<ExpressionItem>();

        private void MouseButtonClick(object sender, MouseEventArgs e)
        {
            _optionFlowLayout.Visible = false;
            CalculatorButton pressedButton = (CalculatorButton)sender;
            if (e.Button == MouseButtons.Left)
            {
                //Perform Operation based on type
                switch (pressedButton.Data.GroupType)
                {
                    case GroupTypeEnum.MEMORY:
                        {
                            PerformMemoryOpeartion(pressedButton);
                            break;
                        }
                    case GroupTypeEnum.OPERATION:
                        {
                            PerformOperation(pressedButton.Data);
                            break;
                        }
                    case GroupTypeEnum.SCIENTIFIC:
                    case GroupTypeEnum.STANDARD:
                        {            
                            PerformStandardOpeartion(pressedButton);
                            break;
                        }
                }
            }
        }

        void PerformStandardOpeartion(CalculatorButton pressedButton)
        {
            ButtonTypeEnum lastExpressionItemList = ButtonTypeEnum.NONE;
            if (_expressionItemList.Count != 0)
                lastExpressionItemList = _expressionItemList.Last().ButtonType;

            switch (pressedButton.Data.ButtonType)
            {
                case ButtonTypeEnum.EQUAL:
                    {
                        AddInputStringToExpression();
                        _inputPanelTextBox.Text += "=";
                        EvaluateResult();
                        break;
                    }
                case ButtonTypeEnum.OPERAND:
                    {
                        if (pressedButton.Text == ".")
                        {
                            if (_inputString.Contains("."))
                            {
                                break;
                            }
                        }
                        _inputString += pressedButton.Text;
                        lastExpressionItemList = ButtonTypeEnum.OPERAND;
                        break;
                    }
                case ButtonTypeEnum.SIGNCHANGE:
                    {
                        if (_inputString[0] == '-')
                        {
                            _inputString = _inputString.Substring(1);
                            break;
                        }
                        _inputString = "-" + _inputString;
                        break;
                    }
                case ButtonTypeEnum.OPERATOR:
                    {
                        if (_inputString.Length != 0 || _expressionItemList.Count != 0)
                        {

                            AddInputStringToExpression();
                            _expressionItemList.Add(new ExpressionItem(pressedButton.Text, ButtonTypeEnum.OPERATOR));
                        }
                        break;
                    }
                case ButtonTypeEnum.FUNCTION:
                    {
                        if (_expressionItemList.Count == 0
                            || lastExpressionItemList == ButtonTypeEnum.OPERATOR)
                        {
                            _expressionItemList.Add(new ExpressionItem(pressedButton.Text, ButtonTypeEnum.FUNCTION));
                            AddOpenParenthesis();
                            if (_inputString.Length != 0)
                                AddInputStringToExpression();
                        }
                        break;
                    }
                case ButtonTypeEnum.OPENPARENTHASIS:
                    {
                        AddOpenParenthesis();
                        break;
                    }
                case ButtonTypeEnum.CLOSEDPARENTHASIS:
                    {
                        if (_lastPressedButtonData == ButtonTypeEnum.OPERAND)
                        {
                            if (_inputString.Length > 0)
                                AddInputStringToExpression();
                            AddClosedParenthesis();
                        }
                        break;
                    }
            }
            UpdateInputTextBox();
            _lastPressedButtonData = pressedButton.Data.ButtonType;
        }
        void AddInputStringToExpression()
        {
            _expressionItemList.Add(new ExpressionItem(_inputString, ButtonTypeEnum.OPERAND));
            _inputString = string.Empty;
        }
        void AddOpenParenthesis()
        {
            _openParenthesisCount++;
            _expressionItemList.Add(new ExpressionItem("(", ButtonTypeEnum.OPENPARENTHASIS));
            if (_inputString.Length > 0)
            {
                AddInputStringToExpression();
            }
        }
        void AddClosedParenthesis()
        {
            if (_openParenthesisCount > 0)
            {
                _openParenthesisCount--;
                AddInputStringToExpression();
                _expressionItemList.Add(new ExpressionItem(")", ButtonTypeEnum.CLOSEDPARENTHASIS));
            }
        }
        private void PerformMemoryOpeartion(CalculatorButton pressedButton)
        {
            switch (pressedButton.Data.ButtonType)
            {
                case ButtonTypeEnum.MEMORYADD:
                    {
                        if (_isMemorySaved)
                            _memoryData += double.Parse(_inputString);
                        break;
                    }
                case ButtonTypeEnum.MEMORYSUBTRACT:
                    {
                        if (_isMemorySaved)
                            _memoryData -= double.Parse(_inputString);
                        break;
                    }
                case ButtonTypeEnum.MEMORYSAVE:
                    {
                        _isMemorySaved = true;
                        if (_inputString != string.Empty)
                            _memoryData = double.Parse(_inputString);
                        break;
                    }
                case ButtonTypeEnum.MEMORYREAD:
                    {
                        if (_isMemorySaved)
                        {
                            _inputString = _memoryData.ToString();
                            UpdateInputTextBox();
                        }
                        break;
                    }
                case ButtonTypeEnum.MEMORYCLEAR:
                    {
                        _memoryData = 0;
                        _isMemorySaved = false;
                        break;
                    }
            }
        }
        internal void PerformOperation(ButtonData data)
        {
            switch (data.ButtonType)
            {
                case ButtonTypeEnum.DELETE:
                    {
                        DeleteOperation();
                        break;
                    }
                case ButtonTypeEnum.CLEAR:
                    {
                        _inputString = string.Empty;
                        _openParenthesisCount = 0;
                        _expressionItemList.Clear();
                        break;
                    }
                case ButtonTypeEnum.CLEARENTRY:
                    {
                        _inputString = string.Empty;
                        break;
                    }
                case ButtonTypeEnum.TYPECHANGE:
                    {
                        //add custom change operator
                        _showScientificButtons = _showScientificButtons == true ? false : true;
                        RemoveButtonsFromTable();
                        break;
                    }
            }
            UpdateInputTextBox();
        }
        private void DeleteOperation()
        {
            if (_inputString.Length > 0)
            {
                _inputString = _inputString.Substring(0, _inputString.Count() - 1);

            }
            else if (_inputString.Length == 0)
            {
                //clear the last entry
                if (_expressionItemList.Count > 0)
                {
                    if (_expressionItemList.Last().Equals("("))
                        _openParenthesisCount--;
                    else if (_expressionItemList.Last().Equals(")"))
                        _openParenthesisCount++;
                    _expressionItemList.RemoveAt(_expressionItemList.Count() - 1);

                }
            }
        }
    }
}
