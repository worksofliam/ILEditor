using ILEditor.Classes;
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
    public partial class UpdateProgram : Form
    {
        private bool isSrvPgm;
        public UpdateProgram(ILEObject Program, ILEObject[] Modules)
        {
            InitializeComponent();

            switch (Program.Type)
            {
                case "*PGM":
                    pgmTypeText.Text = "Program";
                    break;
                case "*SRVPGM":
                    pgmTypeText.Text = "Service Program";
                    break;
            }

            this.Text = "Update " + pgmTypeText.Text;
            pgm.Text = Program.Library + "/" + Program.Name;
            
            isSrvPgm = (Program.Type == "*SRVPGM");

            binderSrcBox.Enabled = isSrvPgm;
            bndLib.Text = Program.Library;
            bndLib.Text = "QSRVSRC";
            bndLib.Text = "*SRVPGM";

            foreach (ILEObject Object in Modules)
                if (Object.Type == "*MODULE")
                    modules.Items.Add(new ListViewItem(Object.Library + "/" + Object.Name, 0));
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            string cmd = "", result = "";
            List<string> ModuleList = new List<string>();
            List<string> BindingDirectories = new List<string>();

            foreach (ListViewItem item in modules.Items)
                if (item.Checked)
                    ModuleList.Add(item.Text);

            foreach (string item in customModules.Items)
                ModuleList.Add(item);

            foreach (string bnddir in bndDirs.Items)
                BindingDirectories.Add(bnddir);
            
            if (ModuleList.Count == 0)
                ModuleList.Add("*NONE");

            if (isSrvPgm)
            {
                cmd = "UPDSRVPGM SRVPGM(" + pgm.Text + ") MODULE(" + String.Join(" ", ModuleList) + ")";
                if (updSrvSrc.Checked)
                    cmd += " EXPORT(*CURRENT)";
                else
                {
                    cmd += " EXPORT(*SRCFILE)";
                    cmd += " SRCFILE(" + bndLib.Text + "/" + bndSpf.Text + ")";
                    cmd += " SRCMBR(" + bndMbr.Text + ")";
                }
            }
            else
            {
                cmd = "UPDPGM PGM(" + pgm.Text + ") MODULE(" + String.Join(" ", ModuleList) + ")";
            }

            if (BindingDirectories.Count > 0)
                cmd += " BNDDIR(" + String.Join(" ", BindingDirectories) + ")";

            actgrp.Text = actgrp.Text.Trim();
            if (actgrp.Text != "" && actgrp.Text != "*SAME")
                cmd += " ACTGRP(" + actgrp.Text + ")";

            result = IBMi.RemoteCommandResponse(cmd);

            if (result == "")
            {
                Editor.TheEditor.SetStatus(pgm.Text + " updated successfully.");
                this.Close();
            }
            else
                MessageBox.Show(result, "Error");
        }

        #region Modules
        private void addMod_Click(object sender, EventArgs e)
        {
            string module = customModule.Text.Trim();
            if (module != "")
            {
                if (!module.Contains("/"))
                    module = "*LIBL/" + module;

                if (!customModules.Items.Contains(module))
                    customModules.Items.Add(module);

                customModule.Text = "";
                customModule.Focus();
            }
        }

        private void customModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addMod.PerformClick();
        }

        private void delMod_Click(object sender, EventArgs e)
        {
            if (customModules.SelectedItem != null)
                customModules.Items.RemoveAt(customModules.SelectedIndex);
        }

        private void customModules_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                delMod.PerformClick();
        }
        #endregion

        private void updSrvSrc_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = !updSrvSrc.Checked;

            bndLib.Enabled = enabled;
            bndSpf.Enabled = enabled;
            bndMbr.Enabled = enabled;
        }

        #region Binding Directories
        private void addBnd_Click(object sender, EventArgs e)
        {
            string bndDirVal = bndDir.Text.Trim();
            if (bndDirVal != "")
            {
                if (!bndDirVal.Contains("/"))
                    bndDirVal = "*LIBL/" + bndDirVal;

                if (!bndDirs.Items.Contains(bndDirVal))
                    bndDirs.Items.Add(bndDirVal);

                bndDir.Text = "";
                bndDir.Focus();
            }
        }

        private void delBnd_Click(object sender, EventArgs e)
        {
            if (bndDirs.SelectedItem != null)
                bndDirs.Items.RemoveAt(bndDirs.SelectedIndex);
        }

        private void bndDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addBnd.PerformClick();
        }

        private void bndDirs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                delBnd.PerformClick();
        }
        #endregion

    }
}
