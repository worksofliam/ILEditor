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
using ILEditor.Forms;
using System.Diagnostics;

namespace ILEditor
{
    public partial class Editor : Form
    {
        public static Editor TheEditor;

        private TabControl editortabs;
        private TabControl usercontrol;

        private Boolean IsSourceTab = false;
        private int RightClickedTab = -1;

        public Editor()
        {
            InitializeComponent();
            TheEditor = this;

            SetUpPanels();
            
            this.Text += " (" + IBMi.CurrentSystem.GetValue("alias") + ")";
            MemberCache.Import();
        }

        private void SetUpPanels()
        {
            this.editortabs = new TabControl();
            this.usercontrol = new TabControl();
            editortabs.ImageList = tabImageList;

            // 
            // editortabs
            // 
            this.editortabs.Dock = DockStyle.Fill;
            this.editortabs.HotTrack = true;
            this.editortabs.ItemSize = new Size(0, 25);
            this.editortabs.Location = new Point(0, 0);
            this.editortabs.Name = "editortabs";
            this.editortabs.SelectedIndex = 0;
            this.editortabs.Size = new Size(591, 531);
            this.editortabs.TabIndex = 0;
            this.editortabs.MouseClick += new MouseEventHandler(this.editortabs_MouseClick);

            // 
            // usercontrol
            // 
            this.usercontrol.Dock = DockStyle.Fill;
            this.usercontrol.ItemSize = new Size(0, 25);
            this.usercontrol.Location = new Point(0, 0);
            this.usercontrol.Name = "usercontrol";
            this.usercontrol.SelectedIndex = 0;
            this.usercontrol.Size = new Size(238, 531);
            this.usercontrol.TabIndex = 0;
            this.usercontrol.MouseClick += new MouseEventHandler(this.usercontrol_MouseClick);

            string side = Program.Config.GetValue("toolbarSide");
            if (side == "Right")
            {
                this.splitContainer1.Panel1.Controls.Add(editortabs);
                this.splitContainer1.Panel2.Controls.Add(usercontrol);
                this.splitContainer1.SplitterDistance = 591;
            }
            else
            {
                this.splitContainer1.Panel1.Controls.Add(usercontrol);
                this.splitContainer1.Panel2.Controls.Add(editortabs);
                this.splitContainer1.SplitterDistance = 166;
            }
        }
        
        private void Editor_Load(object sender, EventArgs e)
        {
            AddTool("Toolbox", new UserToolList());
            AddWelcome();
        }

        private void AddWelcome()
        {
            TabPage tabPage = new TabPage("Welcome");
            tabPage.ImageIndex = 3;
            Welcome WelcomeScrn = new Welcome();
            WelcomeScrn.BringToFront();
            WelcomeScrn.Dock = DockStyle.Fill;
            tabPage.Controls.Add(WelcomeScrn);
            editortabs.TabPages.Add(tabPage);
        }

        #region MemberInfo
        public static readonly Dictionary<string, ILELanguage> LangTypes = new Dictionary<string, ILELanguage>()
        {
            { "RPG", ILELanguage.RPG },
            { "RPGLE", ILELanguage.RPG },
            { "SQLRPGLE", ILELanguage.RPG },
            { "CL", ILELanguage.CL },
            { "CLLE", ILELanguage.CL },
            { "CLP", ILELanguage.CL },
            { "CMD", ILELanguage.CL },
            { "CPP", ILELanguage.CPP },
            { "C", ILELanguage.CPP },
            { "SQL", ILELanguage.SQL },
            { "CBL", ILELanguage.COBOL },
            { "COBOL", ILELanguage.COBOL },
            { "CBLLE", ILELanguage.COBOL }
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
        
        private void start5250EmulatorACSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Program.Config.GetValue("acspath");
            if (path == "false")
                MessageBox.Show("Please setup the ACS path in the Connection Setup.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                try
                {
                    Process.Start(path, " /plugin=5250 /sso /system=" + IBMi.CurrentSystem.GetValue("system"));
                }
                catch
                {
                    MessageBox.Show("Something went wrong launching the 5250 session.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void openToolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTool("Toolbox", new UserToolList(), true);
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
            if (editortabs.SelectedIndex >= 0)
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
        }
        
        private void cLFormatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedIndex >= 0)
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
        }
        
        private void sPFCloneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new CloneWindow().ShowDialog();
        }
        
        private void sPFPushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PushWindow().ShowDialog();
        }

