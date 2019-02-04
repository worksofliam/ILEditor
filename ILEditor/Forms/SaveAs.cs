using System;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class SaveAs : Form
	{
		public bool Success;

		public SaveAs(RemoteSource MemberInfo = null)
		{
			InitializeComponent();

			if (MemberInfo == null)
				return;

			switch (MemberInfo.GetFS())
			{
				case FileSystem.IFS:
					sourceSelectBox.SetSource(MemberInfo.GetRemoteFile());
					sourceSelectBox.SetSource("", "", MemberInfo.GetName());

					break;
				case FileSystem.QSYS:
					sourceSelectBox.SetSource("", MemberInfo.GetObject(), MemberInfo.GetName());

					break;
			}
		}

		public SourceSelectBox SourceInfo()
		{
			return sourceSelectBox;
		}

		private void save_Click(object sender, EventArgs e)
		{
			if (!sourceSelectBox.IsValid())
			{
				MessageBox.Show("Source information not valid.");
			}
			else
			{
				Success = true;
				Close();
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}