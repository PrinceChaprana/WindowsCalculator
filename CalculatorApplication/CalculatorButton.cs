using System.Windows.Forms;

namespace CalculatorApplication
{
    internal class CalculatorButton : Button
    {
        public ButtonData Data { get; set; }
        public ToolTip ToolTip { get; set; }

        public CalculatorButton(ButtonData data)
        {
            Data = data;
            base.Text = data.Symbol;
            base.Dock = DockStyle.Fill;         
            ToolTip = new ToolTip();
            ToolTip.SetToolTip(this, data.ButtonDescription);
        }
	}
}
