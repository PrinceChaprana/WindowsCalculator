using System.Windows.Forms;
using System;
using System.Linq;
using System.Collections;

namespace CalculatorApplication
{
	public partial class MainForm
	{
		ButtonTypeEnum _lastPressedButtonData;
		int _openParenthesisCount = 0;
		double _memoryData = 0;
		bool _isMemorySaved = false;

		private void KeyPressed(object sender, KeyPressEventArgs e)
		{
			_optionFlowLayout.Visible = false;
			if (char.IsDigit(e.KeyChar))
			{
				InputString += e.KeyChar;
				_lastPressedButtonData = ButtonTypeEnum.OPERAND;
			}
			else
			{
				Expression.Add(e.KeyChar.ToString());
				_lastPressedButtonData = ButtonTypeEnum.OPERATOR;
			}
			UpdateInputTextBox();
		}
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
							if (pressedButton.Data.ButtonType == ButtonTypeEnum.EQUAL)
							{
								EvaluateResult();
								break;
							}
							if (ValidateInput(pressedButton) == true)
								PerformStandardOpeartion(pressedButton);
							else
								return;
							break;
						}
				}
			}

			_lastPressedButtonData = pressedButton.Data.ButtonType;
			label.Text = _lastPressedButtonData.ToString();
		}

		private bool ValidateInput(CalculatorButton pressedButton)
		{
			switch (pressedButton.Data.ButtonType)
			{
				case ButtonTypeEnum.OPERATOR:
					{
						if (_lastPressedButtonData == null || InputString == string.Empty)
						{
							if (pressedButton.Text != "-")
							{
								return false;
							}
							else
							{
								return true;
							}
						}
						else if (_lastPressedButtonData == ButtonTypeEnum.OPERATOR)
						{
							return false;
						}
						break;
					}
				case ButtonTypeEnum.OPERAND:
					{
						if (inputPanelTextBox.Text == "0")
						{
							inputPanelTextBox.Text = string.Empty;
							return true;
						}
						if (_lastPressedButtonData == ButtonTypeEnum.CLOSEDPARENTHASIS)
						{
							return false;
						}
						if (InputString.LastIndexOf('.') != -1 && pressedButton.Text == ".")
						{
							return false;
						}
						break;
					}
				case ButtonTypeEnum.FUNCTION:
					{
						if (_lastPressedButtonData == ButtonTypeEnum.OPERATOR)
						{
							Expression.Add(pressedButton.Text + "(");
							//Expression.Add("(");
							_openParenthesisCount++;
						}
						break;
					}
				case ButtonTypeEnum.CLOSEDPARENTHASIS:
					{
						if (_openParenthesisCount > 0)
						{
							_openParenthesisCount -= 1;
							return true;
						}
						return false;
					}
			}
			return true;
		}

		void AddOpenParenthesis()
		{
			_openParenthesisCount++;
			Expression.Add("(");
			_lastPressedButtonData = ButtonTypeEnum.OPENPARENTHASIS;
		}
		void AddClosedParenthesis()
		{
			if (_openParenthesisCount > 0)
			{
				_openParenthesisCount--;
				Expression.Add(")");
				_lastPressedButtonData = ButtonTypeEnum.CLOSEDPARENTHASIS;
			}
		}

		private void PerformStandardOpeartion(CalculatorButton pressedButton)
		{
			switch (pressedButton.Data.ButtonType)
			{
				case ButtonTypeEnum.OPERAND:
					{
						InputString += pressedButton.Text;
						break;
					}
				case ButtonTypeEnum.OPERATOR:
				case ButtonTypeEnum.CLOSEDPARENTHASIS:
					{
						Expression.Add(InputString);
						Expression.Add(pressedButton.Data.Symbol);
						InputString = string.Empty;
						break;
					}
				case ButtonTypeEnum.OPENPARENTHASIS:
					{
						_openParenthesisCount += 1;
						Expression.Add(pressedButton.Data.Symbol);
						break;
					}
			}
			UpdateInputTextBox();
		}

		private void PerformMemoryOpeartion(CalculatorButton pressedButton)
		{
			switch (pressedButton.Data.ButtonType)
			{
				case ButtonTypeEnum.MEMORYADD:
					{
						if (_isMemorySaved)
							_memoryData += double.Parse(InputString);
						break;
					}
				case ButtonTypeEnum.MEMORYSUBTRACT:
					{
						if (_isMemorySaved)
							_memoryData -= double.Parse(InputString);
						break;
					}
				case ButtonTypeEnum.MEMORYSAVE:
					{
						_isMemorySaved = true;
						if (InputString != string.Empty)
							_memoryData = double.Parse(InputString);
						break;
					}
				case ButtonTypeEnum.MEMORYREAD:
					{
						if (_isMemorySaved)
						{
							InputString = _memoryData.ToString();
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
						if (InputString.Length > 0)
						{
							InputString = InputString.Substring(0, InputString.Count() - 1);

						}
						else if (InputString.Length == 0)
						{
							//clear the last entry
							if (Expression.Count > 0)
							{
								if (Expression.Last().Equals("("))
									_openParenthesisCount--;
								else if (Expression.Last().Equals(")"))
									_openParenthesisCount++;
								Expression.RemoveAt(Expression.Count() - 1);

							}
						}
						break;
					}
				case ButtonTypeEnum.CLEAR:
					{
						InputString = string.Empty;
						_openParenthesisCount = 0;
						Expression.Clear();
						ResultString = string.Empty;
						break;
					}
				case ButtonTypeEnum.CLEARENTRY:
					{
						InputString = string.Empty;
						break;
					}
				case ButtonTypeEnum.EQUAL:
					{
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
	}
}
