using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.UserTools
{
    public partial class UserToolList : UserControl
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
                        Editor.TheEditor.AddTool("Member Browse", new MemberBrowse());
                        break;
                    case "ERR":
                        Editor.TheEditor.AddTool("Error Listing", new ErrorListing());
                        break;
                    case "LIBL":
                        new Forms.LibraryList().ShowDialog();
                        break;
                    case "CONN":
                        new Forms.Connection().ShowDialog();
                        break;
                    case "CMP":
                        Editor.TheEditor.AddTool("Compile Settings", new CompileOptions());
                        break;
                }
            }
        }
    }
}
