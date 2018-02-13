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
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
    public partial class SpoolViewer : DockContent
    {
        public SpoolViewer(string Title, string FileLoc)
        {
            InitializeComponent();
            this.Text = Title;

            spoolText.Text = File.ReadAllText(FileLoc);
        }
    }
}
