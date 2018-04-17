using ILEditor.Classes;
using ILEditor.Forms;
using ILEditor.UserTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor
{
    public partial class Editor : Form
    {
        public static bool DarkMode = false;
        public static Editor TheEditor;
        public static UserTools.SourceEditor LastEditing;
        public static UserTools.OutlineView OutlineView;
        public static UserTools.TaskList Tasklist;

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
            { "SQLCPP", Language.CPP },
            { "C", Language.CPP },
            { "SQLC", Language.CPP },
            { "SQL", Language.SQL },
            { "CBL", Language.COBOL },
            { "COBOL", Language.COBOL },
            { "CBLLE", Language.COBOL },
            { "CBBLE", Language.COBOL },
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

        public Editor()
        {
            InitializeComponent();
            TheEditor = this;

            MemberCache.Import();

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
                        new Forms.PushWindow().ShowDialog();
                    }
                }
            }

            IBMi.CurrentSystem.SetValue("lastOffline", (IBMi.IsConnected() == false).ToString().ToLower());

            DarkMode = (Program.Config.GetValue("darkmode") == "true");

            if (DarkMode)
                dockingPanel.Theme = new VS2015DarkTheme();
            else
                dockingPanel.Theme = new VS2015BlueTheme();
            
            ApplyControlTheme(toolStrip);
            ApplyControlTheme(menuStrip);
            ApplyControlTheme(statusStrip);

            if (File.Exists(Program.PanelsXML))
                dockingPanel.LoadFromXml(Program.PanelsXML, new DeserializeDockContent(GetContentFromPersistString));
            else
                AddTool(new UserTools.UserToolList(), DockState.DockLeft, true);

            AddTool(new UserTools.Welcome(), DockState.Document, true);

            OutlineView = new UserTools.OutlineView();
            Tasklist = new UserTools.TaskList();

            AddTool(OutlineView, DockState.DockRightAutoHide, true);
            AddTool(Tasklist, DockState.DockBottomAutoHide, true);
            
            dockingPanel.ActiveContentChanged += DockingPanel_ActiveContentChanged;
        }

        public void SetStatus(string Text) => statusText.Text = Text;
        public void SetColumnLabel(string Text) => columnText.Text = Text;
        
        public void AddTool(DockContent Content, DockState dock = DockState.Document, Boolean Replace = false)
        {
            DockPane currentPane;
            DockPanel content = null;

            if (Replace)
            {
                for (int x = 0; x < dockingPanel.Panes.Count; x++)
                {
                    currentPane = dockingPanel.Panes[x];
                    if (Content.Text == currentPane.CaptionText)
                    {
                        content = currentPane.DockPanel;
                        dock = currentPane.DockState;
                        currentPane.Dispose();
                    }
                }
            }

            if (content == null)
                Content.Show(dockingPanel, dock);
            else
                Content.Show(content, dock);
        }

        public static void ApplyControlTheme(Control content)
        {
            if (content is ToolStrip)
                (content as ToolStrip).Renderer = new Classes.ToolStripRenderer();

            if (DarkMode)
            {
                content.BackColor = Color.FromArgb(45, 45, 48);
                content.ForeColor = Color.White;
            }
            else
            {
                content.BackColor = Color.FromArgb(41, 57, 84);
                content.ForeColor = Color.White;
            }

            foreach (Control subcont in content.Controls)
                ApplyControlTheme(subcont);
        }

        public static void OpenSource(RemoteSource Source)
        {
            SourceEditor sourcePanel;
            string resultFile = "";
            string text = Source.GetName() + (Source.GetExtension() != "" ? '.' + Source.GetExtension().ToLower() : "");

            Editor.TheEditor.SetStatus("Fetching file " + text + "...");

            new Thread((ThreadStart)delegate
            {
                switch (Source.GetFS())
                {
                    case FileSystem.QSYS:
                        resultFile = IBMiUtils.DownloadMember(Source.GetLibrary(), Source.GetObject(), Source.GetName(), Source.GetExtension());
                        break;
                    case FileSystem.IFS:
                        resultFile = IBMiUtils.DownloadFile(Source.GetRemoteFile());
                        break;
                }

                if (resultFile != "")
                {
                    TheEditor.SetStatus("Opening file " + text + "...");

                    Source._Local = resultFile;
                    Source.Lock();

                    sourcePanel = new SourceEditor(Source.GetLocalFile(), GetBoundLangType(Source.GetExtension()), Source.GetRecordLength(), !Source.IsEditable());

                    sourcePanel.Tag = Source;
                    sourcePanel.Text = text;

                    TheEditor.Invoke((MethodInvoker)delegate
                    {
                        TheEditor.AddTool(sourcePanel, DockState.Document, false);
                    });
                }
                else
                {
                    switch (Source.GetFS())
                    {
                        case FileSystem.QSYS:
                            MessageBox.Show("Unable to download member " + Source.GetLibrary() + "/" + Source.GetObject() + "." + Source.GetName() + ". Please check it exists and that you have access to the remote system.");
                            break;
                    }
                }
            }).Start();
        }

        public static void OpenExistingSource(RemoteSource Source)
        {
            string text = Source.GetName() + (Source.GetExtension() != "" ? '.' + Source.GetExtension().ToLower() : "");

            if (File.Exists(Source.GetLocalFile()))
            {
                SourceEditor sourcePanel = new SourceEditor(Source.GetLocalFile(), GetBoundLangType(Source.GetExtension()), Source.GetRecordLength(), !Source.IsEditable());

                sourcePanel.Tag = Source;
                sourcePanel.Text = text;

                Source.Lock();
                TheEditor.AddTool(sourcePanel, DockState.Document);
            }
            else
            {
                MessageBox.Show("There was an error opening the local file. '" + Source.GetLocalFile() + "' does not exist");
            }
        }

        public static void OpenLocalSource(string FilePath, Language Language, string Title = null, bool ReadOnly = false)
        {
            string text = Path.GetFileName(FilePath);

            if (File.Exists(FilePath))
            {
                SourceEditor sourcePanel = new SourceEditor(FilePath, Language, 0, ReadOnly);

                if (Title != null)
                    sourcePanel.Text = Title;
                else
                    sourcePanel.Text = text;
                
                TheEditor.AddTool(sourcePanel, DockState.Document);
            }
            else
            {
                MessageBox.Show("There was an error opening the local file. '" + text + "' does not exist");
            }
        }

        public DockContent GetTabByName(string Title, bool Focus = false)
        {
            foreach (DockPane pane in dockingPanel.Panes)
            {
                foreach (DockContent window in pane.Contents)
                {
                    if (window.Text.StartsWith(Title))
                    {
                        if (Focus)
                            window.Activate();

                        return window;
                    }
                }
            }
            return null;
        }

        public SourceEditor GetTabEditor(DockContent Tab)
        {
            if (Tab is SourceEditor)
                return Tab as SourceEditor;

            return null;
        }
        
        private void dockingPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            DockPanel panel = sender as DockPanel;
            RemoteSource src;

            if (panel != null)
            {
                if (panel.ActiveContent is SourceEditor)
                {
                    LastEditing = null;
                    src = (panel.ActiveContent as SourceEditor).Tag as RemoteSource;
                    if (src != null)
                        src.Unlock();
                }
            }
        }
        
        private void DockingPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            LastEditing = GetTabEditor(dockingPanel.ActiveContent as DockContent);

            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.TasksUpdate);
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCache.Export();
            dockingPanel.SaveAsXml(Program.PanelsXML);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(CompileOptions).ToString())
                return new CompileOptions();
            else if (persistString == typeof(IFSBrowser).ToString())
                return new IFSBrowser();
            else if (persistString == typeof(MemberBrowse).ToString())
                return new MemberBrowse();
            else if (persistString == typeof(ObjectBrowse).ToString())
                return new ObjectBrowse();
            else if (persistString == typeof(QSYSBrowser).ToString())
                return new QSYSBrowser();
            else if (persistString == typeof(TaskList).ToString())
                return new TaskList();
            else if (persistString == typeof(UserToolList).ToString())
                return new UserToolList();

            return null;
        }

        public static BitmapImage ConvertBitmap(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        #region File Dropdown

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoteSource member;
            NewMember newMemberForm = new NewMember();

            newMemberForm.ShowDialog();
            if (newMemberForm.created)
            {
                member = new RemoteSource("", newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr, newMemberForm._type, true);
                OpenSource(member);
            }
            newMemberForm.Dispose();
        }

        private void streamFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStreamFile window = new CreateStreamFile();
            window.ShowDialog();

            if (window.result != null)
                OpenExistingSource(window.result);
        }

        private void sourcePhysicalFileToolStripMenuItem_Click(object sender, EventArgs e) => new NewSPF().ShowDialog();

        private void memberToolStripMenuItem1_Click(object sender, EventArgs e) => new OpenSource(0).ShowDialog();

        private void streamFileToolStripMenuItem1_Click(object sender, EventArgs e) => new OpenSource(1).ShowDialog();

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

                    OpenLocalSource(fileSelect.FileName, GetBoundLangType(ext));
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Save);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Save_As);
        }

        private void switchSystemToolStripMenuItem_Click(object sender, EventArgs e) => Application.Restart();

        #endregion

        #region Compile Dropdown
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

        private void compileToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void compileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            compileOptionsToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> Compiles = new List<ToolStripMenuItem>();
            if (LastEditing != null)
            {
                RemoteSource SourceInfo = (RemoteSource)LastEditing.Tag;
                string[] Items = IBMi.CurrentSystem.GetValue("TYPE_" + SourceInfo.GetExtension()).Split('|');
                foreach (string Item in Items)
                {
                    if (Item.Trim() == "") continue;
                    Compiles.Add(new ToolStripMenuItem(Item, null, compileAnyHandle));
                }
            }

            compileToolStripMenuItem1.Enabled = (Compiles.Count > 0);
            compileOptionsToolStripMenuItem.Enabled = (Compiles.Count > 0);
            compileOptionsToolStripMenuItem.DropDownItems.AddRange(Compiles.ToArray());
        }

        #endregion

        #region Tools dropdown
        private void openToolboxToolStripMenuItem_Click(object sender, EventArgs e) => AddTool(new UserTools.UserToolList(), DockState.DockLeft);

        private void openWelcomeToolStripMenuItem_Click(object sender, EventArgs e) => AddTool(new UserTools.Welcome());

        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e) => new Connection().ShowDialog();

        private void libraryListToolStripMenuItem_Click(object sender, EventArgs e) => new Forms.LibraryList().ShowDialog();

        private void start5250SessionToolStripMenuItem_Click(object sender, EventArgs e)
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
        
        private void startRemoteDebugACSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Program.Config.GetValue("acspath");
            if (path == "false")
                MessageBox.Show("Please setup the ACS path in the Connection Settings.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                try
                {
                    Process.Start(path, " /plugin=sysdbg /system=" + IBMi.CurrentSystem.GetValue("system"));
                }
                catch
                {
                    MessageBox.Show("Something went wrong launching the debug session.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void quickMemberSearchToolStripMenuItem_Click(object sender, EventArgs e) => new QuickMemberSearch().Show();

        private void sourceDiffToolStripMenuItem_Click(object sender, EventArgs e) => new SourceCompareSelect().ShowDialog();
        #endregion

        #region Source dropdown
        private void sPFCloneToolStripMenuItem_Click(object sender, EventArgs e) => new CloneWindow().ShowDialog();

        private void sPFPushToolStripMenuItem_Click(object sender, EventArgs e) => new PushWindow().ShowDialog();

        private void memberSearchToolStripMenuItem_Click(object sender, EventArgs e) => new MemberSearch().ShowDialog();

        private void rPGConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
            {
                RemoteSource SourceInfo = (RemoteSource)LastEditing.Tag;
                Language Language = GetBoundLangType(SourceInfo.GetExtension());
                if (Language == Language.RPG)
                {
                    SetStatus("Converting RPG in " + SourceInfo.GetName());
                    LastEditing.DoAction(EditorAction.Convert_Selected_RPG);
                }
            }
        }

        private void cLFormattingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
            {
                RemoteSource SourceInfo = (RemoteSource)LastEditing.Tag;
                Language Language = GetBoundLangType(SourceInfo.GetExtension());
                if (Language == Language.CL)
                {
                    SetStatus("Formatting CL in " + SourceInfo.GetName());
                    LastEditing.DoAction(EditorAction.Format_CL);
                }
            }
        }

        private void generateSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSelect SelectFile = new FileSelect();
            SelectFile.ShowDialog();

            if (SelectFile.Success)
            {
                IBMiUtils.UsingQTEMPFiles(new[] { "Q_GENSQL" });
                if (IBMi.RemoteCommand(SelectFile.getCommand()))
                {
                    OpenSource(new RemoteSource("", "QTEMP", "Q_GENSQL", "Q_GENSQL", "SQL", false));
                }
                else
                {
                    MessageBox.Show("Error generating SQL source.");
                }
            }
        }

        private void quickCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Comment_Out_Selected);
        }

        private void duplicateLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Dupe_Line);
        }

        private void contentAssistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.ShowContentAssist);
        }
        #endregion

        #region Help dropdown
        private void aboutILEditorToolStripMenuItem_Click(object sender, EventArgs e) => new About().ShowDialog();
        private void sessionFTPLogToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(IBMi.FTPFile);
        #endregion

        #region ToolStrip
        private void newMember_Click(object sender, EventArgs e) => memberToolStripMenuItem.PerformClick();
        private void saveSource_Click(object sender, EventArgs e) => saveToolStripMenuItem.PerformClick();
        private void liblButton_Click(object sender, EventArgs e) => libraryListToolStripMenuItem.PerformClick();
        private void compileButton_Click(object sender, EventArgs e) => compileToolStripMenuItem1.PerformClick();
        private void acsButton_Click(object sender, EventArgs e) => start5250SessionToolStripMenuItem.PerformClick();
        private void dbgButton_Click(object sender, EventArgs e) => startRemoteDebugACSToolStripMenuItem.PerformClick();

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Zoom_Out);
        }
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Zoom_In);
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Undo);
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.DoAction(EditorAction.Redo);
        }

        private void commentButton_Click(object sender, EventArgs e) => quickCommentToolStripMenuItem.PerformClick();
        #endregion
    }
}
