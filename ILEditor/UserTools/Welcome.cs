using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Forms;
using ILEditor.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class Welcome : DockContent
	{
		public Welcome()
		{
			InitializeComponent();
			LoadItems();
		}

		public static void JustOpened(string Lib, string Obj)
		{
			var spFs = IBMi.CurrentSystem.GetValue("opened").Split('|').ToList();
			var key  = Lib + "/" + Obj;

			if (spFs.Contains(key))
				spFs.Remove(key);

			if (spFs.Count >= 5)
				spFs.RemoveAt(0);

			spFs.Add(Lib + "/" + Obj);

			IBMi.CurrentSystem.SetValue("opened", string.Join("|", spFs));
		}

		private static string[] GetRecents()
		{
			return IBMi.CurrentSystem.GetValue("opened").Split('|').Reverse().ToArray();
		}

		private void LoadItems()
		{
			recents.Clear();
			foreach (var item in GetRecents())
			{
				if (item.Trim() == "")
					continue;

				recents.Items.Add(new ListViewItem(item, 0));
			}
		}

		private void recents_DoubleClick(object sender, EventArgs e)
		{
			if (recents.SelectedItems.Count == 1)
			{
				var selection = recents.SelectedItems[0];
				var path      = selection.Text.Split('/');
				Editor.TheEditor.AddTool(new MemberBrowse(path[0], path[1]), DockState.DockRight);
			}
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var label = (LinkLabel) sender;

			switch (label.Tag.ToString())
			{
				case "MBR":
					new HelpWindow(Resources.OpenMember).Show();

					break;
				case "LIBL":
					new HelpWindow(Resources.LibraryList).Show();

					break;
				case "CMP":
					new HelpWindow(Resources.Compiling).Show();

					break;
				case "OFFLINE":
					new HelpWindow(Resources.OfflineMode).Show();

					break;
				case "DARK":
					new HelpWindow(Resources.DarkMode).Show();

					break;
			}
		}

		private void devNews_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			//cancel the current event
			e.Cancel = true;

			//this opens the URL in the user's default browser
			Process.Start(e.Url.ToString());
		}
	}
}