using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class UpdateProgram : Form
	{
		private readonly bool isSrvPgm;

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

			Text     = "Update " + pgmTypeText.Text;
			pgm.Text = Program.Library + "/" + Program.Name;

			isSrvPgm = Program.Type == "*SRVPGM";

			binderSrcBox.Enabled = isSrvPgm;
			bndLib.Text          = Program.Library;
			bndLib.Text          = "QSRVSRC";
			bndLib.Text          = "*SRVPGM";

			foreach (var Object in Modules)
				if (Object.Type == "*MODULE")
					modules.Items.Add(new ListViewItem(Object.Library + "/" + Object.Name, 0));
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void update_Click(object sender, EventArgs e)
		{
			string cmd;
			var    moduleList         = new List<string>();
			var    bindingDirectories = new List<string>();

			foreach (ListViewItem item in modules.Items)
				if (item.Checked)
					moduleList.Add(item.Text);

			foreach (string item in customModules.Items)
				moduleList.Add(item);

			foreach (string bnddir in bndDirs.Items)
				bindingDirectories.Add(bnddir);

			if (moduleList.Count == 0)
				moduleList.Add("*NONE");

			if (isSrvPgm)
			{
				cmd = "UPDSRVPGM SRVPGM(" + pgm.Text + ") MODULE(" + string.Join(" ", moduleList) + ")";
				if (updSrvSrc.Checked)
				{
					cmd += " EXPORT(*CURRENT)";
				}
				else
				{
					cmd += " EXPORT(*SRCFILE)";
					cmd += " SRCFILE(" + bndLib.Text + "/" + bndSpf.Text + ")";
					cmd += " SRCMBR(" + bndMbr.Text + ")";
				}
			}
			else
			{
				cmd = "UPDPGM PGM(" + pgm.Text + ") MODULE(" + string.Join(" ", moduleList) + ")";
			}

			if (bindingDirectories.Count > 0)
				cmd += " BNDDIR(" + string.Join(" ", bindingDirectories) + ")";

			actgrp.Text = actgrp.Text.Trim();
			if (actgrp.Text != "" && actgrp.Text != "*SAME")
				cmd += " ACTGRP(" + actgrp.Text + ")";

			var result = IBMi.RemoteCommandResponse(cmd);

			if (result == "")
			{
				Editor.TheEditor.SetStatus(pgm.Text + " updated successfully.");
				Close();
			}
			else
			{
				MessageBox.Show(result, "Error");
			}
		}

		private void updSrvSrc_CheckedChanged(object sender, EventArgs e)
		{
			var enabled = !updSrvSrc.Checked;

			bndLib.Enabled = enabled;
			bndSpf.Enabled = enabled;
			bndMbr.Enabled = enabled;
		}

	#region Modules

		private void addMod_Click(object sender, EventArgs e)
		{
			var module = customModule.Text.Trim();
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

	#region Binding Directories

		private void addBnd_Click(object sender, EventArgs e)
		{
			var bndDirVal = bndDir.Text.Trim();

			if (bndDirVal == "")
				return;

			if (!bndDirVal.Contains("/"))
				bndDirVal = "*LIBL/" + bndDirVal;

			if (!bndDirs.Items.Contains(bndDirVal))
				bndDirs.Items.Add(bndDirVal);

			bndDir.Text = "";
			bndDir.Focus();
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