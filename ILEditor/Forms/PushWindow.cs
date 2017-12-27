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

        Dictionary<string, int> CreateSPFs = new Dictionary<string, int>();
        Dictionary<string, string> CreateMembers = new Dictionary<string, string>();
        List<string> DeleteMembers = new List<string>();
        Dictionary<string, string> UploadMembers = new Dictionary<string, string>();
        private void fetch_Click(object sender, EventArgs e)
        {
            string LocalLIB = IBMiUtils.GetLocalDir(lib.Text);

            string[] Dirs = Directory.GetDirectories(LocalLIB);
            string Name, Ext, LocalMember, SPF;

            foreach (string Dir in Dirs)
            {
                SPF = Path.GetFileName(Dir);
                Member[] MemberList = IBMiUtils.GetMemberList(lib.Text, SPF);
                if (MemberList == null)
                    CreateSPFs.Add(SPF, 112);

                foreach (string FilePath in Directory.GetFiles(Dir))
                {
                    Name = SPF + '/' + Path.GetFileNameWithoutExtension(FilePath);
                    Ext = Path.GetExtension(FilePath).TrimStart('.');

                    if (MemberList == null)
                    {
                        CreateMembers.Add(Name, Ext);
                    }
                    else
                    {
                        if (MemberList.Where(x => (x.GetObject() + '/' + x.GetMember()) == Name).Count() == 0)
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
                            DeleteMembers.Add(MemberInfo.GetObject() + "/" + MemberInfo.GetMember());
                        }
                    }
                }
            }

            foreach (var SPFInfo in CreateSPFs)
            {
                memberLog.Items.Add(new ListViewItem(SPFInfo.Key + " (" + SPFInfo.Value.ToString() + ")", memberLog.Groups[0]));
            }

            foreach (string MemberName in DeleteMembers)
            {
                memberLog.Items.Add(new ListViewItem(MemberName, memberLog.Groups[1]));
            }

            foreach (var MemberInfo in CreateMembers)
            {
                memberLog.Items.Add(new ListViewItem(MemberInfo.Key + "." + MemberInfo.Value, memberLog.Groups[2]));
            }

            foreach (var MemberName in UploadMembers)
            {
                memberLog.Items.Add(new ListViewItem(MemberName.Key, memberLog.Groups[3]));
            }

            pushButton.Enabled = true;
            fetch.Enabled = false;
            lib.Enabled = false;
        }

        private void pushButton_Click(object sender, EventArgs e)
        {
            string LocalFile;
            List<string> Commands = new List<string>();
            Commands.Add("cd /QSYS.lib");
            string[] Path;

            foreach (var Member in CreateSPFs)
            {
                Commands.Add("QUOTE RCMD CRTSRCPF FILE(" + lib.Text.Trim() + "/" + Member.Key + ") RCDLEN(" + Member.Value.ToString() + ")");
            }

            foreach (string Member in DeleteMembers)
            {
                Path = Member.Split('/');
                Commands.Add("QUOTE RCMD RMVM FILE(" + lib.Text.Trim() + "/" + Path[0] + ") MBR(" + Path[1] + ")");
            }

            foreach (var Member in CreateMembers)
            {
                Path = Member.Key.Trim().Split('/');
                Commands.Add("QUOTE RCMD ADDPFM FILE(" + lib.Text.Trim() + "/" + Path[0] + ") MBR(" + Path[1] + ") SRCTYPE(" + Member.Value.Trim() + ")");
            }

            foreach(var Member in UploadMembers)
            {
                Path = Member.Key.Trim().Split('/');
                LocalFile = IBMiUtils.GetLocalFile(lib.Text.Trim(), Path[0], Path[1], Member.Value);
                Commands.Add("put \"" + LocalFile + "\" \"" + lib.Text.Trim() + ".lib/" + Path[0] + ".file/" + Path[1] + ".mbr\"");
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
