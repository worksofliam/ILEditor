using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.UserTools
{
    public partial class ErrorListing : UserControl
    {
        public ErrorListing(Boolean hideBar = false)
        {
            if (hideBar)
                toolStrip1.Visible = false;

            InitializeComponent();
        }

        public void publishErrors()
        {
            Invoke((MethodInvoker)delegate
            {
                int totalErrors = 0;
                TreeNode curNode;

                treeView1.Nodes.Clear(); //Clear out the nodes
                
                //Add the errors
                TreeNode curErr;
                foreach (int fileid in ErrorHandle.getFileIDs())
                {
                    curNode = new TreeNode(ErrorHandle.getFilePath(fileid));
                    foreach (LineError error in ErrorHandle.getErrors(fileid))
                    {
                        if (error.getSev() >= 20)
                        {
                            totalErrors += 1;
                            curErr = curNode.Nodes.Add(error.getCode() + ": " + error.getData().Trim() + " (" + error.getLine().ToString() + ")");
                            curErr.Tag = error.getLine().ToString() + ',' + error.getColumn().ToString();
                            curErr.ImageIndex = 1;
                            curErr.SelectedImageIndex = 1;
                        }
                    }

                    //Only add a node if there is something to display
                    if (curNode.Nodes.Count > 0)
                    {
                        curNode.ImageIndex = 0;
                        curNode.SelectedImageIndex = 0;
                        treeView1.Nodes.Add(curNode);
                    }
                }

                toolStripStatusLabel1.Text = "Total errors: " + totalErrors.ToString();
                toolStripStatusLabel2.Text = ErrorHandle.doName();
                toolStripStatusLabel3.Text = DateTime.Now.ToString("h:mm:ss tt");
            });
        }

        private void fetchErrors_Click(object sender, EventArgs e)
        {
            if (!IBMiUtils.IsValueObjectName(lib.Text))
            {
                MessageBox.Show("Library name is not valid.");
                return;
            }
            if (!IBMiUtils.IsValueObjectName(obj.Text))
            {
                MessageBox.Show("Object name is not valid.");
                return;
            }

            Thread gothread = new Thread((ThreadStart)delegate
            {
                ErrorHandle.getErrors(lib.Text, obj.Text);
                publishErrors();
            });
            gothread.Start();
        }
    }
}
