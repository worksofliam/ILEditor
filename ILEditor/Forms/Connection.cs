using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
    public partial class Connection : Form
    {
        public Connection()
        {
            InitializeComponent();

            host.Text = IBMi.CurrentSystem.GetValue("system");
            user.Text = IBMi.CurrentSystem.GetValue("username");
            pass.Text = IBMi.CurrentSystem.GetValue("password");
        }

        private void save_Click(object sender, EventArgs e)
        {
            IBMi.CurrentSystem.SetValue("system", host.Text.Trim());
            IBMi.CurrentSystem.SetValue("username", user.Text.Trim());
            IBMi.CurrentSystem.SetValue("password", pass.Text.Trim());
        }
    }
}
