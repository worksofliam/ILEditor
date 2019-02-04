using System;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.UserTools;

namespace ILEditor.Forms
{
	public partial class SourceCompareSelect : Form
	{
		public SourceCompareSelect()
		{
			InitializeComponent();

			if (Editor.LastEditing == null || !(Editor.LastEditing.Tag is RemoteSource src))
				return;

			switch (src.GetFS())
			{
				case FileSystem.QSYS:
					newSourceBox.SetSource(src.GetLibrary(), src.GetObject(), src.GetName());
					oldSourceBox.SetSource("", src.GetObject(), src.GetName());

					break;
				case FileSystem.IFS:
					newSourceBox.SetSource(src.GetRemoteFile());
					newSourceBox.SetTab(src.GetFS());

					break;
			}
		}

		private void compareButton_Click(object sender, EventArgs e)
		{
			if (!newSourceBox.IsValid())
			{
				MessageBox.Show("New source information not valid.");

				return;
			}

			if (!oldSourceBox.IsValid())
			{
				MessageBox.Show("Old source information not valid.");

				return;
			}

			string newFile = "", oldFile = "";

			switch (newSourceBox.GetFS())
			{
				case FileSystem.IFS:
					newFile = IBMiUtils.DownloadFile(newSourceBox.GetIFSPath());

					break;
				case FileSystem.QSYS:
					newFile = IBMiUtils.DownloadMember(newSourceBox.GetLibrary(),
						newSourceBox.GetSPF(),
						newSourceBox.GetMember());

					break;
			}

			switch (oldSourceBox.GetFS())
			{
				case FileSystem.IFS:
					oldFile = IBMiUtils.DownloadFile(oldSourceBox.GetIFSPath());

					break;
				case FileSystem.QSYS:
					oldFile = IBMiUtils.DownloadMember(oldSourceBox.GetLibrary(),
						oldSourceBox.GetSPF(),
						oldSourceBox.GetMember());

					break;
			}

			if (newFile == "" || oldFile == "")
			{
				MessageBox.Show("Unable to download members.");

				return;
			}

			Editor.TheEditor.AddTool(new DiffView(newFile, oldFile));
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}