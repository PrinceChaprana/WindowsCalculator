using CalculatorApplication.Properties;
using CalculatorClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CalculatorApplication
{
    public partial class MainFrame : Form
    {
        public Stack<Control> panelStack = new Stack<Control>();
        int _minWindowWidth = 420;
        //default the window has space for the labels each of 40 unit height
        int _minWindowHeight = 160;
        int _minButtonSize = 40;


        //for spaceing between components
        int _offset = 12;
        //the gap between the table layout component
        int _gapOffset = 6;
        int _totalColumn = 3;
        int _minNumbersButtonRows = 3;
        int _numbersRows = 4;

        //rows for different 
        int operatorRows;

        public MainFrame()
        {
            //InitializeComponent();
            InitilizeUI();
        }

        private void InitilizeUI()
        {
            string pathName = "Properties/OperatorJson.json";
            var jsonData = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText(pathName));            

            

            //offset for showing last
            //_minWindowHeight += 50;
            

            //NavBar Panel
            Panel navPanel = new Panel();
            navPanel.Dock = DockStyle.Top;
            navPanel.Height = 40;
            navPanel.BackColor = Color.Red;
            panelStack.Push(navPanel);

            //Output TextBox Panel
            for (int count = 1; count <= 3; count++)
            {
                switch (count)
                {
                    case 3:
                        CreateTextBox(Resources.Expression);
                        continue;
                    case 2:
                        CreateTextBox(Resources.Input);
                        continue;
                    case 1:
                        CreateTextBox(Resources.Result);
                        continue;
                }
            }

            //Draw the Operators from the Json Library
            TableLayoutPanel operatorTableLayout = new TableLayoutPanel();
            operatorTableLayout.Dock = DockStyle.Top;
            operatorTableLayout.RowCount = operatorRows;
            operatorTableLayout.ColumnCount = _totalColumn;
            operatorTableLayout.Size = new Size(_minWindowWidth, spaceforOperator );
            AddOperatorButtons(jsonData, operatorTableLayout);
            panelStack.Push(operatorTableLayout);

            //Draw Number Operands
            TableLayoutPanel numberTableLayout = new TableLayoutPanel();
            numberTableLayout.Dock = DockStyle.Top;
            numberTableLayout.RowCount = operatorRows;
            numberTableLayout.ColumnCount = _totalColumn;
            numberTableLayout.Size = new Size(_minWindowWidth, _spaceForNumbers);
            AddOperandButtons(numberTableLayout);
            panelStack.Push(numberTableLayout);

            _minWindowHeight += numberTableLayout.Height;

            //calculate the dimension of the windows
            //total height required for showing operators
            int operatorCount = jsonData.Count;
            operatorRows = operatorCount / _totalColumn + 1;
            int spaceforOperator = operatorRows * (_minButtonSize + operatorTableLayout.Padding.Top + operatorTableLayout.Margin.Top);
            _minWindowHeight += spaceforOperator;

            //total height required for showing the numbers            
            int _spaceForNumbers = _numbersRows * (_minButtonSize + numberTableLayout.Padding.Top + numberTableLayout.Margin.Top);
            _minWindowHeight += _spaceForNumbers;

            //setting MainWindow 
            this.Width = _minWindowWidth;
            this.Height = _minWindowHeight;

            //Draw all the panels
            DrawPanel();
           
        }

        private void DrawPanel()
        {
            while(panelStack.Count > 0)
            {
                Controls.Add(panelStack.Pop());
            }
        }

        private void AddOperandButtons(TableLayoutPanel layout)
        {
            int number = 1;
            for (int row = 0; row < _numbersRows; row++)
            {
                for (int column = 0; column < _totalColumn; column++)
                {
                    Button b = new Button();
                    b.Size = new Size((_minWindowWidth / _totalColumn) - _offset, _minButtonSize);
                    b.Text = (number % 11).ToString();
                    layout.Controls.Add(b, column, row);
                    number++;
                }
            }
        }

        private void AddOperatorButtons(List<OperatorData> operatorData,TableLayoutPanel layout)
        {
            int count = operatorData.Count() - 1;
            for(int row=0; row < operatorRows; row++)
            {
                for(int column=0; column < _totalColumn; column++)
                {
                    if (count < 0) return;
                    Button b = new Button();
                    b.Size = new Size((_minWindowWidth / _totalColumn) - _offset ,_minButtonSize);
                    b.Text = operatorData[count--].OperatorSymbol;
                    layout.Controls.Add(b,column,row);
                }
            }
        }

        private void CreateTextBox(string v)
        {
            Panel expressionPanel = new Panel();
            expressionPanel.Dock = DockStyle.Top;
            expressionPanel.Height = 40;
            expressionPanel.BackColor = Color.Yellow;
            Label expressionLabel = new Label();
            expressionLabel.Text = v;
            expressionPanel.Controls.Add(expressionLabel);
            TextBox expressionTextBox = new TextBox();
            expressionTextBox.ReadOnly = true;
            expressionTextBox.Dock = DockStyle.Top;
            expressionTextBox.TextAlign = HorizontalAlignment.Right;
            expressionTextBox.Font = new System.Drawing.Font("Cascadia Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            expressionPanel.Controls.Add(expressionTextBox);
            panelStack.Push(expressionPanel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
