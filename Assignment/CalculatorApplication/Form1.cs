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
