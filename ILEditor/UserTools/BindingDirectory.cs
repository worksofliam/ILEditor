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
                        Rows.Add(new ListViewItem(new string[6] { Entry.Name, Entry.Type, Entry.Library, Entry.Activation, Entry.CreationDate, Entry.CreationTime }));
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
    }
}
