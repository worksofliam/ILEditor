using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        
        private void fetch_Click(object sender, EventArgs e)
        {
            if (!IBMiUtils.IsValueObjectName(lib.Text))
            {
                MessageBox.Show("Library name not valid.");
            }
            if (!IBMiUtils.IsValueObjectName(spf.Text))
            {
                MessageBox.Show("Source-Physical File name not valid.");
            }

            lib.Text = lib.Text.Trim();
            spf.Text = spf.Text.Trim();

            Member[] MemberList = IBMiUtils.GetMemberList(lib.Text, spf.Text);
            ListViewItem[] Items;
            if (MemberList == null)
            {
                MessageBox.Show("Provide Source-Physical File is not valid.", "SPF Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Items = new ListViewItem[MemberList.Length];
                for (int i = 0; i < MemberList.Length; i++)
                {
                    Items[i] = new ListViewItem(MemberList[i].GetMember() + "." + MemberList[i].GetExtension().ToLower());
                    Items[i].Checked = true;
                    Items[i].Tag = new string[2] { MemberList[i].GetMember(), IBMiUtils.GetLocalFile(MemberList[i].GetLibrary(), MemberList[i].GetObject(), MemberList[i].GetMember(), MemberList[i].GetExtension()) };
                }

                memberList.Items.AddRange(Items);
                lib.Enabled = false;
                spf.Enabled = false;
                fetch.Enabled = false;
                clone.Enabled = true;
                memberList.Enabled = true;
            }
        }

        private void clone_Click(object sender, EventArgs e)
        {
            List<string> Commands = new List<string>();
            string[] member;

            Commands.Add("cd /QSYS.lib");
            foreach (ListViewItem listitem in memberList.Items)
            {
                if (listitem.Checked)
                {
                    member = (string[])listitem.Tag;
                    Commands.Add("recv \"" + lib.Text + ".lib/" + spf.Text + ".file/" + member[0] + ".mbr\" \"" + member[1] + "\"");
                }
            }

            if (IBMi.RunCommands(Commands.ToArray()) == false)
            {
                MessageBox.Show("Source-Physical File cloned sucessfully.", "SPF Clone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string Location = Program.SOURCEDIR + "\\" + IBMi.CurrentSystem.GetValue("system") + "\\" + lib.Text + "\\" + spf.Text;
                Process.Start("explorer.exe", "/select, " + Location);
                this.Close();
            }
            else
            {
                
            }
        }
    }
}
