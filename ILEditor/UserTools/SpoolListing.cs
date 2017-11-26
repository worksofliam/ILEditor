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

namespace ILEditor.UserTools
{
    public partial class SpoolListing : UserControl
    {
        public SpoolListing()
        {
            InitializeComponent();
            RefreshList();
        }

        public void RefreshList()
        {
            new Thread((ThreadStart)delegate
            {
                ListViewItem curItem;
                List<ListViewItem> Items = new List<ListViewItem>();
                SpoolFile[] Listing = IBMiUtils.GetSpoolListing();

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
            }).Start();
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
                                Editor.TheEditor.AddSpoolFile(spool.getName(), SpoolFile);
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
    }
}
