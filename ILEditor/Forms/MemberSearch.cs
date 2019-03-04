using System;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.UserTools;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.Forms
{
	public partial class MemberSearch : Form
	{
		public MemberSearch()
		{
			InitializeComponent();
		}

		private void openClone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new CloneWindow().ShowDialog();
		}

		private void search_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(searchVal.Text))
			{
				searchVal.Focus();
				MessageBox.Show("Search value cannot be blank.");

				return;
			}

			if (!IBMiUtils.IsValueObjectName(lib.Text))
			{
				lib.Focus();
				MessageBox.Show("Library name is not valid.");

				return;
			}

			if (!IBMiUtils.IsValueObjectName(spf.Text))
			{
				spf.Focus();
				MessageBox.Show("SPF name is not valid.");

				return;
			}

			Editor.TheEditor.AddTool(new MemberSearchListing(lib.Text, spf.Text, searchVal.Text, caseSense.Checked),
				DockState.DockBottom);

			Close();
		}
	}
}