using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                ListViewItem selection = toollist.SelectedItems[0];
                switch ((String)selection.Tag)
                {
                    case "MBR":
                        Editor.TheEditor.AddTool(new MemberBrowse(), DockState.DockRight);
                        break;
                    case "LIBL":
                        new Forms.LibraryList().ShowDialog();
                        break;
                    case "CONN":
                        new Forms.Connection().ShowDialog();
                        break;
                    case "CMP":
                        Editor.TheEditor.AddTool(new CompileOptions(), DockState.DockLeft);
                        break;
                    case "PGM":
                        Editor.TheEditor.AddTool(new ObjectBrowse(), DockState.DockLeft);
                        break;
                    case "TREE":
                        Editor.TheEditor.AddTool(new QSYSBrowser(), DockState.DockRight);
                        break;
                    case "SPL":
                        Editor.TheEditor.AddTool(new SpoolListing(), DockState.DockRight, true);
                        break;
                    case "IFS":
                        Editor.TheEditor.AddTool(new IFSBrowser(), DockState.DockRight);
                        break;
                }
            }
        }
    }
}
