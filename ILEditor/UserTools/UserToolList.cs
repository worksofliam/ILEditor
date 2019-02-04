using System;
using ILEditor.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class UserToolList : DockContent
	{
		public UserToolList()
		{
			InitializeComponent();
		}

		private void toollist_DoubleClick(object sender, EventArgs e)
		{
			if (toollist.SelectedItems.Count == 1)
			{
				var selection = toollist.SelectedItems[0];
				switch ((string) selection.Tag)
				{
					case "MBR":
						Editor.TheEditor.AddTool(new MemberBrowse(), DockState.DockRight);

						break;
					case "LIBL":
						new JobSettings().ShowDialog();

						break;
					case "CONN":
						new Connection().ShowDialog();

						break;
					case "CMP":
						Editor.TheEditor.AddTool(new CompileOptions(), DockState.DockLeft);

						break;
					case "PGM":
						Editor.TheEditor.AddTool(new ObjectBrowse(), DockState.DockLeft);

						break;
					case "TREE":
						Editor.TheEditor.AddTool(new QsysBrowser(), DockState.DockRight);

						break;
					case "SPL":
						Editor.TheEditor.AddTool(new SpoolListing(), DockState.DockRight, true);

						break;
					case "IFS":
						Editor.TheEditor.AddTool(new IFSBrowser(), DockState.DockRight);

						break;
					case "OBJDIAG":
						new FindReferences().ShowDialog();

						break;
				}
			}
		}
	}
}