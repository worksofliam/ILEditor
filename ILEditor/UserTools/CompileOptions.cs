using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Forms.CompileOptionForms;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
    public partial class CompileOptions : DockContent
    {
        public CompileOptions()
        {
            InitializeComponent();
            this.Text = "Compile Options";
        }

        private void CompileOptions_Load(object sender, EventArgs e)
        {
            reloadConfig();
        }

        private void reloadConfig()
        {
            commandList.Clear();

            string[] Headers = IBMi.CurrentSystem.GetValue("CMPTYPES").Split('|');
            string[] Commands;
            int imageIndex = 0;

            foreach (string Header in Headers)
            {
                commandList.Groups.Add(new ListViewGroup(Header, Header));
                Commands = IBMi.CurrentSystem.GetValue("TYPE_" + Header).Split('|');
                foreach (string Command in Commands)
                {
                    if (Command == "") continue;
                    imageIndex = (IBMi.CurrentSystem.GetValue("DFT_" + Header) == Command ? 1 : 0);
                    commandList.Items.Add(new ListViewItem(Command, imageIndex, commandList.Groups[Header]));
                }
            }
        }

        private void newType_Click(object sender, EventArgs e)
        {
            new NewType().ShowDialog();
            reloadConfig();
        }

        private void newCommand_Click(object sender, EventArgs e)
        {
            new EditCommand().ShowDialog();
            reloadConfig();
        }

        #region RightClick
        private void commandList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightClick.Show(Cursor.Position);
            }
        }

        private void setDefaultForTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (commandList.SelectedItems.Count > 0)
            {
                ListViewItem item = commandList.SelectedItems[0];
                IBMi.CurrentSystem.SetValue("DFT_" + item.Group.Name, item.Text);
                reloadConfig();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (commandList.SelectedItems.Count > 0)
            {
                ListViewItem item = commandList.SelectedItems[0];
                new EditCommand(item.Group.Name, item.Text).ShowDialog();
                reloadConfig();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (commandList.SelectedItems.Count > 0)
            {
                ListViewItem item = commandList.SelectedItems[0];
                if (IBMi.CurrentSystem.GetValue("DFT_" + item.Group.Name) != item.Text)
                {
                    List<string> Commands = IBMi.CurrentSystem.GetValue("TYPE_" + item.Group.Name).Split('|').ToList();
                    Commands.Remove(item.Text);
                    IBMi.CurrentSystem.SetValue("TYPE_" + item.Group.Name, String.Join("|", Commands));
                    reloadConfig();
                }
                else
                {
                    MessageBox.Show("Unable to delete command setting which is currently the default for a group.");
                }
            }
        }
        #endregion
    }
}
