using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using ILEditor.Classes;
using System.Diagnostics;
using System.Net;

namespace ILEditor.UserTools
{

    public partial class Welcome : UserControl
    {
        public static void JustOpened(string Lib, string Obj)
        {
            List<string> SPFs = IBMi.CurrentSystem.GetValue("opened").Split('|').ToList();
            string Key = Lib + "/" + Obj;

            if (SPFs.Contains(Key))
                SPFs.Remove(Key);

            if (SPFs.Count >= 5)
                SPFs.RemoveAt(0);

            SPFs.Add(Lib + "/" + Obj);

            IBMi.CurrentSystem.SetValue("opened", String.Join("|", SPFs));
        }

        private static string[] GetRecents()
        {
            return IBMi.CurrentSystem.GetValue("opened").Split('|').Reverse().ToArray();
        }

        private void LoadItems()
        {
            recents.Clear();
            foreach (string Item in GetRecents())
            {
                if (Item.Trim() == "") continue;
                recents.Items.Add(new ListViewItem(Item, 0));
            }
        }

        public Welcome()
        {
            InitializeComponent();
            LoadItems();
        }

        private void recents_DoubleClick(object sender, EventArgs e)
        {
            if (recents.SelectedItems.Count == 1)
            {
                ListViewItem selection = recents.SelectedItems[0];
                string[] path = selection.Text.Split('/');
                Editor.TheEditor.AddTool("Member Browse", new MemberBrowse(path[0], path[1]));
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel label = (LinkLabel)sender;

            switch (label.Tag.ToString())
            {
                case "MBR":
                    new Forms.HelpWindow(Properties.Resources.OpenMember).Show();
                    break;
                case "LIBL":
                    new Forms.HelpWindow(Properties.Resources.LibraryList).Show();
                    break;
                case "CMP":
                    new Forms.HelpWindow(Properties.Resources.Compiling).Show();
                    break;
                case "OFFLINE":
                    new Forms.HelpWindow(Properties.Resources.OfflineMode).Show();
                    break;
                case "FTP":
                    new Forms.FirewallHelp().Show();
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

        private void devNews_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            devNews.Refresh(WebBrowserRefreshOption.Completely);
        }
    }
}
