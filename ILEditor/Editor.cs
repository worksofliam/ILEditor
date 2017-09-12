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
using ILEditor.Forms;

namespace ILEditor
{
    public partial class Editor : Form
    {
        public static Editor TheEditor;

        private Boolean IsSourceTab = false;
        private int RightClickedTab = -1;

        public Editor()
        {
            InitializeComponent();
            this.Text += " (" + IBMi.CurrentSystem.GetValue("system") + ")";
            TheEditor = this;
        }
        
        private void Editor_Load(object sender, EventArgs e)
        {
            AddTool("Toolbox", new UserToolList());
            AddWelcome();
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

        #region MemberInfo
        private static readonly Dictionary<string, ILELanguage> LangTypes = new Dictionary<string, ILELanguage>()
        {
            { "RPG", ILELanguage.RPG },
            { "RPGLE", ILELanguage.RPG },
            { "SQLRPGLE", ILELanguage.RPG },
            { "CL", ILELanguage.CL },
            { "CLLE", ILELanguage.CL },
            { "CLP", ILELanguage.CL },
            { "CPP", ILELanguage.CPP },
            { "C", ILELanguage.CPP },
            { "SQL", ILELanguage.SQL }
        };

        public static ILELanguage GetBoundLangType(string Obj)
        {
            if (LangTypes.ContainsKey(Obj))
                return LangTypes[Obj];
            else
                return ILELanguage.None;
        }
        #endregion

        #region Tools Dropdown
        private void openToolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTool("Toolbox", new UserToolList());
        }

