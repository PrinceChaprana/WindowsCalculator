using System.Collections.Generic;
using System.Windows.Forms;

namespace TesterWinForms
{
    public enum GroupTypeEnum
    {
        SCIENTIFIC,
        STANDARD,
        OPERATION,
        MEMORY
    }
    public class ButtonGroup
    {
        public GroupTypeEnum GroupName { get; set; }
        public List<ButtonInfo> buttonsList { get;set; }
    }
    public class ButtonInfo
    {
        public string ButtonText
        {
            get; set;
        }
        public string ButtonDescription { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class CalculatorButton : Button
    {
        ButtonInfo buttonInfo { get; set; }
        
        public CalculatorButton(ButtonInfo buttonInfo)
        {
            base.Text = buttonInfo.ButtonText;
        }
    }
}
