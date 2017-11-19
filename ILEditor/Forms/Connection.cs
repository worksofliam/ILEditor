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
using System.IO;

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

            cur_size.Text = IBMi.CurrentSystem.GetValue("ZOOM");
            indent_size.Value = decimal.Parse(IBMi.CurrentSystem.GetValue("INDENT_SIZE"));
            show_spaces.SelectedItem = IBMi.CurrentSystem.GetValue("SHOW_SPACES");
            highlight_line.SelectedItem = IBMi.CurrentSystem.GetValue("HIGHLIGHT_CURRENT_LINE");

            validACS.Checked = (File.ReadAllText(Program.ACSPATH) != "false");
        }

        private void save_Click(object sender, EventArgs e)
        {
            IBMi.CurrentSystem.SetValue("system", host.Text.Trim());
            IBMi.CurrentSystem.SetValue("username", user.Text.Trim());
            IBMi.CurrentSystem.SetValue("password", pass.Text.Trim());

            IBMi.CurrentSystem.SetValue("INDENT_SIZE", indent_size.Value.ToString());
            IBMi.CurrentSystem.SetValue("SHOW_SPACES", show_spaces.SelectedItem.ToString());
            IBMi.CurrentSystem.SetValue("HIGHLIGHT_CURRENT_LINE", highlight_line.SelectedItem.ToString());
            this.Close();
        }

        private void findACS_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "Applications (*.exe)|*.exe";
            openFile.ShowDialog();
            validACS.Checked = File.Exists(openFile.FileName);
            if (validACS.Checked)
            {
                File.WriteAllText(Program.ACSPATH, openFile.FileName);
            }
        }
    }
}
