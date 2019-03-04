using System;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class TaskList : DockContent
	{
		private string _fileName = "";

		public TaskList()
		{
			InitializeComponent();
		}

		public void Display(string name, TaskItem[] Items)
		{
			_fileName = name;
			tasks.Items.Clear();

			foreach (var item in Items)
			{
				var keyword = item.Text.Substring(0, 4);
				tasks.Items.Add(new ListViewItem(new[] {item.Text, item.Line.ToString()},
					Array.IndexOf(Program.TaskKeywords, keyword)));
			}
		}

		//todo the code of this method is almost identical to code of method OnSelectError in ErrorListing.cs.
		private static void SelectTask(string file, int line)
		{
			var theTab = Editor.TheEditor.GetTabByTitle(file, true);

			if (theTab != null)
			{
				var sourceEditor = Editor.TheEditor.GetTabEditor(theTab);

				sourceEditor.Focus();
				sourceEditor.GotoLine(line, 0);
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
				SelectTask(_fileName, int.Parse(tasks.SelectedItems[0].SubItems[1].Text) - 1);
		}
	}
}