using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ILEditor.Classes;
using FluentFTP;
using ILEditor.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
    public partial class IFSBrowser : DockContent
    {
        private string RootDirectory = "";
        public IFSBrowser()
        {
            InitializeComponent();
            this.Text = "IFS Browser";
            RootDirectory = IBMi.CurrentSystem.GetValue("homeDir");
        }

        private void IFSBrowser_Load(object sender, EventArgs e)
        {
            Boolean exists;
            TreeNode node;
            if (IBMi.IsConnected())
            {
                new Thread((ThreadStart)delegate
                {
                    exists = IBMi.DirExists(RootDirectory);
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (exists)
                        {
                            node = new TreeNode(RootDirectory, new[] { new TreeNode("Loading..", 2, 2) });
                            node.Tag = RootDirectory;
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 0;
                            files.Nodes.Add(node);
                        }
                        else
                            files.Nodes.Add(new TreeNode("Home directory (" + RootDirectory + ") does not exist.", 3, 3));
                    });
                }).Start();
            }
            else
            {
                files.Nodes.Add(new TreeNode("IFS Browsing only works when connected to the remote system.", 3, 3));
            }
        }

        private void files_AfterExpand(object sender, TreeViewEventArgs e)
        {
            List<TreeNode> Listing = new List<TreeNode>();
            TreeNode node;

            new Thread((ThreadStart)delegate
            {
                FtpListItem[] items = IBMi.GetListing(e.Node.Tag.ToString());

                foreach (FtpListItem item in items)
                {
                    node = new TreeNode(item.Name);
                    node.Tag = item.FullName;
                    switch (item.Type)
                    {
                        case FtpFileSystemObjectType.Directory:
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 0;
                            node.Nodes.Add(new TreeNode("Loading..", 2, 2));
                            Listing.Add(node);
                            break; 
                        case FtpFileSystemObjectType.File:
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                            Listing.Add(node);
                            break;
                    }
                }

                if (Listing.Count() == 0)
                    Listing.Add(new TreeNode("Directory is empty.", 2, 2));

                this.Invoke((MethodInvoker)delegate
                {
                    e.Node.Nodes.Clear();
                    e.Node.Nodes.AddRange(Listing.ToArray());
                });
            }).Start();
        }

        private void files_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (files.SelectedNode != null)
                if (files.SelectedNode.Tag != null)
                    if (files.SelectedNode.Nodes.Count == 0)
                        Editor.OpenSource(new RemoteSource("", files.SelectedNode.Tag.ToString()));
        }

        private void files_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string pathResult = "";
            bool success = false;

            if (e.Label == null)
                return;

            if (e.Label == e.Node.Text)
                return;

            if (e.Label == "")
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Node.Nodes.Count > 0)
                pathResult = IBMi.RenameDir(e.Node.Tag.ToString(), e.Label);
            else
                pathResult = IBMi.RenameFile(e.Node.Tag.ToString(), e.Label);

            success = (e.Node.Tag.ToString() != pathResult);

            if (success)
                e.Node.Tag = pathResult;

            e.CancelEdit = !success;
        }

        private TreeNode RightClickedNode;
        private void files_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                files.SelectedNode = e.Node;
                RightClickedNode = e.Node;

                createToolStripMenuItem.Enabled = (e.Node.Nodes.Count > 0);
                rightClickMenu.Show(Cursor.Position);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickedNode != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete '" + RightClickedNode.Tag.ToString() + "'?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (RightClickedNode.Nodes.Count == 0)
                    IBMi.DeleteFile(RightClickedNode.Tag.ToString());
                else
                    IBMi.DeleteDir(RightClickedNode.Tag.ToString());

                RightClickedNode.Remove();
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickedNode != null)
            {
                RightClickedNode.BeginEdit();
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickedNode != null)
            {
                CreateStreamFile window = new CreateStreamFile(RightClickedNode.Tag.ToString() + "/");
                window.ShowDialog();

                if (window.result != null)
                    Editor.OpenExistingSource(window.result);
            }
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RightClickedNode != null)
            {
                CreateDirectory window = new CreateDirectory(RightClickedNode.Tag.ToString() + "/");
                window.ShowDialog();

                RightClickedNode.Collapse();
                RightClickedNode.Expand();
            }
        }
    }
}
