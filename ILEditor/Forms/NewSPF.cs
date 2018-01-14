using ILEditor.Classes;
using ILEditor.UserTools;
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
    public partial class NewSPF : Form
    {
        public NewSPF()
        {
            InitializeComponent();
        }

        private void create_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            lib.Text = lib.Text.Trim();
            spf.Text = spf.Text.Trim();

            if (!IBMiUtils.IsValueObjectName(lib.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(spf.Text))
                isValid = false;

            if (isValid)
            {
                string cmd = "CRTSRCPF FILE(" + lib.Text.Trim() + "/" + spf.Text.Trim() + ") RCDLEN(" + rcdLen.Value.ToString() + ") CCSID(" + ccsid.Text + ")";
                if (IBMi.RemoteCommand(cmd) == false)
                {
                    Editor.TheEditor.AddTool("Member Browse", new MemberBrowse(lib.Text, spf.Text));
                    this.Close();
                }
                else
                {
                    MessageBox.Show(lib.Text.Trim() + "/" + spf.Text.Trim() + " not created.");
                }
            }
            else
                MessageBox.Show("SPF information not valid.");

        }
    }
}
