﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            input += "hello";
            textBox1.Text = input;

        }
    }
}
