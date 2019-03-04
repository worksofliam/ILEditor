using System;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class QuickFileSearch : Form
	{
		public QuickFileSearch()
		{
			InitializeComponent();
			nameValue.Focus();
		}

		private void fileValue_TextChanged(object sender, EventArgs e)
		{
			new Thread(() => InitSearch(nameValue.Text)).Start();
		}

		private void InitSearch(string Value)
		{
			var results = FileCache.Find(Value);

			Invoke((MethodInvoker) delegate
			{
				fileList.Items.Clear();
				fileList.Items.AddRange(results);
			});
		}

		private void QuickFileSearch_Deactivate(object sender, EventArgs e)
		{
			Close();
		}

		private void fileValue_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					if (fileList.Items.Count > 0)
						SelectFile(fileList.Items[0].ToString());

					break;
				case Keys.Down:
					fileList.Focus();

					break;
				case Keys.Escape:
					Close();

					break;
			}
		}

		private void fileList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (fileList.SelectedItem != null)
				SelectFile(fileList.SelectedItem.ToString());
		}

		private void fileList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter)
				return;

			if (fileList.SelectedItem != null)
				SelectFile(fileList.SelectedItem.ToString());
		}

		private void SelectFile(string SourcePath)
		{
			if (SourcePath == string.Empty)
				return;

			RemoteSource sourceFile;
			if (SourcePath.StartsWith("/"))
			{
				sourceFile = new RemoteSource(string.Empty, SourcePath);
			}
			else
			{
				var type = FileCache.GetType(SourcePath);
				var data = SourcePath.Split(new[] {'/', '.'}, StringSplitOptions.RemoveEmptyEntries);

				sourceFile = new RemoteSource(string.Empty, data[0], data[1], data[2], type);
			}

			Editor.OpenSource(sourceFile);

			Close();
		}
	}
}