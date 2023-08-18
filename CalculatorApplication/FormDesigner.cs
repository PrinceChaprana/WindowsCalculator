using CalculatorApplication.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApplication
{
    partial class MainForm
    {
        Panel _navPanel;
        TextBox _inputPanelTextBox;
        TextBox _outputPanelTextBox;
        Label label;
        TableLayoutPanel _buttonTableLayout = new TableLayoutPanel();

        Panel _optionFlowLayout;

        private void InitilizeUI()
        {
            //NavBar Panel
            _navPanel = new Panel();
            _navPanel.Dock = DockStyle.Top;
            _navPanel.BackColor = Color.Beige;
            _navPanel.Height = 40;
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
            _navPanel.Controls.Add(helpButton);
            _navPanel.Controls.Add(exitButton);
            _navPanel.Controls.Add(editButton);
            //Controls.Add(_navPanel);
            _panelStack.Push(_navPanel);


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

            _optionFlowLayout.Size = new Size(copyButton.Width, 2 * copyButton.Height);
            _optionFlowLayout.Controls.Add(pasteButton);
            _optionFlowLayout.Controls.Add(copyButton);
            Controls.Add(_optionFlowLayout);



            //textbox
           
            _inputPanelTextBox = new TextBox();
            _inputPanelTextBox.Height = 40;
            _inputPanelTextBox.BorderStyle = BorderStyle.None;
            _inputPanelTextBox.ReadOnly = true;
            _inputPanelTextBox.Dock = DockStyle.Top;
            _inputPanelTextBox.Text = "0";
            _inputPanelTextBox.TextAlign = HorizontalAlignment.Right;
            _inputPanelTextBox.Font = new Font("Cascadia Mono", 20.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //Controls.Add(inputPanel);

            _panelStack.Push(_inputPanelTextBox);

            _outputPanelTextBox = new TextBox();
            _outputPanelTextBox.Height = 40;
            _outputPanelTextBox.BorderStyle = BorderStyle.None;
            _outputPanelTextBox.ReadOnly = true;
            _outputPanelTextBox.Dock = DockStyle.Top;
            _outputPanelTextBox.TextAlign = HorizontalAlignment.Right;
            _outputPanelTextBox.Font = new Font("Cascadia Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _panelStack.Push(_outputPanelTextBox);

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

            _panelStack.Push(memoryButtonLayout);

            label = new Label();
            label.Dock = DockStyle.Top;

            _panelStack.Push(label);

            AddButtonToLayout(_buttonTableLayout, _standardButtonDataList);
            _panelStack.Push(_buttonTableLayout);

            //setting MainWindow 
            AutoSize = true;
            BackColor = Color.FromArgb(238, 238, 255);
            //read Keyboard press
            KeyPreview = true;
            KeyDown += new KeyEventHandler(FormKeyDown);
            //dont work without this
            //KeyPress += new KeyPressEventHandler(KeyPressed);
            //Draw all the panels
            DrawPanel();

        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                DeleteOperation();
            }

            if (e.KeyCode == Keys.S && ModifierKeys == Keys.Control )
            {
                _expression.Add("+");
            }

            if (char.IsDigit((char)e.KeyValue))
            {
                _inputString += ((char)e.KeyValue).ToString();
                _lastPressedButtonData = ButtonTypeEnum.OPERAND;
            }
            UpdateInputTextBox();
        }

        private void ExitButtonClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void PasteButtonClicked(object sender, EventArgs e)
        {
            _optionFlowLayout.Visible = false;
            _expressionString = Clipboard.GetText();
            _inputPanelTextBox.Text = _expressionString;
        }

        private void CopyButtonClicked(object sender, EventArgs e)
        {
            _optionFlowLayout.Visible = false;
            UpdateInputTextBox();
            Clipboard.SetText(_expressionString);
        }

        private void ShowEditOptions(object sender, EventArgs e)
        {
            if (_optionFlowLayout.Visible == true)
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
                cutout = _totalScientificButtons / _tableLayoutColumn;
            }

            int _tableRow = buttonDataList.Count / _tableLayoutColumn;
            _tableRow += buttonDataList.Count % _tableLayoutColumn > 0 ? 1 : 0;

            float _rowPercentSpace = 100F / _tableRow;
            float _columnPercentSpace = 100F / _tableLayoutColumn;

            layout.ColumnCount = _tableLayoutColumn;
            //Add ColumnStyles
            for (int column = 0; column < _tableLayoutColumn; column++)
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, _columnPercentSpace));
            layout.RowCount = _tableRow - cutout;
            for (int rowCount = 0; rowCount < _tableRow; rowCount++)
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, _rowPercentSpace));
            layout.Dock = DockStyle.Fill;
            layout.AutoSize = true;
            layout.BackColor = Color.Red;
            int startRow = -1;
            Update();

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
            for (int i = 0; i < _buttonTableLayout.RowCount; i++)
            {
                for (int j = 0; j < _buttonTableLayout.ColumnCount; j++)
                {
                    CalculatorButton button = (CalculatorButton)_buttonTableLayout.GetControlFromPosition(j, i);
                    _buttonTableLayout.Controls.Remove(button);

                }
            }
            
            AddButtonToLayout(_buttonTableLayout, _standardButtonDataList);
        }
        private void DrawPanel()
        {
            while (_panelStack.Count > 0)
            {
                Controls.Add(_panelStack.Pop());

            }
        }
    }
}