        private void openWelcomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddWelcome();
        }

        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Forms.Connection().ShowDialog();
        }
        
        private void libraryListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Forms.LibraryList().ShowDialog();
        }
        
        private void rPGConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                ILELanguage Language = GetBoundLangType(MemberInfo.GetExtension());
                if (Language == ILELanguage.RPG)
                {
                    SetStatus("Converting RPG in " + MemberInfo.GetMember());
                    GetTabEditor(editortabs.SelectedIndex).ConvertSelectedRPG();
                }
            }
        }
        
        private void cLFormatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                ILELanguage Language = Editor.GetBoundLangType(MemberInfo.GetExtension());
                if (Language == ILELanguage.CL)
                {
                    SetStatus("Formatting CL in " + MemberInfo.GetMember());
                    GetTabEditor(editortabs.SelectedIndex).FormatCL();
                }
            }
        }
        #endregion

        #region Compile

        private void compileCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileMember(MemberInfo);
                }).Start();
            }
        }

        private void compileAnyHandle(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileMember(MemberInfo);
                }).Start();
            }
        }

        private void compileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab == null) return;

            otherForTypeToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> Compiles = new List<ToolStripMenuItem>();
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                string[] Items = IBMi.CurrentSystem.GetValue("TYPE_" + MemberInfo.GetExtension()).Split('|');
                foreach (string Item in Items)
                {
                    if (Item.Trim() == "") continue;
                    Compiles.Add(new ToolStripMenuItem(Item, null, compileAnyHandle));
                }
            }

            compileCurrentToolStripMenuItem.Enabled = (Compiles.Count > 0);
            otherForTypeToolStripMenuItem.Enabled = (Compiles.Count > 0);
            otherForTypeToolStripMenuItem.DropDownItems.AddRange(Compiles.ToArray());
        }
        #endregion

        #region Editor
        public TabPage GetCurrentTab()
        {
            return editortabs.SelectedTab;
        }

        public int EditorContains(string Page)
        {
            for (int i = 0; i < editortabs.TabPages.Count; i++)
            {
                if (editortabs.TabPages[i].Text.StartsWith(Page))
                    return i;
            }

            return -1;
        }

        public void SwitchToTab(int index)
        {
            editortabs.SelectTab(index);
        }

        public SourceEditor GetTabEditor(int index)
        {
            if (editortabs.TabPages[index].Tag != null)
            {
                return (SourceEditor)editortabs.TabPages[index].Controls[0];
            }
            else
            {
                return null;
            }
        }

        public static void OpenMember(Member member)
        {
            string TabText = member.GetLibrary() + "/" + member.GetObject() + "(" + member.GetMember() + ")";
            int TabIndex = TheEditor.EditorContains(TabText);
            if (TabIndex == -1)
            {
                Thread gothread = new Thread((ThreadStart)delegate {
                    string resultFile = IBMiUtils.DownloadMember(member.GetLibrary(), member.GetObject(), member.GetMember());

                    if (resultFile != "")
                    {
                        member._Local = resultFile;
                        TheEditor.Invoke((MethodInvoker)delegate
                        {
                            TheEditor.AddMemberEditor(member, GetBoundLangType(member.GetExtension()));
                        });
                    }
                    else
                    {
                        MessageBox.Show("Unable to download member " + member.GetLibrary() + "/" + member.GetObject() + "." + member.GetMember() + ". Please check it exists and that you have access to the remote system.");
                    }

                });
                gothread.Start();
            }
            else
            {
                TheEditor.SwitchToTab(TabIndex);
            }
        }

        public void AddBindingList(string Lib, string Obj)
        {
            string pageName = Lib + "/" + Obj + " Binding Directory";
            int currentTab = EditorContains(pageName);

            //Close tab if it already exists.
            if (currentTab >= 0)
                editortabs.TabPages.RemoveAt(currentTab);

            TabPage tabPage = new TabPage(pageName);
            BindingDirectory bnddirlist = new BindingDirectory(Lib, Obj);
            bnddirlist.BringToFront();
            bnddirlist.Dock = DockStyle.Fill;
            tabPage.Controls.Add(bnddirlist);
            editortabs.TabPages.Add(tabPage);

            SwitchToTab(editortabs.TabPages.Count - 1);
        }

        private void AddMemberEditor(Member MemberInfo, ILELanguage Language = ILELanguage.None)
        {
            string pageName = MemberInfo.GetLibrary() + "/" + MemberInfo.GetObject() + "(" + MemberInfo.GetMember() + ")";
            int currentTab = EditorContains(pageName);

            //Close tab if it already exists.
            if (currentTab >= 0)
                editortabs.TabPages.RemoveAt(currentTab);

            TabPage tabPage = new TabPage(pageName);
            SourceEditor srcEdit = new SourceEditor(MemberInfo.GetLocalFile(), Language);
            srcEdit.BringToFront();
            srcEdit.Dock = DockStyle.Fill;
            tabPage.Tag = MemberInfo;
            tabPage.Controls.Add(srcEdit);
            editortabs.TabPages.Add(tabPage);

            SwitchToTab(editortabs.TabPages.Count - 1);
        }
        
        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMember newMemberForm = new NewMember();
            newMemberForm.ShowDialog();
            if (newMemberForm.created)
            {
                new Thread((ThreadStart)delegate {
                    string resultFile = IBMiUtils.DownloadMember(newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr);

                    if (resultFile != "")
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Editor.TheEditor.AddMemberEditor(new Member(resultFile, newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr, newMemberForm._type, true), GetBoundLangType(newMemberForm._type));
                        });
                    }
                }).Start();
            }
            newMemberForm.Dispose();
        }


        private void memberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new OpenMember().ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                if (MemberInfo.IsEditable())
                {
                    if (!MemberInfo._IsBeingSaved)
                    {
                        MemberInfo._IsBeingSaved = true;

                        SetStatus("Saving " + MemberInfo.GetMember() + "..");
                        FastColoredTextBox sourceCode = (FastColoredTextBox)editortabs.SelectedTab.Controls[0].Controls[0];
                        Thread gothread = new Thread((ThreadStart)delegate
                        {
                            File.WriteAllText(MemberInfo.GetLocalFile(), sourceCode.Text);
                            bool UploadResult = IBMiUtils.UploadMember(MemberInfo.GetLocalFile(), MemberInfo.GetLibrary(), MemberInfo.GetObject(), MemberInfo.GetMember());
                            if (UploadResult == false)
                            {
                                MessageBox.Show("Failed to upload to " + MemberInfo.GetMember() + ".");
                            }
                            else
                            {

                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (editortabs.SelectedTab.Text.EndsWith("*"))
                                        editortabs.SelectedTab.Text = editortabs.SelectedTab.Text.Substring(0, editortabs.SelectedTab.Text.Length - 1);
                                });
                            }

                            this.Invoke((MethodInvoker)delegate
                            {
                                SetStatus(MemberInfo.GetMember() + " " + (UploadResult ? "" : "not ") + "saved.");
                            });
                            MemberInfo._IsBeingSaved = false;
                        });

                        gothread.Start();
                    }

                }
            }
            else
            {
                MessageBox.Show("This file is readonly.");
            }
        }

        private void editortabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int ix = 0; ix < editortabs.TabCount; ++ix)
                {
                    if (editortabs.GetTabRect(ix).Contains(e.Location))
                    {
                        RightClickedTab = ix;
                        IsSourceTab = true;
                        toolstabrightclick.Show(Cursor.Position);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Tools
        public void AddTool(string TabName, UserControl UserForm)
        {
            if (!usercontrol.TabPages.ContainsKey(TabName))
            {
                TabPage tabPage = new TabPage(TabName);
                UserForm.BringToFront();
                UserForm.Dock = DockStyle.Fill;
                tabPage.Controls.Add(UserForm);

                this.Invoke((MethodInvoker)delegate
                {
                    usercontrol.TabPages.Add(tabPage);
                    usercontrol.SelectedIndex = usercontrol.TabPages.Count - 1;
                });
            }
        }

        private void usercontrol_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int ix = 0; ix < usercontrol.TabCount; ++ix)
                {
                    if (usercontrol.GetTabRect(ix).Contains(e.Location))
                    {
                        RightClickedTab = ix;
                        IsSourceTab = false;
                        toolstabrightclick.Show(Cursor.Position);
                        break;
                    }
                }
            }
        }
        #endregion

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSourceTab)
                editortabs.TabPages.RemoveAt(RightClickedTab);
            else
                usercontrol.TabPages.RemoveAt(RightClickedTab);
        }

        public void SetStatus(string Text)
        {
            statusLabel.Text = Text;
        }

        private void switchSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
