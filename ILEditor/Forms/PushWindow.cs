using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class PushWindow : Form
	{
		private readonly Dictionary<string, string> CreateMembers = new Dictionary<string, string>();
		private readonly Dictionary<string, int>    CreateSPFs    = new Dictionary<string, int>();
		private readonly List<string>               DeleteMembers = new List<string>();
		private readonly Dictionary<string, string> UploadMembers = new Dictionary<string, string>();

		public PushWindow()
		{
			InitializeComponent();
		}

		private void PushWindow_Load(object sender, EventArgs e)
		{
			if (!IBMi.IsConnected)
			{
				MessageBox.Show("The SPF Push tool does not work in Offline Mode.");
				Close();
			}
		}

		private void fetch_Click(object sender, EventArgs e)
		{
			var localLib = IBMiUtils.GetLocalDir(lib.Text);

			var    dirs = Directory.GetDirectories(localLib);
			string name;

			foreach (var dir in dirs)
			{
				var spf = Path.GetFileName(dir);

				if (spf == null || spf.StartsWith("."))
					continue;

				var memberList = IBMiUtils.GetMemberList(lib.Text, spf);
				if (memberList == null)
					CreateSPFs.Add(spf, 112);

				foreach (var filePath in Directory.GetFiles(dir))
				{
					name = spf + '/' + Path.GetFileNameWithoutExtension(filePath);
					var ext = Path.GetExtension(filePath).TrimStart('.');

					if (UploadMembers.ContainsKey(name))
						continue;

					if (memberList == null)
					{
						CreateMembers.Add(name, ext);
					}
					else
					{
						if (memberList.Count(x => x.Object + '/' + x.Name == name) == 0)
							CreateMembers.Add(name, ext);
					}

					UploadMembers.Add(name, ext);
				}

				if (memberList != null)
					foreach (var memberInfo in memberList)
					{
						var localMember = IBMiUtils.GetLocalFile(memberInfo.Library,
							memberInfo.Object,
							memberInfo.Name,
							memberInfo.Extension);

						if (!File.Exists(localMember))
							DeleteMembers.Add(memberInfo.Object + "/" + memberInfo.Name);
					}
			}

			foreach (var spfInfo in CreateSPFs)
				commandLog.Items.Add(new ListViewItem(spfInfo.Key + " (" + spfInfo.Value + ")", commandLog.Groups[0]));

			foreach (var memberName in DeleteMembers)
				commandLog.Items.Add(new ListViewItem(memberName, commandLog.Groups[1]));

			foreach (var memberInfo in CreateMembers)
				commandLog.Items.Add(new ListViewItem(memberInfo.Key + "." + memberInfo.Value, commandLog.Groups[2]));

			foreach (var memberName in UploadMembers)
			{
				var mbrPath       = memberName.Key.Trim().Split('/');
				var checkedUpload = FileCache.EditsContains(lib.Text.ToUpper(), mbrPath[0], mbrPath[1]);
				memberLog.Items.Add(new ListViewItem(memberName.Key) {Checked = checkedUpload});
			}

			pushButton.Enabled  = true;
			fetch.Enabled       = false;
			lib.Enabled         = false;
			runCommands.Enabled = true;
		}

		private void pushButton_Click(object sender, EventArgs e)
		{
			var      commands = new List<string>();
			var      pushList = new Dictionary<string, string>();
			string[] path;

			if (runCommands.Checked)
			{
				foreach (var member in CreateSPFs)
					commands.Add("CRTSRCPF FILE(" +
					             lib.Text.Trim() +
					             "/" +
					             member.Key +
					             ") RCDLEN(" +
					             member.Value +
					             ")");

				foreach (var member in DeleteMembers)
				{
					path = member.Split('/');
					commands.Add("RMVM FILE(" + lib.Text.Trim() + "/" + path[0] + ") MBR(" + path[1] + ")");
				}

				foreach (var member in CreateMembers)
				{
					path = member.Key.Trim().Split('/');
					commands.Add("ADDPFM FILE(" +
					             lib.Text.Trim() +
					             "/" +
					             path[0] +
					             ") MBR(" +
					             path[1] +
					             ") SRCTYPE(" +
					             member.Value.Trim() +
					             ")");
				}
			}

			foreach (ListViewItem member in memberLog.Items)
				if (member.Checked)
				{
					path = member.Text.Trim().Split('/');
					var localFile = IBMiUtils.GetLocalFile(lib.Text.Trim(),
						path[0],
						path[1],
						UploadMembers[path[0] + '/' + path[1]]);

					pushList.Add(localFile,
						"/QSYS.lib/" + lib.Text.Trim() + ".lib/" + path[0] + ".file/" + path[1] + ".mbr");
				}

			if (!IBMi.RunCommands(commands.ToArray()))
			{
				MessageBox.Show("Push to server was not successful (stage 1)");

				return;
			}

			var success = true;
			foreach (var file in pushList)
				if (IBMi.UploadFile(file.Key, file.Value) == false)
					success = false;

			if (success)
			{
				MessageBox.Show("Push to server was successful.");
				FileCache.EditsClear();
				Close();
			}
			else
			{
				MessageBox.Show("Push to server was not successful (stage 2)");
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}