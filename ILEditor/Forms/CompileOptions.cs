using ILEditor.Classes;
using ILEditor.Forms.CompileOptionForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class CompileOptions : Form
    {
        public CompileOptions()
        {
            InitializeComponent();
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
    }
}
