using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApplication
{
    partial class MainFrame
    {      
        Panel inputPanel;
        Panel navPanel;
        TextBox inputPanelTextBox;
        Panel outputPanel;
        TextBox outputPanelTextBox;
        int counter = 0;

        private void InitilizeUI()
        {
            //calculate the rows and column required for the tablelayout
            _tableLayoutRow = ButtonDataList.Count / _tableLayoutColumn;
            _tableLayoutRow += ButtonDataList.Count % _tableLayoutColumn != 0 ? 1 : 0;

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

            FlowLayoutPanel memoryButtonLayout = new FlowLayoutPanel();



            //Draw the Operators from the Json Library
            TableLayoutPanel buttonTableLayout = new TableLayoutPanel();
            buttonTableLayout.Dock = DockStyle.Fill;
            buttonTableLayout.ColumnCount = _tableLayoutColumn;
            for (int columnCount = 0; columnCount < _tableLayoutColumn; columnCount++)
                buttonTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / _tableLayoutColumn));
            buttonTableLayout.RowCount = _tableLayoutRow;
            for (int i = 0; i < _tableLayoutRow; i++)
                buttonTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / _tableLayoutRow));
            buttonTableLayout.AutoSize = true;
            AddButtonDataList(buttonTableLayout);
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

        private void AddButtonDataList(TableLayoutPanel layout)
        {
            foreach (var buttonData in ButtonDataList)
            {
                if (_showScientificButtons == true 
                    //&& buttonData.GroupType == GroupTypeEnum.SCIENTIFIC
                    )
                {
                    Button button = new Button();
                    //create custom ButtonDataList
                    button.Text = buttonData.Name;
                    //button.Click += new EventHandler(Button_click);
                    button.Dock = DockStyle.Fill;
                    layout.Controls.Add(button, buttonData.Column - 1, buttonData.Row - 1);
                }
            }
        }
    }
}
