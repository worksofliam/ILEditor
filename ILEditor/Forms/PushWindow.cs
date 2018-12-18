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

        private void PushWindow_Load(object sender, EventArgs e)
        {
            if (!IBMi.IsConnected())
            {
                MessageBox.Show("The SPF Push tool does not work in Offline Mode.");
                this.Close();
            }
        }

        Dictionary<string, int> CreateSPFs = new Dictionary<string, int>();
        Dictionary<string, string> CreateMembers = new Dictionary<string, string>();
        List<string> DeleteMembers = new List<string>();
        Dictionary<string, string> UploadMembers = new Dictionary<string, string>();
        private void fetch_Click(object sender, EventArgs e)
        {
            string LocalLIB = IBMiUtils.GetLocalDir(lib.Text);

            string[] Dirs = Directory.GetDirectories(LocalLIB), MbrPath;
            string Name, Ext, LocalMember, SPF;
            bool CheckedUpload = false;

            foreach (string Dir in Dirs)
            {
                SPF = Path.GetFileName(Dir);

                if (SPF.StartsWith(".")) continue;

                RemoteSource[] MemberList = IBMiUtils.GetMemberList(lib.Text, SPF);
                if (MemberList == null)
                    CreateSPFs.Add(SPF, 112);

                foreach (string FilePath in Directory.GetFiles(Dir))
                {
                    Name = SPF + '/' + Path.GetFileNameWithoutExtension(FilePath);
                    Ext = Path.GetExtension(FilePath).TrimStart('.');

                    if (UploadMembers.ContainsKey(Name))
                        continue;

                    if (MemberList == null)
                    {
                        CreateMembers.Add(Name, Ext);
                    }
                    else
                    {
                        if (MemberList.Where(x => (x.GetObject() + '/' + x.GetName()) == Name).Count() == 0)
                            CreateMembers.Add(Name, Ext);
                    }

                    UploadMembers.Add(Name, Ext);
                }

                if (MemberList != null)
                {
                    foreach (RemoteSource MemberInfo in MemberList)
                    {
                        LocalMember = IBMiUtils.GetLocalFile(MemberInfo.GetLibrary(), MemberInfo.GetObject(), MemberInfo.GetName(), MemberInfo.GetExtension());
                        if (!File.Exists(LocalMember))
                            DeleteMembers.Add(MemberInfo.GetObject() + "/" + MemberInfo.GetName());
                    }
                }
            }

            foreach (var SPFInfo in CreateSPFs)
            {
                commandLog.Items.Add(new ListViewItem(SPFInfo.Key + " (" + SPFInfo.Value.ToString() + ")", commandLog.Groups[0]));
            }

            foreach (string MemberName in DeleteMembers)
            {
                commandLog.Items.Add(new ListViewItem(MemberName, commandLog.Groups[1]));
            }

            foreach (var MemberInfo in CreateMembers)
            {
                commandLog.Items.Add(new ListViewItem(MemberInfo.Key + "." + MemberInfo.Value, commandLog.Groups[2]));
            }

            foreach (var MemberName in UploadMembers)
            {
                MbrPath = MemberName.Key.Trim().Split('/');
                CheckedUpload = FileCache.EditsContains(lib.Text.ToUpper(), MbrPath[0], MbrPath[1]);
                memberLog.Items.Add(new ListViewItem(MemberName.Key) { Checked = CheckedUpload });
            }

            pushButton.Enabled = true;
            fetch.Enabled = false;
            lib.Enabled = false;
            runCommands.Enabled = true;
        }

        private void pushButton_Click(object sender, EventArgs e)
        {
            string LocalFile;
            List<string> Commands = new List<string>();
            Dictionary<string, string> PushList = new Dictionary<string, string>();
            string[] Path;

            if (runCommands.Checked)
            {
                foreach (var Member in CreateSPFs)
                {
                    Commands.Add("CRTSRCPF FILE(" + lib.Text.Trim() + "/" + Member.Key + ") RCDLEN(" + Member.Value.ToString() + ")");
                }

                foreach (string Member in DeleteMembers)
                {
                    Path = Member.Split('/');
                    Commands.Add("RMVM FILE(" + lib.Text.Trim() + "/" + Path[0] + ") MBR(" + Path[1] + ")");
                }

                foreach (var Member in CreateMembers)
                {
                    Path = Member.Key.Trim().Split('/');
                    Commands.Add("ADDPFM FILE(" + lib.Text.Trim() + "/" + Path[0] + ") MBR(" + Path[1] + ") SRCTYPE(" + Member.Value.Trim() + ")");
                }
            }

            foreach(ListViewItem Member in memberLog.Items)
            {
                if (Member.Checked)
                {
                    Path = Member.Text.Trim().Split('/');
                    LocalFile = IBMiUtils.GetLocalFile(lib.Text.Trim(), Path[0], Path[1], UploadMembers[Path[0] + '/' + Path[1]]);
                    PushList.Add(LocalFile, "/QSYS.lib/" + lib.Text.Trim() + ".lib/" + Path[0] + ".file/" + Path[1] + ".mbr");
                }
            }

            Boolean Success = IBMi.RunCommands(Commands.ToArray());
            if (Success)
            {
                foreach (var File in PushList)
                {
                    if (IBMi.UploadFile(File.Key, File.Value) == false)
                        Success = false;
                }

                if (Success)
                {
                    MessageBox.Show("Push to server was successful.");
                    FileCache.EditsClear();
                    this.Close();
                } 
                else
                    MessageBox.Show("Push to server was not successful (stage 2)");
            }
            else
            {
                MessageBox.Show("Push to server was not successful (stage 1)");
            }

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
