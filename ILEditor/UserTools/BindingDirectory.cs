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
using System.Threading;

namespace ILEditor.UserTools
{
    public partial class BindingDirectory : UserControl
    {
        public BindingDirectory(string Lib, string Obj)
        {
            InitializeComponent();
            UpdateListing(Lib, Obj);
        }

        public void UpdateListing(string Lib, string Obj)
        {
            Thread gothread = new Thread((ThreadStart)delegate
            {
                List<ListViewItem> Rows = new List<ListViewItem>();
                BindingEntry[] Entries = IBMiUtils.GetBindingDirectory(Lib, Obj);
                if (Entries != null)
                {
                    foreach (BindingEntry Entry in Entries)
                    {
                        ListViewItem Item = new ListViewItem(new string[6] { Entry.Name, Entry.Type, Entry.Library, Entry.Activation, Entry.CreationDate, Entry.CreationTime });
                        Item.Tag = Entry;
                        Rows.Add(Item);
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        entriesList.Items.Clear();
                        entriesList.Items.AddRange(Rows.ToArray());
                    });
                }
            });

            gothread.Start();
        }

        private void entriesList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (entriesList.SelectedItems.Count > 0)
                {
                    ListViewItem Selected = entriesList.SelectedItems[0];
                    if (Selected.Tag != null)
                    {
                        BindingEntry Entry = (BindingEntry)Selected.Tag;
                        string command = "QUOTE RCMD RMVBNDDIRE BNDDIR(" + Entry.BindingLib + "/" + Entry.BindingObj + ") OBJ((" + Entry.Library + "/" + Entry.Name + " " + Entry.Type + "))";
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this binding entry?", "Deleting Binding Entry", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            Thread gothread = new Thread((ThreadStart)delegate
                            {
                                if (IBMi.RunCommands(new string[1] { command }) == false)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        Selected.Remove();
                                    });
                                }
                            });
                            gothread.Start();
                        }
                    }
                }
            }
        }
    }
}