        private void serviceProgramGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServiceGenerator().ShowDialog();
        }
        
        private void searchMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MemberSearch().ShowDialog();
        }
        
        private void quickMemberSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new QuickMemberSearch().Show();
        }

        private void compareMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lib = "", spf = "", mbr = "";
            if (editortabs.SelectedIndex >= 0)
            {
                if (editortabs.SelectedTab.Tag != null)
                {
                    Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                    lib = MemberInfo.GetLibrary();
                    spf = MemberInfo.GetObject();
                    mbr = MemberInfo.GetMember();
                }
            }
            new MemberCompareSelect(lib, spf, mbr).ShowDialog();
        }
        
        private void generateSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSelect SelectFile = new FileSelect();
            SelectFile.ShowDialog();

            if (SelectFile.Success)
            {
                new Thread((ThreadStart)delegate {
                    string resultFile = IBMiUtils.DownloadMember("QTEMP", "Q_GENSQL", "Q_GENSQL", new[] { SelectFile.getCommand() }, "SQL");

                    if (resultFile != "")
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Editor.TheEditor.AddMemberEditor(new Member(resultFile, "QTEMP", "Q_GENSQL", "Q_GENSQL", "SQL", false), GetBoundLangType("SQL"));
                        });
                    }
                }).Start();
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
                    IBMiUtils.CompileMember(MemberInfo, clickedItem.Text);
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
                    string resultFile = IBMiUtils.DownloadMember(member.GetLibrary(), member.GetObject(), member.GetMember(), null, member.GetExtension());

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

        public void AddSpoolFile(string pageName, string Local)
        {
            pageName += " Spool";
            int currentTab = EditorContains(pageName);

            TabPage tabPage = new TabPage(pageName);
            tabPage.ImageIndex = 2;
            SpoolViewer SpoolFile = new SpoolViewer(Local);
            SpoolFile.BringToFront();
            SpoolFile.Dock = DockStyle.Fill;
            tabPage.Controls.Add(SpoolFile);
            editortabs.TabPages.Add(tabPage);

            SwitchToTab(editortabs.TabPages.Count - 1);
        }

        public void AddBindingList(string Lib, string Obj)
        {
            string pageName = Lib + "/" + Obj + " Binding Directory";
            int currentTab = EditorContains(pageName);

            //Close tab if it already exists.
            if (currentTab >= 0)
                editortabs.TabPages.RemoveAt(currentTab);

            TabPage tabPage = new TabPage(pageName);
            tabPage.ImageIndex = 1;
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
            tabPage.ImageIndex = 0;
            tabPage.ToolTipText = MemberInfo.GetLibrary() + "/" + MemberInfo.GetObject() + "(" + MemberInfo.GetMember() + ")";
            SourceEditor srcEdit = new SourceEditor(MemberInfo.GetLocalFile(), Language, MemberInfo.GetRecordLength());
            srcEdit.SetReadOnly(!MemberInfo.IsEditable());
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
                    string resultFile = IBMiUtils.DownloadMember(newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr, null, newMemberForm._type);

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
        
        private void closeMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedIndex >= 0)
            {
                if (editortabs.TabPages[editortabs.SelectedIndex].Text.EndsWith("*"))
                {
                    MessageBox.Show("Cannot close this member because there are unsaved changes.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    editortabs.TabPages.RemoveAt(editortabs.SelectedIndex);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                if (!editortabs.TabPages[editortabs.SelectedIndex].Text.EndsWith("*"))
                {
                    SaveAs SaveAsWindow = new SaveAs();
                    SaveAsWindow.ShowDialog();
                    if (SaveAsWindow.Success)
                    {
                        Member MemberInfo = (Member)editortabs.SelectedTab.Tag;
                        if (!MemberInfo._IsBeingSaved)
                        {
                            MemberInfo._IsBeingSaved = true;

                            SetStatus("Saving " + SaveAsWindow.Mbr + "..");
                            SourceEditor sourceCode = (SourceEditor)editortabs.SelectedTab.Controls[0];
                            Thread gothread = new Thread((ThreadStart)delegate
                            {
                                bool UploadResult = IBMiUtils.UploadMember(MemberInfo.GetLocalFile(), SaveAsWindow.Lib, SaveAsWindow.Spf, SaveAsWindow.Mbr);
                                if (UploadResult == false)
                                    MessageBox.Show("Failed to upload to " + SaveAsWindow.Mbr + ".");

                                this.Invoke((MethodInvoker)delegate
                                {
                                    SetStatus(SaveAsWindow.Mbr + " " + (UploadResult ? "" : "not ") + "saved.");
                                });

                                MemberInfo._IsBeingSaved = false;
                            });

                            gothread.Start();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must save the source before you can Save-As.");
                }
            }
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
                        SourceEditor sourceCode = (SourceEditor)editortabs.SelectedTab.Controls[0];
                        Thread gothread = new Thread((ThreadStart)delegate
                        {

                            this.Invoke((MethodInvoker)delegate
                            {
                                File.WriteAllText(MemberInfo.GetLocalFile(), sourceCode.GetText());
                            });
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
                else
                {
                    MessageBox.Show("This file is readonly.");
                }
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

        public void AddTool(string TabName, UserControl UserForm, Boolean Replace = false)
        {
            if (Replace)
            {
                for (int i = 0; i < usercontrol.TabPages.Count; i++)
                {
                    if (usercontrol.TabPages[i].Text.Equals(TabName))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            usercontrol.TabPages.RemoveAt(i);
                        });
                    }
                }
            }
            
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

        #region Toolbar
        private void newButton_Click(object sender, EventArgs e)
        {
            memberToolStripMenuItem.PerformClick();
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.PerformClick();
        }

        private void liblButton_Click(object sender, EventArgs e)
        {
            libraryListToolStripMenuItem.PerformClick();
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            compileCurrentToolStripMenuItem.PerformClick();
        }
        
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                SourceEditor sourceCode = (SourceEditor)editortabs.SelectedTab.Controls[0];
                sourceCode.Zoom(+1f);
            }
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (editortabs.SelectedTab.Tag != null)
            {
                SourceEditor sourceCode = (SourceEditor)editortabs.SelectedTab.Controls[0];
                sourceCode.Zoom(-1f);
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

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCache.Export();
        }
    }
}
