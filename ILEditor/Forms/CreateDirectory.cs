using System;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class CreateDirectory : Form
	{
		public CreateDirectory(string Path = "")
		{
			InitializeComponent();

			path.Text = Path;
		}

		private void open_Click(object sender, EventArgs e)
		{
			if (path.Text == "")
			{
				MessageBox.Show("Path cannot be blank");
			}
			else
			{
				if (!IBMi.DirExists(path.Text))
				{
					IBMi.CreateDirectory(path.Text);
					Close();
				}
				else
				{
					MessageBox.Show("Chosen path already exists.");
				}
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void CreateDirectory_Load(object sender, EventArgs e)
		{
			path.Focus();
		}
	}
}