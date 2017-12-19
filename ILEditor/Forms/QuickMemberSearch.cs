using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class QuickMemberSearch : Form
    {
        public QuickMemberSearch()
        {
            InitializeComponent();
            memberValue.Focus();
        }

        private void memberValue_TextChanged(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                InitSearch(memberValue.Text);
            }).Start();
        }

        private void InitSearch(string Value)
        {
            string[] results = MemberCache.Find(Value);
            
            this.Invoke((MethodInvoker)delegate
            {
                memberList.Items.Clear();
                memberList.Items.AddRange(results);
            });
        }

        private void QuickMemberSearch_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void memberValue_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (memberList.Items.Count > 0)
                    {
                        SelectMember(memberList.Items[0].ToString());
                    }
                    break;
                case Keys.Down:
                    memberList.Focus();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void memberList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (memberList.SelectedItem != null)
            {
                SelectMember(memberList.SelectedItem.ToString());
            }
        }

        private void memberList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (memberList.SelectedItem != null)
                {
                    SelectMember(memberList.SelectedItem.ToString());
                }
            }
        }

        private void SelectMember(string Member)
        {
            if (Member != "")
            {
                string type = MemberCache.GetType(Member);
                string[] data = Member.Split(new char[] { '/', '.' }, StringSplitOptions.RemoveEmptyEntries);

                Member openMember = new Classes.Member("", data[0], data[1], data[2], type);
                Editor.OpenMember(openMember);

                this.Close();
            }
        }
    }
}
