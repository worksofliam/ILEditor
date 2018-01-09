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
using ILEditor.Classes;
using System.Diagnostics;

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
            if (WriteFile(alias.Text.Trim(), host.Text.Trim(), user.Text.Trim(), pass.Text.Trim(), ftpes.Checked.ToString().ToLower()))
                this.Close();
        }

        private Boolean WriteFile(string Alias, string Host, string User, string Pass, string useFTPES)
        {
            Pass = Password.Encode(Pass);
            Boolean Successful = false;
            string SystemPath = Program.SYSTEMSDIR + @"\" + Alias;
            string[] lines = new string[5];
            if (!File.Exists(SystemPath))
            {
                lines[0] = "alias=" + Alias;
                lines[1] = "system=" + Host;
                lines[2] = "username=" + User;
                lines[3] = "password=" + Pass;
                lines[4] = "useFTPes=" + useFTPES;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www-01.ibm.com/support/docview.wss?uid=nas8N1014798");
        }
    }
}
