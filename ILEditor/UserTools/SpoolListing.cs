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
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
    public partial class SpoolListing : DockContent
    {
        public SpoolListing()
        {
            InitializeComponent();

            this.Text = "Spool Listing";

            RefreshList();
        }

        public void RefreshList()
        {
            string Lib = IBMi.CurrentSystem.GetValue("printerLib"), Obj = IBMi.CurrentSystem.GetValue("printerObj");

            Thread spoolThread = new Thread((ThreadStart)delegate
            {
                ListViewItem curItem;
                List<ListViewItem> Items = new List<ListViewItem>();
                SpoolFile[] Listing = IBMiUtils.GetSpoolListing(Lib, Obj);

                if (Listing != null)
                {
                    foreach (SpoolFile Spool in Listing)
                    {
                        curItem = new ListViewItem(new[] { Spool.getName(), Spool.getData(), Spool.getStatus(), Spool.getJob() }, 0);
                        curItem.Tag = Spool;
                        Items.Add(curItem);
                    }
                }
                else
                {
                    Items.Add(new ListViewItem("No spool files found."));
                }

                this.Invoke((MethodInvoker)delegate
                {
                    spoolList.Items.Clear();
                    spoolList.Items.AddRange(Items.ToArray());
                });
            });

            if (Lib == "" || Obj == "")
            {
                MessageBox.Show("You must setup the Output Queue in the Connection Settings for the spool file listing to work.", "Spool Listing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                spoolThread.Start();
            }
        }

        private void spoolList_DoubleClick(object sender, EventArgs e)
        {
            if (spoolList.SelectedItems.Count == 1)
            {
                ListViewItem selection = spoolList.SelectedItems[0];
                if (selection.Tag != null)
                {
                    new Thread((ThreadStart)delegate
                    {
                        SpoolFile spool = selection.Tag as SpoolFile;
                        string SpoolFile = spool.Download();

                        if (SpoolFile != "")
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //TODO: AddTool for spool file!!
                            });
                        }
                        else
                        {
                            MessageBox.Show("Spool file was not downloaded. Please check the spool file exists.");
                        }
                    }).Start();
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete all spool files of user " + IBMi.CurrentSystem.GetValue("username") + "? This process can take some time.", "Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            
            if (result == DialogResult.Yes)
            {
                Editor.TheEditor.SetStatus("Deleting all spool files..");
                new Thread((ThreadStart)delegate
                {
                    if (IBMi.RemoteCommand("DLTSPLF FILE(*SELECT)") == true)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Editor.TheEditor.SetStatus("Spool files deleted.");
                            spoolList.Items.Clear();
                        });
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete all spool files.");
                    }
                }).Start();
            }
        }
    }
}
