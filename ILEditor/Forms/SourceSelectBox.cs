using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class SourceSelectBox : UserControl
	{
		public SourceSelectBox()
		{
			InitializeComponent();
		}

		public FileSystem GetFS()
		{
			return tabs.SelectedTab.Text == "Member" ? FileSystem.QSYS : FileSystem.IFS;
		}

		public string GetIFSPath()
		{
			return stmfPath.Text;
		}

		public string GetLibrary()
		{
			return lib.Text;
		}

		public string GetSPF()
		{
			return spf.Text;
		}

		public string GetMember()
		{
			return mbr.Text;
		}

		public void SetSource(string Lib, string Spf, string Mbr)
		{
			lib.Text = Lib;
			spf.Text = Spf;
			mbr.Text = Mbr;
		}

		public void SetSource(string Path)
		{
			stmfPath.Text = Path;
		}

		public void SetTab(FileSystem System)
		{
			switch (System)
			{
				case FileSystem.QSYS:
					tabs.SelectedIndex = 0;

					break;
				case FileSystem.IFS:
					tabs.SelectedIndex = 1;

					break;
			}
		}

		public bool IsValid()
		{
			switch (tabs.SelectedIndex)
			{
				case 0:

					if (!IBMiUtils.IsValueObjectName(lib.Text))
						return false;

					if (!IBMiUtils.IsValueObjectName(spf.Text))
						return false;

					if (!IBMiUtils.IsValueObjectName(mbr.Text))
						return false;

					break;

				case 1:

					if (spf.Text.Trim() != "")
						return false;

					break;
			}

			return true;
		}
	}
}