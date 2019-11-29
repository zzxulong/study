using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compnay
{
    public class JsEvent
    {
        public string MessageText { get; set; }
        public void ShowTest ()
        {
            //MessageBox.Show("this in c#.\n\r" );
            Program.MainForm.SwitchTab();
        }
        public void ShowTestArg (string ss)
        {
            //MessageBox.Show("收到Js参数的调用\n\r" + ss);
        }
    }
}
