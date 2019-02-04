using System;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class CreateStreamFile : Form
	{
		public RemoteSource Result;

		public CreateStreamFile(string PrePath = "")
		{
			InitializeComponent();

			stmfPath.Text = PrePath;
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void open_Click(object sender, EventArgs e)
		{
			stmfPath.Text = stmfPath.Text.Trim();

			if (stmfPath.Text.Length == 0)
			{
				MessageBox.Show("Path cannot be blank");
			}
			else
			{
				if (IBMi.FileExists(stmfPath.Text))
				{
					MessageBox.Show("Chosen path already exists.");
				}
				else
				{
					var fileTemp = Path.Combine(IBMiUtils.GetLocalDir("IFS"), Path.GetFileName(stmfPath.Text));
					File.Create(fileTemp).Close();
					Result = new RemoteSource(fileTemp, stmfPath.Text);
					Close();
				}
			}
		}

		private void CreateStreamFile_Load(object sender, EventArgs e)
		{
			stmfPath.Focus();
		}
	}
}