using System;
using System.Linq;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Forms.CompileOptionForms;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class CompileOptions : DockContent
	{
		public CompileOptions()
		{
			InitializeComponent();
			Text = "Compile Options";
		}

		private void CompileOptions_Load(object sender, EventArgs e)
		{
			reloadConfig();
		}

		private void reloadConfig()
		{
			commandList.Clear();

			var      headers = IBMi.CurrentSystem.GetValue("CMPTYPES").Split('|');

			foreach (var header in headers)
			{
				commandList.Groups.Add(new ListViewGroup(header, header));
				var commands = IBMi.CurrentSystem.GetValue("TYPE_" + header).Split('|');
				foreach (var command in commands)
				{
					if (command == "") continue;

					var imageIndex = IBMi.CurrentSystem.GetValue("DFT_" + header) == command ? 1 : 0;
					commandList.Items.Add(new ListViewItem(command, imageIndex, commandList.Groups[header]));
				}
			}
		}

		private void newType_Click(object sender, EventArgs e)
		{
			new NewType().ShowDialog();
			reloadConfig();
		}

		private void newCommand_Click(object sender, EventArgs e)
		{
			new EditCommand().ShowDialog();
			reloadConfig();
		}

	#region RightClick

		private void commandList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				rightClick.Show(Cursor.Position);
		}

		private void setDefaultForTypeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (commandList.SelectedItems.Count > 0)
			{
				var item = commandList.SelectedItems[0];
				IBMi.CurrentSystem.SetValue("DFT_" + item.Group.Name, item.Text);
				reloadConfig();
			}
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (commandList.SelectedItems.Count > 0)
			{
				var item = commandList.SelectedItems[0];
				new EditCommand(item.Group.Name, item.Text).ShowDialog();
				reloadConfig();
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (commandList.SelectedItems.Count > 0)
			{
				var item = commandList.SelectedItems[0];
				if (IBMi.CurrentSystem.GetValue("DFT_" + item.Group.Name) != item.Text)
				{
					var commands = IBMi.CurrentSystem.GetValue("TYPE_" + item.Group.Name).Split('|').ToList();
					commands.Remove(item.Text);
					IBMi.CurrentSystem.SetValue("TYPE_" + item.Group.Name, string.Join("|", commands));
					reloadConfig();
				}
				else
				{
					MessageBox.Show("Unable to delete command setting which is currently the default for a group.");
				}
			}
		}

	#endregion
	}
}