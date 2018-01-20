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
        private string Library;
        private string Object;

        public BindingDirectory(string Lib, string Obj)
        {
            InitializeComponent();
            UpdateListing(Lib, Obj);

            Library = Lib;
            Object = Obj;
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
                else
                {
                    MessageBox.Show("Unable to obtain binding directory!");
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
                        string command = "RMVBNDDIRE BNDDIR(" + Entry.BindingLib + "/" + Entry.BindingObj + ") OBJ((" + Entry.Library + "/" + Entry.Name + " " + Entry.Type + "))";
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!IBMiUtils.IsValueObjectName(objectName.Text))
            {
                MessageBox.Show("Object name is not valid.");
                objectName.Focus();
                return;
            }
            if (objectType.Text == "")
            {
                MessageBox.Show("Object type is not valid.");
                objectType.Focus();
                return;
            }
            if (IBMiUtils.IsValueObjectName(objectLib.Text) == false && objectLib.Text != "*LIBL")
            {
                MessageBox.Show("Object library name is not valid.");
                objectLib.Focus();
                return;
            }
            if (objectActivation.Text == "")
            {
                MessageBox.Show("Object activation is not valid.");
                objectType.Focus();
                return;
            }

            BindingEntry Entry = new BindingEntry();

            Entry.BindingLib = Library;
            Entry.BindingObj = Object;
            Entry.Name = objectName.Text.Trim();
            Entry.Library = objectLib.Text.Trim();
            Entry.Type = objectType.Text;
            Entry.Activation = objectActivation.Text;
            Entry.CreationDate = "";
            Entry.CreationTime = "";

            string command = "ADDBNDDIRE BNDDIR(" + Library + "/" + Object + ") OBJ((" + Entry.Library + "/" + Entry.Name + " " + Entry.Type + " " + Entry.Activation + "))";
            Thread gothread = new Thread((ThreadStart)delegate
            {
                if (IBMi.RunCommands(new string[1] { command }) == false)
                {
                    ListViewItem Item = new ListViewItem(new string[6] { Entry.Name, Entry.Type, Entry.Library, Entry.Activation, Entry.CreationDate, Entry.CreationTime });
                    Item.Tag = Entry;
                    this.Invoke((MethodInvoker)delegate
                    {
                        entriesList.Items.Add(Item);
                    });
                }
                else
                {
                    MessageBox.Show("Unable to create binding entry.");
                }
            });
            gothread.Start();
        }
    }
}
