using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class NewSystem : Form
	{
		public NewSystem()
		{
			InitializeComponent();
		}

		private void save_Click(object sender, EventArgs e)
		{
			alias.Text = alias.Text.Trim();
			host.Text  = host.Text.Trim();
			user.Text  = user.Text.Trim();

			if (alias.Text == string.Empty)
			{
				MessageBox.Show("Alias name cannot be blank.");

				return;
			}

			if (host.Text == string.Empty)
			{
				MessageBox.Show("Host name cannot be blank.");

				return;
			}

			if (user.Text == string.Empty)
			{
				MessageBox.Show("Username cannot be blank.");

				return;
			}

			if (WriteFile(alias.Text, host.Text, user.Text, pass.Text, ftpes.Checked.ToString().ToLower()))
				Close();
		}

		private static bool WriteFile(string Alias, string Host, string User, string Pass, string useFtpes)
		{
			if (Pass != string.Empty)
				Pass = Password.Encode(Pass);

			var systemPath = Program.SYSTEMSDIR + @"\" + Alias;

			if (File.Exists(systemPath))
			{
				MessageBox.Show("Setup with same alias name already exists.");

				return false;
			}

			var lines = new string[5];
			lines[0] = "alias=" + Alias;
			lines[1] = "system=" + Host;
			lines[2] = "username=" + User;
			lines[3] = "password=" + Pass;
			lines[4] = "useFTPes=" + useFtpes;
			File.WriteAllLines(systemPath, lines);

			return true;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www-01.ibm.com/support/docview.wss?uid=nas8N1014798");
		}
	}
}