using System;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.UserTools;
using WeifenLuo.WinFormsUI.Docking;

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
			var isValid = true;

			lib.Text = lib.Text.Trim();
			spf.Text = spf.Text.Trim();

			if (!IBMiUtils.IsValueObjectName(lib.Text))
				isValid = false;

			else if (!IBMiUtils.IsValueObjectName(spf.Text))
				isValid = false;

			if (isValid)
			{
				if (IBMi.IsConnected)
				{
					var cmd = "CRTSRCPF FILE(" +
					          lib.Text +
					          "/" +
					          spf.Text +
					          ") RCDLEN(" +
					          rcdLen.Value +
					          ") CCSID(" +
					          ccsid.Text +
					          ")";

					if (IBMi.RemoteCommand(cmd))
					{
						Editor.TheEditor.AddTool(new MemberBrowse(lib.Text, spf.Text), DockState.DockRight);
						Close();
					}
					else
					{
						MessageBox.Show(lib.Text.Trim() + "/" + spf.Text.Trim() + " not created.");
					}
				}
				else
				{
					Directory.CreateDirectory(IBMiUtils.GetLocalDir(lib.Text, spf.Text));
					Editor.TheEditor.AddTool(new MemberBrowse(lib.Text, spf.Text), DockState.DockRight);
				}
			}
			else
			{
				MessageBox.Show("SPF information not valid.");
			}
		}
	}
}