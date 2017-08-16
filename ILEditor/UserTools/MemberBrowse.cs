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
using ILEditor.Forms;

namespace ILEditor.UserTools
{
    public partial class MemberBrowse : UserControl
    {
        public MemberBrowse(string Lib = "", string Obj = "") 
        {
            InitializeComponent();

            if (Lib != "" && Obj != "")
            {
                library.Text = Lib;
                spf.Text = Obj;
            }
        }
        
        private void MemberBrowse_Load(object sender, EventArgs e)
        {
            if (library.Text != "" && spf.Text != "")
                fetchButton.PerformClick();
        }

        private static readonly Dictionary<string, ILELanguage> LangTypes = new Dictionary<string, ILELanguage>()
        {
            { "RPGLE", ILELanguage.RPG },
            { "SQLRPGLE", ILELanguage.RPG },
            { "CPP", ILELanguage.CPP },
            { "C", ILELanguage.CPP }
        };

        public static ILELanguage GetBoundLangType(string Obj)
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
                        curItem = new ListViewItem(new string[3] { member[0], member[1], member[2] }, 0);
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

                this.Invoke((MethodInvoker)delegate
                {
                    addmember.Enabled = true;
                });
            });
            gothread.Start();
        }

        public void OpenMember(string Lib, string Obj, string Mbr, string Ext, Boolean Editing)
        {
            string TabText = Lib + "/" + Obj + "(" + Mbr + ")";
            int TabIndex = Editor.TheEditor.EditorContains(TabText);
            if (Editor.TheEditor.EditorContains(TabText) == -1)
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
            else
            {
                Editor.TheEditor.SwitchToTab(TabIndex);
            }
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
            Welcome.JustOpened(library.Text, spf.Text);
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

        private void addmember_Click(object sender, EventArgs e)
        {
            NewMember newMemberForm = new NewMember(library.Text.Trim(), spf.Text.Trim());
            newMemberForm.ShowDialog();

            if (newMemberForm.created)
            {
                ListViewItem curItem = new ListViewItem(new string[3] { newMemberForm._mbr, newMemberForm._type, newMemberForm._text }, 0);
                curItem.Tag = newMemberForm._lib + '|' + newMemberForm._spf + '|' + newMemberForm._mbr + '|' + newMemberForm._type;
                memberList.Items.Add(curItem);
            }

            newMemberForm.Dispose();
        }

        #region rightclick

        private string currentRightClick;
        private void memberList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (memberList.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    currentRightClick = memberList.FocusedItem.Tag.ToString();
                    compileRightclick.Show(Cursor.Position);
                }
            }
        }

        private void compileRightclick_Opening(object sender, CancelEventArgs e)
        {
            compileOtherToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> Compiles = new List<ToolStripMenuItem>();
            if (currentRightClick != null)
            {
                string[] path = currentRightClick.Split('|');
                Member MemberInfo = new Member("", path[0], path[1], path[2], path[3], false);
                string[] Items = IBMi.CurrentSystem.GetValue("TYPE_" + path[3]).Split('|');
                foreach (string Item in Items)
                {
                    if (Item.Trim() == "") continue;
                    Compiles.Add(new ToolStripMenuItem(Item, null, compileAnyHandle));
                }
            }

            compileToolStripMenuItem.Enabled = (Compiles.Count > 0);
            compileOtherToolStripMenuItem.Enabled = (Compiles.Count > 0);
            compileOtherToolStripMenuItem.DropDownItems.AddRange(Compiles.ToArray());
        }


        private void compileAnyHandle(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (currentRightClick != null)
            {
                string[] path = currentRightClick.Split('|');
                Member MemberInfo = new Member("", path[0], path[1], path[2], path[3], false);
                IBMiUtils.CompileMember(MemberInfo, clickedItem.Text);
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentRightClick != null)
            {
                string[] path = currentRightClick.Split('|');
                Member MemberInfo = new Member("", path[0], path[1], path[2], path[3], false);
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileMember(MemberInfo);
                }).Start();
            }
        }

        #endregion
    }
}
