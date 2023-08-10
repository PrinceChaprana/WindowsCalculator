using CalculatorApplication.Properties;
using CalculatorClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class MainFrame : Form
    {
        public Stack<Control> panelStack = new Stack<Control>();
        //for spaceing between components
        //the gap between the table layout component
        int _totalColumn = 4;

        private string Expression;
        private string InputString = "";
        private string ResultString = "";

        List<ButtonData> buttons;
        Evaluator evaluator;
        ButtonTypeEnum _lastPressedButton = ButtonTypeEnum.OPERATION;

        int _parenthesisCount = 0;


        Panel navPanel;
        Panel inputPanel;
        TextBox inputPanelTextBox; 
        Panel outputPanel;
        TextBox outputPanelTextBox;
        int counter = 0;

        public MainFrame()
        {
            //InitializeComponent();
            string json = File.ReadAllText("Properties/ButtonsDataJson.json");
            buttons = JsonConvert.DeserializeObject<List<ButtonData>>(json);
            evaluator = new Evaluator();
            InitilizeUI();
        }

        private void InitilizeUI()
        {   
            //NavBar Panel
            navPanel = new Panel();
            navPanel.Dock = DockStyle.Top;
            navPanel.Height = 40;
            navPanel.BackColor = Color.Red;
            //Controls.Add(navPanel);
            panelStack.Push(navPanel);

            //textbox
            
            inputPanel = new Panel();
            inputPanel.Dock = DockStyle.Top;
            inputPanel.Height = 40;
            inputPanelTextBox = new TextBox();
            inputPanelTextBox.BorderStyle = BorderStyle.None;
            inputPanelTextBox.ReadOnly = true;
            inputPanelTextBox.Dock = DockStyle.Top;
            inputPanelTextBox.TextAlign = HorizontalAlignment.Right;
            inputPanelTextBox.Font = new System.Drawing.Font("Cascadia Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            outputPanelTextBox.Font = new System.Drawing.Font("Cascadia Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            outputPanel.Controls.Add(outputPanelTextBox);
            panelStack.Push(outputPanel);
                        

            //Draw the Operators from the Json Library
            TableLayoutPanel buttonTableLayout = new TableLayoutPanel();
            buttonTableLayout.Dock = DockStyle.Top;
            buttonTableLayout.RowCount = buttons.Count/_totalColumn + 1;
            buttonTableLayout.ColumnCount = _totalColumn;
            buttonTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonTableLayout.AutoSize = true;
            AddButtons(buttonTableLayout);
            panelStack.Push(buttonTableLayout);


            //setting MainWindow 
            AutoSize = true;

            //Draw all the panels
            DrawPanel();

        }

        private void DrawPanel()
        {
            while (panelStack.Count > 0)
            {
                Controls.Add(panelStack.Pop());
                
            }
        }

        private void AddButtons(TableLayoutPanel layout)
        {
            foreach(var buttonData in buttons)
            {
                Button button = new Button();
                button.Text = buttonData.ButtonInfo.Name;
                button.Click += new EventHandler(Button_click);
                button.Dock = DockStyle.Fill;
                layout.Controls.Add(button,buttonData.ButtonInfo.Column-1,buttonData.ButtonInfo.Row-1);
            }            
        }

        //eventargs kya
        private void Button_click(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            string buttonName = button.Text;
            ButtonData buttonData = GetButtonData(buttonName);
            //modify content based of operation
            //make these in background thread
            if(_lastPressedButton == ButtonTypeEnum.EQUAL)
                ResetCalculator();

            switch (buttonData.ButtonType)
            {
                case ButtonTypeEnum.EQUAL:
                    {
                        EvaluateResult();
                        break;
                    }
                case ButtonTypeEnum.OPERATION:
                    {
                        PerformOperation(buttonData.OpeartorSymbol);
                        break;
                    }
                case ButtonTypeEnum.OPENPARENTHASIS:
                    {
                        if(_lastPressedButton == ButtonTypeEnum.OPERAND)
                        {
                            InputString += "*";
                        }
                        _parenthesisCount += 1;
                        UpdateInputExpression(buttonData.OpeartorSymbol);
                        break;
                    }
                case ButtonTypeEnum.CLOSEDPARENTHASIS:
                    {
                        if(_parenthesisCount <= 0)
                        {
                            return;
                        }
                        _parenthesisCount -= 1;
                        UpdateInputExpression(buttonData.OpeartorSymbol);
                        break;
                    }
                case ButtonTypeEnum.FUNCTION:
                    {
                        _parenthesisCount += 1;
                        UpdateInputExpression(buttonData.OpeartorSymbol + "(");
                        break;
                    }
                case ButtonTypeEnum.OPERAND:
                    {
                        UpdateInputExpression(buttonData.OpeartorSymbol);
                        break;
                    }
                case ButtonTypeEnum.OPERATOR:
                    {
                        UpdateInputExpression(buttonData.OpeartorSymbol); 
                        break;
                    }
                
            }
            UpdateInputTextBox();
            _lastPressedButton = buttonData.ButtonType;
            
        }

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

        private void PerformOperation(string opeartorSymbol)
        {
            if(opeartorSymbol == "del" && InputString.Length > 0)
            {
                InputString = InputString.Substring(0,InputString.Length - 1);
                inputPanelTextBox.Text = InputString;                
            }
            else if(opeartorSymbol == "CE")
            {
                ResetCalculator();
                ResultString = "";
                outputPanelTextBox.Text = ResultString;
            }else if(opeartorSymbol == "C")
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
            foreach(var button in buttons)
            {
                if (buttonName.Equals(button.ButtonInfo.Name))
                {
                    return button;
                }
            }
            return null;
        }        
    }
}
