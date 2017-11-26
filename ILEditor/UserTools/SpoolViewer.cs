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

namespace ILEditor.UserTools
{
    public partial class SpoolViewer : UserControl
    {
        public SpoolViewer(string FileLoc)
        {
            InitializeComponent();
            spoolText.Text = File.ReadAllText(FileLoc);
        }
    }
}
