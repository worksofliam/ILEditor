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
using ILEditor.Classes.LanguageTools;

namespace ILEditor.UserTools
{
    public partial class OutlineView : DockContent
    {
        public OutlineView()
        {
            InitializeComponent();
        }

        private string File;
        public void Display(string FileName, Function[] Functions)
        {
            if (Functions == null) return;

            int iconIndex = -1;
            List<TreeNode> total = new List<TreeNode>();
            TreeNode node;
            TreeNode varNode;
            File = FileName;

            foreach (Function function in Functions)
            {
                node = new TreeNode(function.GetName(), 0, 0);
                node.Tag = function.GetLineNumber();
                foreach (Variable var in function.GetVariables())
                {
                    iconIndex = -1;
                    switch (var.GetStorageType())
                    {
                        case StorageType.Const:
                            iconIndex = 1;
                            break;
                        case StorageType.File:
                            iconIndex = 2;
                            break;
                        case StorageType.Normal:
                            iconIndex = 3;
                            break;
                        case StorageType.Struct:
                            iconIndex = 4;
                            break;
                        case StorageType.Subroutine:
                            iconIndex = 5;
                            break;
                    }

                    varNode = new TreeNode(var.GetName() + " " + var.GetType(), iconIndex, iconIndex);
                    varNode.Tag = var.GetLine();
                    node.Nodes.Add(varNode);
                }
                total.Add(node);
            }

            funcList.Nodes.Clear();
            funcList.Nodes.AddRange(total.ToArray());
            funcList.ExpandAll();
        }

        private void funcList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int line;
            if (e.Node.Tag == null) { }
            else
            {
                line = (int) e.Node.Tag;
                onSelectError(this.File, line);
            }
        }

        private void onSelectError(string File, int Line)
        {
            DockContent theTab = Editor.TheEditor.GetTabByName(File, true);

            if (theTab != null)
            {
                SourceEditor SourceEditor = Editor.TheEditor.GetTabEditor(theTab);

                SourceEditor.Focus();
                SourceEditor.GotoLine(Line, 0);
            }
            else
            {
                MessageBox.Show("Unable to open error. Please open the source manually first and then try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (Editor.LastEditing != null)
            {
                Editor.LastEditing.DoAction(EditorAction.ParseCode);
                Editor.LastEditing.DoAction(EditorAction.OutlineUpdate);
            }
        }
    }
}
