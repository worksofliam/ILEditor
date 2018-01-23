using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms.ProjectWindows
{
    public partial class BuildResult : UserControl
    {
        public BuildResult(string[] Lines)
        {
            InitializeComponent();
            textListing.Items.AddRange(Lines);
        }
    }
}
