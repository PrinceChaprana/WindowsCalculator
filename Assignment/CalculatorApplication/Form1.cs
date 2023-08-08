using CalculatorApplication.Properties;
using CalculatorClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            //InitializeComponent();
            InitilizeUI();
        }

        private void InitilizeUI()
        {
            string pathName = Path.Combine(Environment.CurrentDirectory, "Properties/OperatorJson.json");

            var jsonData = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText(pathName));

            //Main form decleration

            //nav bar panel
            Panel navPanel = new Panel();
            navPanel.Dock = DockStyle.Top;
            navPanel.Height = 40;
            navPanel.BackColor = Color.Red;

            TableLayoutPanel numberTableLayout = new TableLayoutPanel();
            numberTableLayout.Dock = DockStyle.Top;
            numberTableLayout.RowCount = 4;
            numberTableLayout.ColumnCount = 3;
            numberTableLayout.Size = new Size(379, 531);

            AddOperandButtons(numberTableLayout);


            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Top;
            List<OperatorData> _jsonData = JsonConvert.DeserializeObject<List<OperatorData>>(File.ReadAllText("Properties/OperatorJson.json"));

            tableLayoutPanel.RowCount = (_jsonData.Count()/3) + 1;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.Size = new Size(379, 260);

            AddOperatorButtons(_jsonData,tableLayoutPanel);
            //expression Panel for showing expression
            

            Controls.Add(numberTableLayout);
            Controls.Add(tableLayoutPanel);
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
            Controls.Add(navPanel);
            

        }

        private void AddOperandButtons(TableLayoutPanel numberTableLayout)
        {
            int count = 10;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    if (count == 10)
                        button.Text = 0.ToString();
                    else
                    {
                        button.Text = count.ToString();
                    }
                    button.Size = new Size(80, 80);
                    numberTableLayout.Controls.Add(button,j,i);
                    count--;
                }
            }
        }

        private void AddOperatorButtons(List<OperatorData> operatorData,TableLayoutPanel layout)
        {
            int count = operatorData.Count() - 1;
            for(int j = 0; j < layout.RowCount; j++) { 
                for(int i=0;i<layout.ColumnCount;i++) 
                {
                    Button btn = new Button();
                    if (count < 0) break;
                    btn.Text = operatorData[count--].OperatorSymbol;
                    btn.Dock = DockStyle.Fill;
                    btn.Size = new Size(80, 60);

                    layout.Controls.Add(btn, j, i);
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
            Controls.Add(expressionPanel);
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
    }
}
