using System;
using System.Windows.Forms;

namespace ILEditor.Forms
{
	public partial class PasswordPrompt : Form
	{
		public bool Success;

		public PasswordPrompt(string System, string User)
		{
			InitializeComponent();
			userText.Text += " " + User + "@" + System;
		}

		public string GetResult()
		{
			return password.Text;
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void select_Click(object sender, EventArgs e)
		{
			if (password.Text != "")
			{
				Success = true;
				Close();
			}
		}
	}
}