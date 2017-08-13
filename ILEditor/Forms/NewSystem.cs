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
            if (WriteFile(host.Text.Trim(), user.Text.Trim(), pass.Text.Trim()))
                this.Close();
        }

        private Boolean WriteFile(string Host, string User, string Pass)
        {
            Boolean Successful = false;
            string SystemPath = Program.SYSTEMSDIR + @"\" + Host;
            string[] lines = new string[3];
            if (!File.Exists(SystemPath))
            {
                lines[0] = "system=" + Host;
                lines[1] = "username=" + User;
                lines[2] = "password=" + Pass;
                File.WriteAllLines(SystemPath, lines);
                Successful = true;
            }
            else
            {
                MessageBox.Show("System with same host already exists.");
                Successful = false;
            }

            return Successful;
        }
    }
}
