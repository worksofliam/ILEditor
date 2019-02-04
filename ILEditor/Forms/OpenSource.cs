using System;
using System.Linq;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class OpenSource : Form
	{
		public static string Library = "", SPF = "", Stmf = "";

		public OpenSource(int tab = 0)
		{
			InitializeComponent();
			type.Items.AddRange(Editor.LangTypes.Keys.ToArray());

			tabs.SelectedIndex = tab;

			lib.Text      = Library;
			spf.Text      = SPF;
			stmfPath.Text = Stmf;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var isValid = true;

			switch (tabs.SelectedIndex)
			{
				case 0:
					if (!IBMiUtils.IsValueObjectName(lib.Text))
						isValid = false;

					if (!IBMiUtils.IsValueObjectName(spf.Text))
						isValid = false;

					if (!IBMiUtils.IsValueObjectName(mbr.Text))
						isValid = false;

					if (isValid)
					{
						Editor.OpenSource(new RemoteSource("", lib.Text, spf.Text, mbr.Text, type.Text, true));
						Library = lib.Text;
						SPF     = spf.Text;
						Close();
					}
					else
					{
						MessageBox.Show("Member information not valid.");
					}

					break;

				case 1:
					stmfPath.Text = stmfPath.Text.Trim();

					if (IBMi.FileExists(stmfPath.Text))
					{
						Editor.OpenSource(new RemoteSource("", stmfPath.Text));
						Stmf = stmfPath.Text;
						Close();
					}
					else
					{
						MessageBox.Show("Chosen file does not exist.");
					}

					break;
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void tabs_SelectedIndexChanged(object sender, EventArgs e)
		{
			open.Text = "Open " + tabs.SelectedTab.Text;
		}
	}
}