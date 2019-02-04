using System;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class TaskList : DockContent
	{
		private string FileName = "";

		public TaskList()
		{
			InitializeComponent();
		}

		public void Display(string name, TaskItem[] Items)
		{
			FileName = name;
			tasks.Items.Clear();

			foreach (var item in Items)
			{
				var keyword = item.Text.Substring(0, 4);
				tasks.Items.Add(new ListViewItem(new[] {item.Text, item.Line.ToString()},
					Array.IndexOf(Program.TaskKeywords, keyword)));
			}
		}

		private static void SelectTask(string File, int Line)
		{
			var theTab = Editor.TheEditor.GetTabByName(File, true);

			if (theTab != null)
			{
				var sourceEditor = Editor.TheEditor.GetTabEditor(theTab);

				sourceEditor.Focus();
				sourceEditor.GotoLine(Line, 0);
			}
			else
			{
				MessageBox.Show("Unable to open location. Please open the source manually first and then try again.",
					"Information",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
		}

		private void tasks_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (tasks.SelectedItems.Count > 0)
				SelectTask(FileName, int.Parse(tasks.SelectedItems[0].SubItems[1].Text) - 1);
		}
	}
}