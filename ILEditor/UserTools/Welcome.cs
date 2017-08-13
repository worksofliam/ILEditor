using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ILEditor.UserTools
{
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();
            webBrowser1.DocumentText = Properties.Resources.welcome;
        }
    }
}
