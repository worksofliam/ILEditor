using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Forms;
using ILEditor.Classes;
using System.Threading;

namespace ILEditor.UserTools
{
    public partial class TreeBrowse : UserControl
    {
        public TreeBrowse()
        {
            InitializeComponent();
        }

        private SPFSelect window;

        private void addItem(string value)
        {
            List<string> Items = IBMi.CurrentSystem.GetValue("TREE_LIST").Split('|').ToList();
            if (!Items.Contains(value))
            {
                if (AddSPF(value))
                {
                    Items.Add(value);
                    IBMi.CurrentSystem.SetValue("TREE_LIST", String.Join("|", Items));
                }
            }
        }

        private Boolean AddSPF(string Value)
        {
            Boolean added = false;
            string[] path;
            TreeNode lib, spf;

            Value = Value.ToUpper();
            path = Value.Split('/');

            if (IBMiUtils.IsValueObjectName(path[0]) && IBMiUtils.IsValueObjectName(path[1]))
            {
                if (objectList.Nodes.ContainsKey(path[0]))
                    lib = objectList.Nodes[path[0]];
                else
                {
                    lib = new TreeNode(path[0]);
                    lib.Name = path[0];
                    lib.ImageIndex = 1;
                    lib.SelectedImageIndex = lib.ImageIndex;
                    objectList.Nodes.Add(lib);
                }

                if (!lib.Nodes.ContainsKey(path[0]))
                {
                    spf = new TreeNode(path[1]);
                    spf.Name = path[1];
                    spf.Tag = String.Join("/", Value);
                    spf.ImageIndex = 2;
                    spf.SelectedImageIndex = spf.ImageIndex;
                    spf.Nodes.Add("Loading..");
                    //assign tag here also
                    lib.Nodes.Add(spf);
                    added = true;
                }
            }

            return added;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string value = "";
            window = new SPFSelect();
            window.ShowDialog();
            if (window.Successful)
            {
                if (window.Spf != "")
                {
                    value = (window.Lib + "/" + window.Spf).ToUpper();
                    addItem(value);
                }
                else
                {
                    Thread gothread = new Thread((ThreadStart)delegate
                    {
                        ILEObject[] Objects = IBMiUtils.GetSPFList(window.Lib);
                        if (Objects != null)
                        {
                            foreach (ILEObject Obj in Objects)
                            {
                                value = (Obj.Library + "/" + Obj.Name).ToUpper();

                                this.Invoke((MethodInvoker)delegate
                                {
                                    addItem(value);
                                });
                            }
                        }
                    });
                    gothread.Start();
                }
            }
        }
        
        private void requestRefresh(TreeNode node)
        {
            TreeNode mbr;
            List<TreeNode> items;
            string[] path;

            if (node.Tag is string)
            {
                Thread gothread = new Thread((ThreadStart)delegate
                {
                    path = node.Tag.ToString().Split('/');
                    items = new List<TreeNode>();
                    Member[] members = IBMiUtils.GetMemberList(path[0], path[1]);

                    if (members != null)
                    {
                        foreach (Member member in members)
                        {
                            mbr = new TreeNode(member.GetMember() + (member.GetExtension() == "" ? "" : "." + member.GetExtension().ToLower()) + (member.GetText() == "" ? "" : " - " + member.GetText()));
                            mbr.Tag = member;
                            mbr.ImageIndex = 3;
                            mbr.SelectedImageIndex = mbr.ImageIndex;
                            items.Add(mbr);
                        }

                        if (members.Length == 0)
                        {
                            items.Add(new TreeNode("No members found."));
                        }
                    }
                    else
                    {
                        items.Add(new TreeNode("No members found."));
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        node.Nodes.Clear();
                        node.Nodes.AddRange(items.ToArray());
                    });
                });

                gothread.Start();
            }
        }

        private void objectList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            requestRefresh(node);
        }

        private void objectList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null) { }
            else
            {
                if (e.Node.Tag is Member)
                {
                    Member member = (Member)e.Node.Tag;
                    Editor.OpenMember(member);
                }
            }
        }

        private void TreeBrowse_Load(object sender, EventArgs e)
        {
            List<string> Items = IBMi.CurrentSystem.GetValue("TREE_LIST").Split('|').ToList();

            foreach(string Item in Items)
            {
                if (Item == "") continue;
                AddSPF(Item);
            }
        }

        private void objectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (objectList.SelectedNode != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (objectList.SelectedNode.Tag != null)
                        {
                            string path = objectList.SelectedNode.Tag.ToString();
                            if (path.Contains("/"))
                            {
                                var confirmResult = MessageBox.Show("Are you sure to delete this shortcut?",
                                                 "Delete shortcut",
                                                 MessageBoxButtons.YesNo);

                                if (confirmResult == DialogResult.Yes)
                                {
                                    List<string> Items = IBMi.CurrentSystem.GetValue("TREE_LIST").Split('|').ToList();
                                    Items.Remove(path);
                                    IBMi.CurrentSystem.SetValue("TREE_LIST", String.Join("|", Items));
                                    objectList.Nodes.Remove(objectList.SelectedNode);
                                }
                            }
                        }
                        break;
                    case Keys.F5:
                        if (objectList.SelectedNode.Tag == null)
                        {
                            foreach(TreeNode node in objectList.SelectedNode.Nodes)
                                requestRefresh(node);
                        }
                        else if (objectList.SelectedNode.Tag is string)
                        {
                            requestRefresh(objectList.SelectedNode);
                        }
                        break;
                }
            }
        }
    }
}
