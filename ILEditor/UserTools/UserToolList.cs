using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditorPlugin;

namespace ILEditor.UserTools
{
    public partial class UserToolList : UserControl
    {
        public UserToolList()
        {
            InitializeComponent();
        }

        private void UserToolList_Load(object sender, EventArgs e)
        {
            ListViewItem PluginItem;
            foreach(IPlugin Plugin in Plugins.GetTools())
            {
                PluginItem = new ListViewItem(Plugin.DisplayName, 8);
                PluginItem.Tag = Plugin.Name;
                toollist.Items.Add(PluginItem);
            }
        }

        private void toollist_DoubleClick(object sender, EventArgs e)
        {
            if (toollist.SelectedItems.Count == 1)
            {
                ListViewItem selection = toollist.SelectedItems[0];
                string Option = (String)selection.Tag;
                switch (Option)
                {
                    case "MBR":
                        Editor.TheEditor.AddTool("Member Browse", new MemberBrowse());
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
                    case "PGM":
                        Editor.TheEditor.AddTool("Program Listing", new ObjectBrowse());
                        break;
                    case "TREE":
                        Editor.TheEditor.AddTool("Tree Browser", new TreeBrowse());
                        break;
                    case "SPL":
                        Editor.TheEditor.AddTool("Spool Listing", new SpoolListing(), true);
                        break;
                    default:
                        Plugins.GetPlugin(Option).OnPluginSelected();
                        break;
                }
            }
        }
    }
}
