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
            string password = "";
            InitializeComponent();

            host.Text = IBMi.CurrentSystem.GetValue("system");
            user.Text = IBMi.CurrentSystem.GetValue("username");

            password = IBMi.CurrentSystem.GetValue("password");
            password = Password.Decode(password);
            pass.Text = password;

            selectedFont.SelectedItem = IBMi.CurrentSystem.GetValue("FONT");
            cur_size.Text = IBMi.CurrentSystem.GetValue("ZOOM");
            indent_size.Value = decimal.Parse(IBMi.CurrentSystem.GetValue("INDENT_SIZE"));
            show_spaces.SelectedItem = IBMi.CurrentSystem.GetValue("SHOW_SPACES");
            highlight_line.SelectedItem = IBMi.CurrentSystem.GetValue("HIGHLIGHT_CURRENT_LINE");

            prntLib.Text = IBMi.CurrentSystem.GetValue("printerLib");
            prntObj.Text = IBMi.CurrentSystem.GetValue("printerObj");

            validACS.Checked = (Program.Config.GetValue("acspath") != "false");
            darkMode.Checked = (Program.Config.GetValue("darkmode") == "true");
        }

        private void save_Click(object sender, EventArgs e)
        {
            string password = "";
            IBMi.CurrentSystem.SetValue("system", host.Text.Trim());
            IBMi.CurrentSystem.SetValue("username", user.Text.Trim());
            password = pass.Text.Trim();
            password = Password.Encode(password);
            IBMi.CurrentSystem.SetValue("password", password);

            IBMi.CurrentSystem.SetValue("FONT", selectedFont.SelectedItem.ToString());
            IBMi.CurrentSystem.SetValue("INDENT_SIZE", indent_size.Value.ToString());
            IBMi.CurrentSystem.SetValue("SHOW_SPACES", show_spaces.SelectedItem.ToString());
            IBMi.CurrentSystem.SetValue("HIGHLIGHT_CURRENT_LINE", highlight_line.SelectedItem.ToString());

            IBMi.CurrentSystem.SetValue("printerLib", prntLib.Text);
            IBMi.CurrentSystem.SetValue("printerObj", prntObj.Text);

            Program.Config.SetValue("darkmode", darkMode.Checked.ToString().ToLower());
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
                Program.Config.SetValue("acspath", openFile.FileName);
            }
        }
    }
}
