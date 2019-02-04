using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class IFSManager : Form
	{
		public IFSManager()
		{
			InitializeComponent();

			foreach (var dir in IBMi.CurrentSystem.GetValue("IFS_LINKS").Split('|'))
				if (dir.Trim() != "")
					ifsDirs.Items.Add(dir);
		}

		private void addDir_Click(object sender, EventArgs e)
		{
			ifsDir.Text = ifsDir.Text.Trim();
			if (ifsDir.Text != "")
			{
				ifsDirs.Items.Add(ifsDir.Text);
				ifsDir.Text = "";
				ifsDir.Focus();
			}
		}

		private void delIfs_Click(object sender, EventArgs e)
		{
			if (ifsDirs.SelectedItem != null)
				ifsDirs.Items.RemoveAt(ifsDirs.SelectedIndex);
		}

		private void ifsDir_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				addDir.PerformClick();
		}

		private void ifsDirs_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				delIfs.PerformClick();
		}

		private void save_Click(object sender, EventArgs e)
		{
			var dirs = new List<string>();
			foreach (string dir in ifsDirs.Items)
				dirs.Add(dir);

			IBMi.CurrentSystem.SetValue("IFS_LINKS", string.Join("|", dirs));
			Close();
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}