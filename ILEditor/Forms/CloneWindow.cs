using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class CloneWindow : Form
    {
        public CloneWindow()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Dictionary<string, string> CloneList;
        private List<string> LocalSPFs;
        private void fetch_Click(object sender, EventArgs e)
        {
            Member[] MemberList;
            List<ListViewItem> Items = new List<ListViewItem>();
            ListViewItem Item;
            LocalSPFs = new List<string>();

            if (!IBMiUtils.IsValueObjectName(lib.Text))
            {
                MessageBox.Show("Library name not valid.");
            }

            lib.Text = lib.Text.Trim();

            ILEObject[] Files = IBMiUtils.GetSPFList(lib.Text);

            foreach (ILEObject Object in Files)
            {
                MemberList = IBMiUtils.GetMemberList(lib.Text, Object.Name);
                if (MemberList != null)
                {
                    LocalSPFs.Add(IBMiUtils.GetLocalDir(lib.Text, Object.Name));
                    for (int i = 0; i < MemberList.Length; i++)
                    {
                        Item = new ListViewItem(MemberList[i].GetObject() + "/" + MemberList[i].GetMember() + "." + MemberList[i].GetExtension().ToLower());
                        Item.Checked = true;
                        Item.Tag = new string[2] { MemberList[i].GetObject() + "/" + MemberList[i].GetMember(), IBMiUtils.GetLocalFile(MemberList[i].GetLibrary(), MemberList[i].GetObject(), MemberList[i].GetMember(), MemberList[i].GetExtension()) };
                        Items.Add(Item);
                    }
                }
            }
            memberList.Items.AddRange(Items.ToArray());

            lib.Enabled = false;
            fetch.Enabled = false;
            clone.Enabled = true;
            memberList.Enabled = true;
        }

        private void clone_Click(object sender, EventArgs e)
        {
            List<string> Commands = new List<string>();
            string[] member, path;

            CloneList = new Dictionary<string, string>();
            foreach (ListViewItem listitem in memberList.Items)
            {
                if (listitem.Checked)
                {
                    member = (string[])listitem.Tag;
                    path = member[0].Split('/');
                    CloneList.Add("/QSYS.lib/" + lib.Text + ".lib/" + path[0] + ".file/" + path[1] + ".mbr", member[1]);
                }
            }

            foreach (string Dir in Directory.GetDirectories(IBMiUtils.GetLocalDir(lib.Text)))
            {
                try
                {
                    Directory.Delete(Dir, true);
                } catch (Exception ex) { }
            }

            foreach (string Dir in LocalSPFs)
                Directory.CreateDirectory(Dir);

            bool isOkay = true;
            foreach (var File in CloneList)
            {
                if (IBMi.DownloadFile(File.Value, File.Key) == true) //Error?
                {
                    isOkay = false;
                    break;
                }
            }

            if (isOkay)
            {
                MessageBox.Show("Source-Physical File cloned sucessfully.", "SPF Clone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string Location = Program.SOURCEDIR + "\\" + IBMi.CurrentSystem.GetValue("system") + "\\" + lib.Text;
                Process.Start("explorer.exe", "/select, " + Location);
                this.Close();
            }
            else
            {
                MessageBox.Show("There was an error during the clone process.");
            }
        }
    }
}
