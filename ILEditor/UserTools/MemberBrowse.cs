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

        private List<ListViewItem> curItems = new List<ListViewItem>();
        public void UpdateListing(string Lib, string Obj)
        {
            Thread gothread = new Thread((ThreadStart)delegate
            {
                Member[] members;
                ListViewItem curItem;
                Boolean NoMembers = false;

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
                    NoMembers = (members.Length == 0);
                    if (!NoMembers)
                    {
                        foreach (Member member in members)
                        {
                            curItem = new ListViewItem(new string[3] { member.GetMember(), member.GetExtension(), member.GetText() }, 0);
                            curItem.Tag = member;

                            curItems.Add(curItem);
                        }

                        this.Invoke((MethodInvoker)delegate
                        {
                            memberList.Items.AddRange(curItems.ToArray());
                            membercount.Text = members.Length.ToString() + " member" + (members.Length == 1 ? "" : "s");
                        });
                    }
                }
                else
                {
                    NoMembers = true;
                }

                if (NoMembers)
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

        private void fetchButton_Click(object sender, EventArgs e)
        {
            library.Text = library.Text.Trim();
            spf.Text = spf.Text.Trim();

            if (!IBMiUtils.IsValueObjectName(library.Text))
            {
                MessageBox.Show("Library name is not valid.");
                return;
            }
            if (library.Text.ToUpper() == "*ALL")
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
                    Member member = (Member)selection.Tag;

                    Editor.OpenMember(member);
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
                curItem.Tag = new Member("", library.Text.Trim(), spf.Text.Trim(), newMemberForm._mbr, newMemberForm._type);
                memberList.Items.Add(curItem);
            }

            newMemberForm.Dispose();
        }

        #region rightclick

        private Member currentRightClick;
        private void memberList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (memberList.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    currentRightClick = (Member)memberList.FocusedItem.Tag;
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
                Member MemberInfo = currentRightClick;
                string[] Items = IBMi.CurrentSystem.GetValue("TYPE_" + MemberInfo.GetExtension()).Split('|');
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
                IBMiUtils.CompileMember(currentRightClick, clickedItem.Text);
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentRightClick != null)
            {
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileMember(currentRightClick);
                }).Start();
            }
        }

        #endregion
    }
}
