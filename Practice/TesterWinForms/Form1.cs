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

namespace TesterWinForms
{
    public partial class Form1 : Form
    {
        string[] arrays;
        string input;

        public Form1()
        {
            //InitializeComponent();           

            List<ButtonGroup> json = JsonConvert.DeserializeObject<List<ButtonGroup>>(File.ReadAllText("Properties/buttongroup.json"));
            foreach (ButtonGroup button in json)
            {
                TableLayoutPanel groupPanel = new TableLayoutPanel();
                groupPanel.ColumnCount = 3;
                groupPanel.RowCount = button.buttonsList.Count;
                groupPanel.Dock = DockStyle.Top;
                foreach(ButtonInfo info in  button.buttonsList)
                {
                    CalculatorButton b = new CalculatorButton(info);
                    groupPanel.Controls.Add(b);
                }
                Controls.Add(groupPanel);
            }

            //Controls.Add(AddButton3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input += "hello";
            //textBox1.Text = input;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
