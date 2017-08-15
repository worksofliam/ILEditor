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
using System.IO;

namespace ILEditor.UserTools
{
    public partial class MemberBrowse : UserControl
    {
        public MemberBrowse()
        {
            InitializeComponent();
        }

        private static readonly Dictionary<string, ILELanguage> LangTypes = new Dictionary<string, ILELanguage>()
        {
            { "RPGLE", ILELanguage.RPG },
            { "SQLRPGLE", ILELanguage.RPG },
            { "CPP", ILELanguage.CPP },
            { "C", ILELanguage.CPP }
        };

        private ILELanguage GetBoundLangType(string Obj)
        {
            if (LangTypes.ContainsKey(Obj))
                return LangTypes[Obj];
            else
                return ILELanguage.None;
        }

        private List<ListViewItem> curItems = new List<ListViewItem>();
        public void UpdateListing(string Lib, string Obj)
        {
            Thread gothread = new Thread((ThreadStart)delegate
            {
                string[][] members;
                ListViewItem curItem;

                curItems.Clear();

                this.Invoke((MethodInvoker)delegate
                {
                    memberList.Items.Clear();
                    memberList.Items.Add(new ListViewItem("Loading...", 2));
                });

                members = IBMiUtils.GetMemberList(Lib, Obj);
                
                this.Invoke((MethodInvoker)delegate
                {
                    memberList.Items.Clear();
                });

                if (members != null)
                {
                    foreach (string[] member in members)
                    {
                        curItem = new ListViewItem(member[0] + "." + member[1] + " - " + member[2], 0);
                        curItem.Tag = Lib + '|' + Obj + '|' + member[0] + '|' + member[1];

                        curItems.Add(curItem);
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        memberList.Items.AddRange(curItems.ToArray());
                        membercount.Text = members.Length.ToString() + " member" + (members.Length == 1 ? "" : "s");
                    });
                }
                else
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        memberList.Items.Add(new ListViewItem("No members found!", 1));
                        membercount.Text = "0 members";
                    });
                }
            });
            gothread.Start();
        }

        private void OpenMember(string Lib, string Obj, string Mbr, string Ext, Boolean Editing)
        {
            Thread gothread = new Thread((ThreadStart)delegate {
                string resultFile = IBMiUtils.DownloadMember(Lib, Obj, Mbr);

                if (resultFile != "")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Editor.TheEditor.AddMemberEditor(new Member(resultFile, Lib, Obj, Mbr, Ext, Editing), GetBoundLangType(Ext));
                    });
                }
                else
                {
                    MessageBox.Show("Unable to download member " + Lib + "/" + Obj + "." + Mbr + ". Please check it exists and that you have access to the remote system.");
                }
            });
            gothread.Start();
        }

        private void fetchButton_Click(object sender, EventArgs e)
        {
            if (!IBMiUtils.IsValueObjectName(library.Text))
            {
                MessageBox.Show("Library name is not valid.");
                return;
            }
            if (!IBMiUtils.IsValueObjectName(spf.Text))
            {
                MessageBox.Show("Object name is not valid.");
                return;
            }

            this.Parent.Text = library.Text + "/" + spf.Text + " [Listing]";
            UpdateListing(library.Text, spf.Text);
        }

        private void memberList_DoubleClick(object sender, EventArgs e)
        {
            if (memberList.SelectedItems.Count == 1)
            {
                ListViewItem selection = memberList.SelectedItems[0];
                if (selection.Tag != null)
                {
                    string tag = (string)selection.Tag;
                    if (tag != "")
                    {
                        string[] path = tag.Split('|');

                        OpenMember(path[0], path[1], path[2], path[3], true);
                    }
                }
            }
        }
    }
}
