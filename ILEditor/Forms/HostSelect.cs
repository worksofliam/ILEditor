using System;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class HostSelect : Form
	{
		public bool SystemSelected;

		public HostSelect()
		{
			InitializeComponent();
			SystemSelected = false;
			LoadListView();

			BringToFront();
			versionLabel.Text = "ILEditor " + Program.GetVersion();
		}

		public bool OfflineModeSelected()
		{
			return isOffline.Checked;
		}

		private void LoadListView()
		{
			systemlist.Clear();

			foreach (var system in GetSystemsList())
				systemlist.Items.Add(new ListViewItem(system, 0));
		}

		public static string[] GetSystemsList()
		{
			if (!Directory.Exists(Program.SYSTEMSDIR))
				Directory.CreateDirectory(Program.SYSTEMSDIR);

			var systems = Directory.GetFiles(Program.SYSTEMSDIR);

			for (var i = 0; i < systems.Length; i++)
				systems[i] = Path.GetFileName(systems[i]);

			return systems;
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void NewHost_Click(object sender, EventArgs e)
		{
			new NewSystem().ShowDialog();
			LoadListView();
		}

		private void SystemList_DoubleClick(object sender, EventArgs e)
		{
			if (systemlist.SelectedItems.Count == 1)
			{
				var configPath = Program.SYSTEMSDIR + @"\" + systemlist.SelectedItems[0].Text;
				IBMi.CurrentSystem = new Config(configPath);
				IBMi.CurrentSystem.DoSystemDefaults();
				SystemSelected = true;
				Close();
			}
		}

		private void SystemList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Delete || systemlist.SelectedItems.Count <= 0)
				return;

			var item = systemlist.SelectedItems[0];
			var result = MessageBox.Show("Are you sure you want to delete this setup?",
				"Warning",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (result == DialogResult.Yes)
			{
				var deleting = Program.SYSTEMSDIR + @"\" + item.Text;
				File.Delete(deleting);
				systemlist.Items.Remove(item);
			}
		}

		private void SystemList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			if (systemlist.FocusedItem.Bounds.Contains(e.Location))
				hostRightClick.Show(Cursor.Position);
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (systemlist.FocusedItem == null)
				return;

			var configPath = Program.SYSTEMSDIR + @"\" + systemlist.FocusedItem.Text;
			IBMi.CurrentSystem = new Config(configPath);
			IBMi.CurrentSystem.DoSystemDefaults();

			new Connection().ShowDialog();

			IBMi.CurrentSystem = null;
		}
	}
}