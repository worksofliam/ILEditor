using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Forms.ProjectWindows;

namespace ILEditor.UserTools
{
    public partial class ProjectExplorer : UserControl
    {
        public ProjectExplorer()
        {
            InitializeComponent();
        }

        private void newProject_ButtonClick(object sender, EventArgs e)
        {
            new NewProjectWindow().ShowDialog();
        }
    }
}
