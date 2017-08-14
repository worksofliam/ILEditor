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
using FastColoredTextBoxNS;

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

                if (ErrorHandle.WasSuccessful())
                {
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

            this.Parent.Text = lib.Text + "/" + obj.Text + " [Errors]";
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Tag == null) { }
            else
            {
                string[] data = e.Node.Tag.ToString().Split(',');
                int line, col;
                string error;

                line = int.Parse(data[0]) - 1;
                col = int.Parse(data[1]);
                error = e.Node.Text;
                if (col > 0) col--;

                onSelectError(e.Node.Parent.Text, line, col, error);
            }
        }

        private void onSelectError(string File, int Line, int Col, string ErrorText)
        {
            int index = Editor.TheEditor.EditorContains(File);

            if (index >= 0)
            {
                Editor.TheEditor.SwitchToTab(index);
                FastColoredTextBox SourceEditor = Editor.TheEditor.GetTabEditor(index);
                Range errorRange = SourceEditor.GetLine(Line);

                SourceEditor.ClearHints();
                SourceEditor.AddHint(errorRange, ErrorText, true, false, true);
            }
            else
            {
                MessageBox.Show("Unable to open error. Please open the source manually first and then try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
