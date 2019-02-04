using System;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class SPFSelect : Form
	{
		public string Lib;
		public string Spf;

		public bool Successful;

		public SPFSelect()
		{
			InitializeComponent();
			Successful = false;
		}

		private void select_Click(object sender, EventArgs e)
		{
			var valid = true;
			Lib = lib.Text.Trim();
			Spf = spf.Text.Trim();

			if (!IBMiUtils.IsValueObjectName(Lib))
				valid = false;

			if (!valid)
			{
				MessageBox.Show("Member information not valid.");
			}
			else
			{
				Successful = true;
				Close();
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void loadAllCheck_CheckedChanged(object sender, EventArgs e)
		{
			spf.Enabled = !loadAllCheck.Checked;
		}
	}
}