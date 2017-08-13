using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.UserTools;
using ILEditor.Classes;
using System.Threading;
using System.IO;
using FastColoredTextBoxNS;

namespace ILEditor
{
    public partial class Editor : Form
    {
        public static Editor TheEditor;

        public Editor()
        {
            InitializeComponent();
            AddTool("Toolbox", new UserToolList());
            AddWelcome();
            TheEditor = this;
        }

        public void AddTool(string TabName, UserControl UserForm)
        {
            if (!usercontrol.TabPages.ContainsKey(TabName))
            {
                TabPage tabPage = new TabPage(TabName);
                UserForm.BringToFront();
                UserForm.Dock = DockStyle.Fill;
                tabPage.Controls.Add(UserForm);
                usercontrol.TabPages.Add(tabPage);
            }
        }

        private void AddWelcome()
        {
            TabPage tabPage = new TabPage("Welcome");
            Welcome WelcomeScrn = new Welcome();
            WelcomeScrn.BringToFront();
            WelcomeScrn.Dock = DockStyle.Fill;
            tabPage.Controls.Add(WelcomeScrn);
            editortabs.TabPages.Add(tabPage);
        }


        private int EditorContains(string Page)
        {
            for (int i = 0; i < editortabs.TabPages.Count; i++)
            {
                if (editortabs.TabPages[i].Text == Page)
                    return i;
            }

            return -1;
        }

        public void AddMemberEditor(Member MemberInfo)
        {
            string pageName = MemberInfo.GetLibrary() + "/" + MemberInfo.GetObject() + "." + MemberInfo.GetMember();
            int currentTab = EditorContains(pageName);

            //Close tab if it already exists.
            if (currentTab >= 0)
                editortabs.TabPages.RemoveAt(currentTab);

            TabPage tabPage = new TabPage(pageName);
            SourceEditor srcEdit = new SourceEditor(MemberInfo.GetLocalFile());
            srcEdit.BringToFront();
            srcEdit.Dock = DockStyle.Fill;
            tabPage.Tag = MemberInfo;
            tabPage.Controls.Add(srcEdit);
            editortabs.TabPages.Add(tabPage);

            editortabs.SelectTab(editortabs.TabPages.Count - 1);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                if (MemberInfo.IsEditable())
                {
                    FastColoredTextBox sourceCode = (FastColoredTextBox)editortabs.SelectedTab.Controls[0].Controls[0];
                    Thread gothread = new Thread((ThreadStart)delegate
                    {
                        File.WriteAllText(MemberInfo.GetLocalFile(), sourceCode.Text);
                        bool UploadResult = IBMiUtils.UploadMember(MemberInfo.GetLocalFile(), MemberInfo.GetLibrary(), MemberInfo.GetObject(), MemberInfo.GetMember());
                        if (UploadResult == false)
                        {
                            MessageBox.Show("Failed to upload to " + MemberInfo.GetMember() + ".");
                        }
                    });

                    gothread.Start();
                }
            } else
            {
                MessageBox.Show("This file is readonly.");
            }
        }
    }
}
