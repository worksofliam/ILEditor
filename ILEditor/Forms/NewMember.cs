using System;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class NewMember : Form
	{
		public bool   IsCreated;
		public string Lib, Mbr, Spf, Type;
		public string MemberText;

		public NewMember(string Lib = "", string Spf = "")
		{
			InitializeComponent();

			lib.Text = Lib;
			spf.Text = Spf;
		}

		private void create_Click(object sender, EventArgs e)
		{
			var isValid = true;

			if (!IBMiUtils.IsValueObjectName(lib.Text))
				isValid = false;

			else if (!IBMiUtils.IsValueObjectName(spf.Text))
				isValid = false;

			else if (!IBMiUtils.IsValueObjectName(mbr.Text))
				isValid = false;

			if (!isValid)
			{
				MessageBox.Show("Provided member information not valid.",
					"Invalid member.",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);

				return;
			}

			Lib  = lib.Text.Trim();
			Spf  = spf.Text.Trim();
			Mbr  = mbr.Text.Trim();
			Type = type.Text.Trim() == "" ? "*NONE" : type.Text.Trim();

			if (IBMi.IsConnected)
			{
				MemberText = text.Text.Trim() == "" ? "*BLANK" : "'" + text.Text.Trim() + "'";

				var command = "ADDPFM FILE(" +
				              Lib +
				              "/" +
				              Spf +
				              ") MBR(" +
				              Mbr +
				              ") TEXT(" +
				              MemberText +
				              ") SRCTYPE(" +
				              Type +
				              ")";

				if (IBMi.RemoteCommand(command)) //No error
					Close();
				else
					MessageBox.Show("Member not created.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				if (Type == "*NONE")
					Type = "";

				var local = IBMiUtils.GetLocalFile(Lib, Spf, Mbr, Type);

				if (!File.Exists(local))
				{
					File.Create(local).Close();
					IsCreated = true;
					Close();
				}
				else
				{
					MessageBox.Show("Local member not created as already exists.",
						"Warning",
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation);
				}
			}
		}
	}
}