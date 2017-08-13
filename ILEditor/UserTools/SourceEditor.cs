using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Classes;
using FastColoredTextBoxNS;
using System.Runtime.InteropServices;

namespace ILEditor.UserTools
{
    public partial class SourceEditor : UserControl
    {

        public SourceEditor(String LocalFile)
        {
            InitializeComponent();

            //https://www.codeproject.com/Articles/10675/Enabling-syntax-highlighting-in-a-RichTextBox

            //https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting
            FastColoredTextBox mybox = new FastColoredTextBox();
            
            mybox.Dock = DockStyle.Fill;
            mybox.Text = File.ReadAllText(LocalFile);
            this.Controls.Add(mybox);
        }
    }
}
