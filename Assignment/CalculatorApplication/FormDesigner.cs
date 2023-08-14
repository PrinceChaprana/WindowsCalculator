using CalculatorApplication.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApplication
{
	//layout row change is are hardcoded
	partial class MainForm
	{
		Panel inputPanel;
		Panel navPanel;
		TextBox inputPanelTextBox;
		Panel outputPanel;
		TextBox outputPanelTextBox;
		Label label;
		TableLayoutPanel scientificButtonLayout = new TableLayoutPanel();
		TableLayoutPanel buttonTableLayout = new TableLayoutPanel();

		Panel _optionFlowLayout;

		private void InitilizeUI()
		{
			//NavBar Panel
			navPanel = new Panel();
			navPanel.Dock = DockStyle.Top;
			navPanel.BackColor = Color.Beige;
			navPanel.Height = 40;
			Button editButton = new Button();
			editButton.Text = Resource.EDIT;
			editButton.Click += new EventHandler(ShowEditOptions);
			editButton.Dock = DockStyle.Left;
			editButton.FlatStyle = FlatStyle.Flat;
			editButton.FlatAppearance.BorderSize = 0;

			Button exitButton = new Button(); 
			exitButton.Text = Resource.EXIT;
			exitButton.Click += new EventHandler(ExitButtonClicked);
			exitButton.Dock = DockStyle.Left;
			exitButton.FlatStyle = FlatStyle.Flat;
			exitButton.FlatAppearance.BorderSize = 0;

			Button helpButton = new Button();
			helpButton.Text = Resource.HELP;
			helpButton.Click += new EventHandler(ExitButtonClicked);
			helpButton.Dock = DockStyle.Left;
			helpButton.FlatStyle = FlatStyle.Flat;
			helpButton.FlatAppearance.BorderSize = 0;
			navPanel.Controls.Add(helpButton);
			navPanel.Controls.Add(exitButton);
			navPanel.Controls.Add(editButton);
			//Controls.Add(navPanel);
			panelStack.Push(navPanel);

			
			_optionFlowLayout = new Panel();
			_optionFlowLayout.BackColor = DefaultBackColor;
			_optionFlowLayout.AutoSize = true;
			_optionFlowLayout.Location = new Point(editButton.Location.X + (editButton.Width / 2), editButton.Location.Y + editButton.Height);

			Button copyButton = new Button();
			copyButton.Text = Resource.COPY;
			copyButton.FlatStyle = FlatStyle.Flat;
			copyButton.Dock = DockStyle.Top;
			copyButton.FlatAppearance.BorderSize = 0;
			copyButton.Click += new EventHandler(CopyButtonClicked);

			Button pasteButton = new Button();
			pasteButton.Text = Resource.PASTE;
			pasteButton.Dock = DockStyle.Top;
			pasteButton.FlatStyle = FlatStyle.Flat;
			pasteButton.FlatAppearance.BorderSize = 0;
			pasteButton.Click += new EventHandler(PasteButtonClicked);
			_optionFlowLayout.Visible = false;

			_optionFlowLayout.Size = new Size(copyButton.Width,2*copyButton.Height);
			_optionFlowLayout.Controls.Add(pasteButton);
			_optionFlowLayout.Controls.Add(copyButton);
			Controls.Add(_optionFlowLayout);

			

			//textbox
			inputPanel = new Panel();
			inputPanel.Dock = DockStyle.Top;
			inputPanel.Height = 40;
			inputPanelTextBox = new TextBox();
			inputPanelTextBox.BorderStyle = BorderStyle.None;
			inputPanelTextBox.ReadOnly = true;
			inputPanelTextBox.Dock = DockStyle.Top;
			inputPanelTextBox.Text = "0";
			inputPanelTextBox.TextAlign = HorizontalAlignment.Right;
			inputPanelTextBox.Font = new Font("Cascadia Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			inputPanel.Controls.Add(inputPanelTextBox);
			//Controls.Add(inputPanel);

			panelStack.Push(inputPanel);

			outputPanel = new Panel();
			outputPanel.Dock = DockStyle.Top;
			outputPanel.Height = 40;
			outputPanelTextBox = new TextBox();
			outputPanelTextBox.BorderStyle = BorderStyle.None;
			outputPanelTextBox.ReadOnly = true;
			outputPanelTextBox.Dock = DockStyle.Top;
			outputPanelTextBox.TextAlign = HorizontalAlignment.Right;
			outputPanelTextBox.Font = new Font("Cascadia Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			outputPanel.Controls.Add(outputPanelTextBox);
			panelStack.Push(outputPanel);

			//memory buttons
			FlowLayoutPanel memoryButtonLayout = new FlowLayoutPanel();
			memoryButtonLayout.FlowDirection = FlowDirection.LeftToRight;
			memoryButtonLayout.Dock = DockStyle.Top;
			memoryButtonLayout.AutoSize = true;
			foreach (ButtonData memoryButtonData in _memoryButtonDataList)
			{
				CalculatorButton memoryButton = new CalculatorButton(memoryButtonData);
				memoryButton.Dock = DockStyle.None;
				memoryButton.BackColor = Color.LightGray;
				memoryButton.FlatStyle = FlatStyle.Flat;
				memoryButton.FlatAppearance.BorderSize = 0;
				memoryButton.MouseClick += new MouseEventHandler(MouseButtonClick);
				memoryButtonLayout.Controls.Add(memoryButton);
			}

			panelStack.Push(memoryButtonLayout);

			label = new Label();
			label.Dock = DockStyle.Top;

			panelStack.Push(label);

			AddButtonToLayout(buttonTableLayout, _standardButtonDataList);
			panelStack.Push(buttonTableLayout);

			//setting MainWindow 
			AutoSize = true;
			BackColor = Color.FromArgb(238,238,255);
			//read Keyboard press
			//KeyDown += new KeyEventHandler(KeyPressed);
			Focus();
			KeyPress += new KeyPressEventHandler(KeyPressed);
			//Draw all the panels
			DrawPanel();

		}

		private void ExitButtonClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void PasteButtonClicked(object sender, EventArgs e)
		{
			_optionFlowLayout.Visible = false;
		}

		private void CopyButtonClicked(object sender, EventArgs e)
		{
			_optionFlowLayout.Visible = false;
		}

		private void ShowEditOptions(object sender, EventArgs e)
		{
			if(_optionFlowLayout.Visible == true)
				_optionFlowLayout.Visible = false;
			else
				_optionFlowLayout.Show();
		}

		private void AddButtonToLayout(TableLayoutPanel layout, List<ButtonData> buttonDataList)
		{
			//calculate Number of rows required by the layout
			int cutout = 0;
			if (_showScientificButtons == false)
			{
				cutout = _totalScientificButtons/_tableLayoutColumn;
			}

			int _tableRow = buttonDataList.Count / _tableLayoutColumn;
			_tableRow += buttonDataList.Count % _tableLayoutColumn > 0 ? 1 : 0;

			float _rowPercentSpace = 100F / _tableRow;
			float _columnPercentSpace = 100F / _tableLayoutColumn;

			layout.ColumnCount = _tableLayoutColumn;
			//Add ColumnStyles
			for (int column = 0; column < _tableLayoutColumn; column++)
				layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _columnPercentSpace));
			layout.RowCount = _tableRow;
			for (int rowCount = 0; rowCount < _tableRow; rowCount++)
				layout.RowStyles.Add(new RowStyle(SizeType.Percent, _rowPercentSpace));
			layout.Dock = DockStyle.Fill;
			layout.AutoSize = true;
			int startRow = -1;

			//Add Buttons
			foreach (ButtonData buttonData in buttonDataList)
			{
				if (startRow == -1)
				{
					startRow = buttonData.Row;
				}
				if (_showScientificButtons == false && buttonData.GroupType == GroupTypeEnum.SCIENTIFIC)
				{
					continue;
				}

				CalculatorButton _calculatorButton = new CalculatorButton(buttonData);
				_calculatorButton.MouseClick += new MouseEventHandler(MouseButtonClick);
				_calculatorButton.BackColor = Color.LightGray;
				_calculatorButton.FlatStyle = FlatStyle.Flat;
				_calculatorButton.FlatAppearance.BorderSize = 0;
				layout.Controls.Add(_calculatorButton, buttonData.Column - 1, buttonData.Row - startRow - cutout);
			}
		}
		private void RemoveButtonsFromTable()
		{
			for (int i = 0; i < buttonTableLayout.RowCount; i++)
			{
				for (int j = 0; j < buttonTableLayout.ColumnCount; j++)
				{
					CalculatorButton button = (CalculatorButton)buttonTableLayout.GetControlFromPosition(j, i);					
					buttonTableLayout.Controls.Remove(button);

				}
			}
			buttonTableLayout.RowCount -= 2;
			AddButtonToLayout(buttonTableLayout, _standardButtonDataList);
		}
		private void DrawPanel()
		{
			while (panelStack.Count > 0)
			{
				Controls.Add(panelStack.Pop());

			}
		}
	}
}
