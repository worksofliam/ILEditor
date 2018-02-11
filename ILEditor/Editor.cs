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

        public SourceEditor LastEditing;

        private TabControlExtra editortabsleft;
        private TabControlExtra editortabsright;
        private TabControlExtra usercontrol;
        private SplitContainer editortabs;

        private OpenTab RightClickedTab = null;

        public Editor()
        {
            InitializeComponent();
            TheEditor = this;
            
            MemberCache.Import();
            SetUpPanels();
            
            this.Text += ' ' + Program.getVersion() + " (" + IBMi.CurrentSystem.GetValue("alias") + ")";
            if (!IBMi.IsConnected())
                this.Text += " - Offline Mode";

            if (IBMi.IsConnected())
            {
                if (IBMi.CurrentSystem.GetValue("lastOffline") == "true")
                {
                    DialogResult result = MessageBox.Show("Looks like your last session was in Offline Mode. Would you like the launch the SPF Push tool?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        new PushWindow().ShowDialog();
                    }
                }
            }
            IBMi.CurrentSystem.SetValue("lastOffline", (IBMi.IsConnected() == false).ToString().ToLower());
        }

        private void SetUpPanels()
        {
            editortabs = new SplitContainer();
            this.editortabsleft = new TabControlExtra();
            this.editortabsright = new TabControlExtra();
            this.usercontrol = new TabControlExtra();

            editortabs.Dock = DockStyle.Fill;

            // 
            // editortabsleft
            // 
            this.editortabsleft.DisplayStyleProvider.TabColorSelected1 = Color.White;
            this.editortabsleft.DisplayStyleProvider.TabColorUnSelected1 = Color.White;
            this.editortabsleft.DisplayStyleProvider.TabColorFocused1 = Color.White;

            this.editortabsleft.DisplayStyleProvider.CloserColorSelectedActive = Color.Black;
            this.editortabsleft.DisplayStyleProvider.ShowTabCloser = true;
            this.editortabsleft.DisplayStyleProvider.HotTrack = true;
            this.editortabsleft.Dock = DockStyle.Fill;
            this.editortabsleft.ItemSize = new Size(0, 20);
            this.editortabsleft.Name = "editortabsleft";
            this.editortabsleft.SelectedIndex = 0;
            this.editortabsleft.TabIndex = 0;
            this.editortabsleft.ImageList = tabImageList;
            this.editortabsleft.MouseClick += new MouseEventHandler(this.editortabs_MouseClick);
            this.editortabsleft.ControlAdded += Editortabs_ControlAdded;
            this.editortabsleft.TabClosed += Editortabs_TabClosed;

            // 
            // editortabsright
            // 
            this.editortabsright.DisplayStyleProvider.TabColorSelected1 = Color.White;
            this.editortabsright.DisplayStyleProvider.TabColorUnSelected1 = Color.White;
            this.editortabsright.DisplayStyleProvider.TabColorFocused1 = Color.White;

            this.editortabsright.DisplayStyleProvider.CloserColorSelectedActive = Color.Black;
            this.editortabsright.DisplayStyleProvider.ShowTabCloser = true;
            this.editortabsright.DisplayStyleProvider.HotTrack = true;
            this.editortabsright.Dock = DockStyle.Fill;
            this.editortabsright.ItemSize = new Size(0, 20);
            this.editortabsright.Name = "editortabsright";
            this.editortabsright.SelectedIndex = 0;
            this.editortabsright.TabIndex = 0;
            this.editortabsright.ImageList = tabImageList;
            this.editortabsright.MouseClick += new MouseEventHandler(this.editortabs_MouseClick);
            this.editortabsright.ControlAdded += Editortabs_ControlAdded;
            this.editortabsright.TabClosed += Editortabs_TabClosed;

            // 
            // usercontrol
            // 
            this.usercontrol.DisplayStyleProvider.TabColorSelected1 = Color.White;
            this.usercontrol.DisplayStyleProvider.TabColorUnSelected1 = Color.White;
            this.usercontrol.DisplayStyleProvider.TabColorFocused1 = Color.White;

            this.usercontrol.DisplayStyleProvider.CloserColorSelectedActive = Color.Black;
            this.usercontrol.DisplayStyleProvider.ShowTabCloser = true;
            this.usercontrol.DisplayStyleProvider.HotTrack = true;
            this.usercontrol.Dock = DockStyle.Fill;
            this.usercontrol.ItemSize = new Size(0, 20);
            this.usercontrol.Name = "usercontrol";
            this.usercontrol.SelectedIndex = 0;
            this.usercontrol.TabIndex = 0;

            editortabs.Panel1.Controls.Add(editortabsleft);
            editortabs.Panel2.Controls.Add(editortabsright);
            editortabs.Panel2Collapsed = true;

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

        private void Editortabs_TabClosed(object sender, TabControlEventArgs e)
        {
            RemoteSource source;
            if (e.TabPage.Tag is RemoteSource)
            {
                source = e.TabPage.Tag as RemoteSource;
                source.Unlock();
            }

            e.TabPage.Dispose();
            FixEditorSplitters();
        }

        private void Editortabs_ControlAdded(object sender, ControlEventArgs e)
        {
            FixEditorSplitters();
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
            editortabsleft.TabPages.Add(tabPage);
        }

        #region SourceInfo
        public static readonly Dictionary<string, Language> LangTypes = new Dictionary<string, Language>()
        {
            { "RPG", Language.RPG },
            { "RPGLE", Language.RPG },
            { "SQLRPGLE", Language.RPG },
            { "CL", Language.CL },
            { "CLLE", Language.CL },
            { "CLP", Language.CL },
            { "CMD", Language.CL },
            { "CPP", Language.CPP },
            { "C", Language.CPP },
            { "SQL", Language.SQL },
            { "CBL", Language.COBOL },
            { "COBOL", Language.COBOL },
            { "CBLLE", Language.COBOL },
            { "PYTHON", Language.Python },
            { "PY", Language.Python }
        };

        public static Language GetBoundLangType(string Obj)
        {
            Obj = Obj.ToUpper();
            if (LangTypes.ContainsKey(Obj))
                return LangTypes[Obj];
            else
                return Language.None;
        }
        #endregion

        #region Tools Dropdown
        
        private void start5250EmulatorACSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Program.Config.GetValue("acspath");
            if (path == "false")
                MessageBox.Show("Please setup the ACS path in the Connection Settings.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (this.LastEditing.Tag != null)
            {
                RemoteSource SourceInfo = (RemoteSource)this.LastEditing.Tag;
                Language Language = GetBoundLangType(SourceInfo.GetExtension());
                if (Language == Language.RPG)
                {
                    SetStatus("Converting RPG in " + SourceInfo.GetName());
                    LastEditing.ConvertSelectedRPG();
                }
            }
        }
        
        private void cLFormatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LastEditing.Tag != null)
            {
                RemoteSource SourceInfo = (RemoteSource)this.LastEditing.Tag;
                Language Language = Editor.GetBoundLangType(SourceInfo.GetExtension());
                if (Language == Language.CL)
                {
                    SetStatus("Formatting CL in " + SourceInfo.GetName());
                    LastEditing.FormatCL();
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
            if (this.LastEditing.Tag != null)
            {
                RemoteSource MemberInfo = (RemoteSource)this.LastEditing.Tag;
                lib = MemberInfo.GetLibrary();
                spf = MemberInfo.GetObject();
                mbr = MemberInfo.GetName();
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
                    IBMi.RemoteCommand(SelectFile.getCommand());
                    string resultFile = IBMiUtils.DownloadMember("QTEMP", "Q_GENSQL", "Q_GENSQL", "SQL");

                    if (resultFile != "")
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Editor.TheEditor.AddSourceEditor(new RemoteSource(resultFile, "QTEMP", "Q_GENSQL", "Q_GENSQL", "SQL", false), GetBoundLangType("SQL"));
                        });
                    }
                }).Start();
            }
        }

        private void quickCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
            {
                LastEditing.CommentOutSelected();
            }
        }
        #endregion

        #region Compile

        private void compileCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
            {
                RemoteSource SourceInfo = (RemoteSource)LastEditing.Tag;
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileSource(SourceInfo);
                }).Start();
            }
        }

        private void compileAnyHandle(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (LastEditing.Tag != null)
            {
                RemoteSource SourceInfo = (RemoteSource)LastEditing.Tag;
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileSource(SourceInfo, clickedItem.Text);
                }).Start();
            }
        }

        private void compileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (editortabsleft.SelectedTab == null) return;

            otherForTypeToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> Compiles = new List<ToolStripMenuItem>();
            if (editortabsleft.SelectedTab.Tag != null)
            {
                RemoteSource SourceInfo = (RemoteSource)LastEditing.Tag;
                string[] Items = IBMi.CurrentSystem.GetValue("TYPE_" + SourceInfo.GetExtension()).Split('|');
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
            return editortabsleft.SelectedTab;
        }

        public OpenTab EditorContains(string Page)
        {
            OpenTab result = null;

            for (int i = 0; i < editortabsleft.TabPages.Count; i++)
            {
                if (editortabsleft.TabPages[i].Text.StartsWith(Page))
                    result = new OpenTab(OpenTab.TAB_SIDE.Left, i);
            }

            for (int i = 0; i < editortabsright.TabPages.Count; i++)
            {
                if (editortabsleft.TabPages[i].Text.StartsWith(Page))
                    result = new OpenTab(OpenTab.TAB_SIDE.Left, i);
            }

            return result;
        }

        public void SwitchToTab(OpenTab.TAB_SIDE side, int index)
        {
            switch (side)
            {
                case OpenTab.TAB_SIDE.Left:
                    editortabsleft.SelectTab(index);
                    break;
                case OpenTab.TAB_SIDE.Right:
                    editortabsright.SelectTab(index);
                    break;
            }
        }

        public SourceEditor GetTabEditor(OpenTab Tab)
        {
            switch (Tab.getSide())
            {
                case OpenTab.TAB_SIDE.Left:
                    return (SourceEditor)editortabsleft.TabPages[Tab.getIndex()].Controls[0];
                case OpenTab.TAB_SIDE.Right:
                    return (SourceEditor)editortabsright.TabPages[Tab.getIndex()].Controls[0];
            }
            return null;
        }

        public static void OpenLocal(string FilePath)
        {
            string Extension = Path.GetExtension(FilePath).Substring(1);
            TheEditor.AddFileEditor(FilePath, GetBoundLangType(Extension));
        }

        public static void OpenSource(RemoteSource source)
        {
            string resultFile = "";
            Thread gothread = new Thread((ThreadStart)delegate {
                switch (source.GetFS())
                {
                    case FileSystem.QSYS:
                        resultFile = IBMiUtils.DownloadMember(source.GetLibrary(), source.GetObject(), source.GetName(), source.GetExtension());
                        break;
                    case FileSystem.IFS:
                        resultFile = IBMiUtils.DownloadFile(source.GetRemoteFile());
                        break;
                }

                if (resultFile != "")
                {
                    source._Local = resultFile;
                    //LOCK HERE
                    source.Lock();
                    TheEditor.Invoke((MethodInvoker)delegate
                    {
                        TheEditor.AddSourceEditor(source, GetBoundLangType(source.GetExtension()));
                    });
                }
                else
                {
                    switch (source.GetFS())
                    {
                        case FileSystem.QSYS:
                            MessageBox.Show("Unable to download member " + source.GetLibrary() + "/" + source.GetObject() + "." + source.GetName() + ". Please check it exists and that you have access to the remote system.");
                            break;
                    }
                }

            });
            gothread.Start();
        }

        public void AddSpoolFile(string pageName, string Local)
        {
            pageName += " Spool";

            TabPage tabPage = new TabPage(pageName);
            tabPage.ImageIndex = 2;
            SpoolViewer SpoolFile = new SpoolViewer(Local);
            SpoolFile.BringToFront();
            SpoolFile.Dock = DockStyle.Fill;
            tabPage.Controls.Add(SpoolFile);
            editortabsleft.TabPages.Add(tabPage);

            SwitchToTab(OpenTab.TAB_SIDE.Left, editortabsleft.TabPages.Count - 1);
        }

        public void AddBindingList(string Lib, string Obj)
        {
            string pageName = Lib + "/" + Obj + " Binding Directory";
            OpenTab currentTab = EditorContains(pageName);

            //Close tab if it already exists.
            if (currentTab != null)
            {
                switch (currentTab.getSide())
                {
                    case OpenTab.TAB_SIDE.Left:
                        editortabsleft.TabPages.RemoveAt(currentTab.getIndex());
                        break;
                    case OpenTab.TAB_SIDE.Right:
                        editortabsright.TabPages.RemoveAt(currentTab.getIndex());
                        break;
                }
            }

            TabPage tabPage = new TabPage(pageName);
            tabPage.ImageIndex = 1;
            BindingDirectory bnddirlist = new BindingDirectory(Lib, Obj);
            bnddirlist.BringToFront();
            bnddirlist.Dock = DockStyle.Fill;
            tabPage.Controls.Add(bnddirlist);
            editortabsleft.TabPages.Add(tabPage);

            SwitchToTab(OpenTab.TAB_SIDE.Left, editortabsleft.TabPages.Count - 1);
        }
        
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (RightClickedTab.getSide())
            {
                case OpenTab.TAB_SIDE.Left:
                    editortabsright.TabPages.Add(editortabsleft.TabPages[RightClickedTab.getIndex()]);
                    break;
                case OpenTab.TAB_SIDE.Right:
                    editortabsleft.TabPages.Add(editortabsright.TabPages[RightClickedTab.getIndex()]);
                    break;
            }
        }

        public void FixEditorSplitters()
        {
            editortabs.Panel1Collapsed = (editortabsleft.TabPages.Count == 0);
            editortabs.Panel2Collapsed = (editortabsright.TabPages.Count == 0);
        }

        private void AddFileEditor(string FilePath, Language Language)
        {
            string pageName = Path.GetFileName(FilePath);
            OpenTab currentTab = EditorContains(pageName);

            if (File.Exists(FilePath))
            {
                if (currentTab != null)
                {
                    switch (currentTab.getSide())
                    {
                        case OpenTab.TAB_SIDE.Left:
                            editortabsleft.TabPages.RemoveAt(currentTab.getIndex());
                            break;
                        case OpenTab.TAB_SIDE.Right:
                            editortabsright.TabPages.RemoveAt(currentTab.getIndex());
                            break;
                    }
                }

                TabPage tabPage = new TabPage(pageName);
                tabPage.ImageIndex = 0;
                tabPage.ToolTipText = pageName;
                SourceEditor srcEdit = new SourceEditor(FilePath, Language);
                srcEdit.BringToFront();
                srcEdit.Dock = DockStyle.Fill;
                srcEdit.Tag = null;

                tabPage.Tag = null;
                tabPage.Controls.Add(srcEdit);
                editortabsleft.TabPages.Add(tabPage);

                SwitchToTab(OpenTab.TAB_SIDE.Left, editortabsleft.TabPages.Count - 1);
            }
        }

        public void AddSourceEditor(RemoteSource SourceInfo, Language Language = Language.None)
        {
            string pageName = "";
            switch (SourceInfo.GetFS())
            {
                case FileSystem.QSYS:
                    pageName = SourceInfo.GetLibrary() + "/" + SourceInfo.GetObject() + "(" + SourceInfo.GetName() + ")";
                    break;
                case FileSystem.IFS:
                    pageName = SourceInfo.GetName() + "." + SourceInfo.GetExtension();
                    break;
            }
            
            OpenTab currentTab = EditorContains(pageName);

            if (File.Exists(SourceInfo.GetLocalFile()))
            {

                //Close tab if it already exists.
                if (currentTab != null)
                {
                    switch (currentTab.getSide())
                    {
                        case OpenTab.TAB_SIDE.Left:
                            editortabsleft.TabPages.RemoveAt(currentTab.getIndex());
                            break;
                        case OpenTab.TAB_SIDE.Right:
                            editortabsright.TabPages.RemoveAt(currentTab.getIndex());
                            break;
                    }
                }

                TabPage tabPage = new TabPage(pageName);
                tabPage.ImageIndex = 0;
                tabPage.ToolTipText = pageName;
                SourceEditor srcEdit = new SourceEditor(SourceInfo.GetLocalFile(), Language, SourceInfo.GetRecordLength());
                srcEdit.SetReadOnly(!SourceInfo.IsEditable());
                srcEdit.BringToFront();
                srcEdit.Dock = DockStyle.Fill;
                srcEdit.Tag = SourceInfo;

                tabPage.Tag = SourceInfo;
                tabPage.Controls.Add(srcEdit);
                editortabsleft.TabPages.Add(tabPage);

                SwitchToTab(OpenTab.TAB_SIDE.Left, editortabsleft.TabPages.Count - 1);
            }
            else
            {
                MessageBox.Show("There was an error opening the local member. '" + SourceInfo.GetLocalFile() + "' does not exist");
            }
        }
        
        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoteSource member;
            NewMember newMemberForm = new NewMember();

            newMemberForm.ShowDialog();
            if (newMemberForm.created)
            {
                new Thread((ThreadStart)delegate {

                    string resultFile = IBMiUtils.DownloadMember(newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr, (newMemberForm._type == "*NONE" ? "" : newMemberForm._type));

                    if (resultFile != "")
                    {
                        member = new RemoteSource(resultFile, newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr, newMemberForm._type, true);
                        //LOCK HERE
                        member.Lock();
                        this.Invoke((MethodInvoker)delegate
                        {
                            Editor.TheEditor.AddSourceEditor(member, GetBoundLangType(newMemberForm._type));
                        });
                    }
                }).Start();
            }
            newMemberForm.Dispose();
        }
        
        private void streamFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateStreamFile window = new CreateStreamFile();
            window.ShowDialog();

            if (window.result != null)
                Editor.TheEditor.AddSourceEditor(window.result, GetBoundLangType(window.result.GetExtension()));
        }

        private void sourcePhysicalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewSPF().ShowDialog();
        }

        private void memberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new OpenSource(0).ShowDialog();
        }
        
        private void streamFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OpenSource(1).ShowDialog();
        }

        private void localFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ext;
            using (OpenFileDialog fileSelect = new OpenFileDialog())
            {
                DialogResult result = fileSelect.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    ext = Path.GetExtension(fileSelect.FileName);
                    if (ext.StartsWith(".")) ext = ext.Substring(1);

                    AddFileEditor(fileSelect.FileName, GetBoundLangType(ext));
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LastEditing != null)
            {
                this.LastEditing.SaveAs();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LastEditing != null)
            {
                this.LastEditing.Save();
            }
        }

        private void editortabs_MouseClick(object sender, MouseEventArgs e)
        {
            Control fromTabs = sender as Control;
            OpenTab.TAB_SIDE side = OpenTab.TAB_SIDE.None;
            TabControlExtra Tab = null;
            switch (fromTabs.Name)
            {
                case "editortabsleft":
                    side = OpenTab.TAB_SIDE.Left;
                    Tab = editortabsleft;
                    break;
                case "editortabsright":
                    side = OpenTab.TAB_SIDE.Right;
                    Tab = editortabsright;
                    break;
            }

            if (e.Button == MouseButtons.Right)
            {
                for (int ix = 0; ix < Tab.TabCount; ++ix)
                {
                    if (Tab.GetTabRect(ix).Contains(e.Location))
                    {
                        RightClickedTab = new OpenTab(side, ix);
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
            if (LastEditing != null)
                LastEditing.Zoom(+1f);
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.Zoom(-1f);
        }
        #endregion
        
        public void SetStatus(string Text)
        {
            statusLabel.Text = Text;
        }

        public void SetColumnLabel(string Text)
        {
            columnLabel.Text = Text;
        }

        private void switchSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCache.Export();
        }

        #region Help
        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please send any feedback for ILEditor to feedback@worksofbarry.com.", "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutILEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
        
        private void sessionFTPLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(IBMi.FTPFile);
        }
        #endregion
    }

    public class OpenTab
    {
        public enum TAB_SIDE
        {
            None, Left, Right
        }

        private TAB_SIDE _Side;
        private int _Index;
        public OpenTab(TAB_SIDE side, int tabindex)
        {
            this._Side = side;
            this._Index = tabindex;
        }

        public TAB_SIDE getSide() => this._Side;
        public int getIndex() => this._Index;
    }
}
