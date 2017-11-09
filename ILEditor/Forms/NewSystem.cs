using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ILEditor.Forms
{
    public partial class NewSystem : Form
    {
        public NewSystem()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (WriteFile(alias.Text.Trim(), host.Text.Trim(), user.Text.Trim(), pass.Text.Trim()))
                this.Close();
        }

        private Boolean WriteFile(string Alias, string Host, string User, string Pass)
        {
            Boolean Successful = false;
            string SystemPath = Program.SYSTEMSDIR + @"\" + Alias;
            string[] lines = new string[4];
            if (!File.Exists(SystemPath))
            {
                lines[0] = "alias=" + Alias;
                lines[1] = "system=" + Host;
                lines[2] = "username=" + User;
                lines[3] = "password=" + Pass;
                File.WriteAllLines(SystemPath, lines);
                Successful = true;
            }
            else
            {
                MessageBox.Show("Setup with same alias name already exists.");
                Successful = false;
            }

            return Successful;
        }
    }
}
