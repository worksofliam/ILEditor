using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class CloneWindow : Form
	{
		private Dictionary<string, string> CloneList;
		private List<string>               LocalSPFs;

		public CloneWindow()
		{
			InitializeComponent();
		}

		private void CloneWindow_Load(object sender, EventArgs e)
		{
			if (!IBMi.IsConnected())
			{
				MessageBox.Show("The SPF Clone tool does not work in Offline Mode.");
				Close();
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void fetch_Click(object sender, EventArgs e)
		{
			RemoteSource[] memberList;
			var            items = new List<ListViewItem>();
			LocalSPFs = new List<string>();

			if (!IBMiUtils.IsValueObjectName(lib.Text))
				MessageBox.Show("Library name not valid.");

			lib.Text = lib.Text.Trim();

			var files = IBMiUtils.GetSpfList(lib.Text);

			foreach (var Object in files)
			{
				memberList = IBMiUtils.GetMemberList(lib.Text, Object.Name);

				if (memberList == null)
					continue;

				LocalSPFs.Add(IBMiUtils.GetLocalDir(lib.Text, Object.Name));
				for (var i = 0; i < memberList.Length; i++)
				{
					var item = new ListViewItem(memberList[i].GetObject() +
					                            "/" +
					                            memberList[i].GetName() +
					                            "." +
					                            memberList[i].GetExtension().ToLower());

					item.Checked = true;
					item.Tag = new string[2]
					{
						memberList[i].GetObject() + "/" + memberList[i].GetName(),
						IBMiUtils.GetLocalFile(memberList[i].GetLibrary(),
							memberList[i].GetObject(),
							memberList[i].GetName(),
							memberList[i].GetExtension())
					};

					items.Add(item);
				}
			}

			this.memberList.Items.AddRange(items.ToArray());

			lib.Enabled        = false;
			fetch.Enabled      = false;
			clone.Enabled      = true;
			this.memberList.Enabled = true;
		}

		private void clone_Click(object sender, EventArgs e)
		{
			var commands = new List<string>(); // Unused

			CloneList = new Dictionary<string, string>();
			foreach (ListViewItem listItem in memberList.Items)
				if (listItem.Checked)
				{
					var member = (string[]) listItem.Tag;
					var path = member[0].Split('/');
					CloneList.Add("/QSYS.lib/" + lib.Text + ".lib/" + path[0] + ".file/" + path[1] + ".mbr", member[1]);
				}

			foreach (var dir in Directory.GetDirectories(IBMiUtils.GetLocalDir(lib.Text)))
				try
				{
					Directory.Delete(dir, true);
				}
				catch
				{
					// no catch code
				}

			foreach (var dir in LocalSPFs)
				Directory.CreateDirectory(dir);

			var isOkay = true;
			foreach (var file in CloneList)
				if (IBMi.DownloadFile(file.Value, file.Key)) //Error?
				{
					isOkay = false;

					break;
				}

			if (isOkay)
			{
				MessageBox.Show("Source-Physical File cloned sucessfully.",
					"SPF Clone",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);

				var location = Program.SOURCEDIR + "\\" + IBMi.CurrentSystem.GetValue("system") + "\\" + lib.Text;
				Process.Start("explorer.exe", "/select, " + location);
				Close();
			}
			else
			{
				MessageBox.Show("There was an error during the clone process.");
			}
		}
	}
}