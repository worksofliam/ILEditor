using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ILEditor.Forms
{
    public partial class PushWindow : Form
    {
        public PushWindow()
        {
            InitializeComponent();
        }

        Dictionary<string, string> CreateMembers = new Dictionary<string, string>();
        List<string> DeleteMembers = new List<string>();
        Dictionary<string, string> UploadMembers = new Dictionary<string, string>();
        private void fetch_Click(object sender, EventArgs e)
        {
            string LocalSPF = IBMiUtils.GetLocalDir(lib.Text, spf.Text);

            string[] Files = Directory.GetFiles(LocalSPF);
            string Name, Ext, LocalMember;

            if (Files.Length > 0)
            {
                Member[] MemberList = IBMiUtils.GetMemberList(lib.Text, spf.Text);
                foreach (string FilePath in Files)
                {
                    Name = Path.GetFileNameWithoutExtension(FilePath);
                    Ext = Path.GetExtension(FilePath).TrimStart('.');

                    if (MemberList == null)
                    {
                        CreateMembers.Add(Name, Ext);
                    }
                    else
                    {
                        if (MemberList.Where(x => x.GetMember() == Name).Count() == 0)
                        {
                            CreateMembers.Add(Name, Ext);
                        }
                    }

                    UploadMembers.Add(Name, Ext);
                }

                if (MemberList != null)
                {
                    foreach (Member MemberInfo in MemberList)
                    {
                        LocalMember = IBMiUtils.GetLocalFile(MemberInfo.GetLibrary(), MemberInfo.GetObject(), MemberInfo.GetMember(), MemberInfo.GetExtension());
                        if (!File.Exists(LocalMember))
                        {
                            DeleteMembers.Add(MemberInfo.GetMember());
                        }
                    }
                }

                ListViewItem item;
                foreach (string MemberName in DeleteMembers)
                {
                    memberLog.Items.Add(new ListViewItem(MemberName, memberLog.Groups[0]));
                }
                
                foreach (var MemberInfo in CreateMembers)
                {
                    memberLog.Items.Add(new ListViewItem(MemberInfo.Key + "." + MemberInfo.Value, memberLog.Groups[1]));
                }
                
                foreach (var MemberName in UploadMembers)
                {
                    memberLog.Items.Add(new ListViewItem(MemberName.Key, memberLog.Groups[2]));
                }

                pushButton.Enabled = true;
                fetch.Enabled = false;
                lib.Enabled = false;
                spf.Enabled = false;
            }
            else
            {
                memberLog.Items.Add("No local members found.");
            }
        }

        private void pushButton_Click(object sender, EventArgs e)
        {
            string LocalFile;
            List<string> Commands = new List<string>();
            Commands.Add("cd /QSYS.lib");

            foreach(string Member in DeleteMembers)
            {
                Commands.Add("QUOTE RCMD RMVM FILE(" + lib.Text.Trim() + "/" + spf.Text.Trim() +") MBR(" + Member.Trim() + ")");
            }

            foreach (var Member in CreateMembers)
            {
                Commands.Add("QUOTE RCMD ADDPFM FILE(" + lib.Text.Trim() + "/" + spf.Text.Trim() + ") MBR(" + Member.Key.Trim() + ") SRCTYPE(" + Member.Value.Trim() + ")");
            }

            foreach(var Member in UploadMembers)
            {
                LocalFile = IBMiUtils.GetLocalFile(lib.Text.Trim(), spf.Text.Trim(), Member.Key, Member.Value);
                Commands.Add("put \"" + LocalFile + "\" \"" + lib.Text.Trim() + ".lib/" + spf.Text.Trim() + ".file/" + Member.Key + ".mbr\"");
            }

            Boolean Failure = IBMi.RunCommands(Commands.ToArray());

            if (Failure == false)
            {
                MessageBox.Show("Push to server was successful.");
            }
            else
            {
                MessageBox.Show("Push to server was not successful.");
            }

            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
