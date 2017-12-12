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

            SetUpPanels();
            
            this.Text += " (" + IBMi.CurrentSystem.GetValue("alias") + ")";
            MemberCache.Import();
        }

        private void SetUpPanels()
        {
            editortabs = new SplitContainer();
            this.editortabsleft = new TabControlExtra();
            this.editortabsright = new TabControlExtra();
            this.usercontrol = new TabControlExtra();
            editortabsleft.ImageList = tabImageList;

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
            this.editortabsleft.MouseClick += new MouseEventHandler(this.editortabs_MouseClick);
            this.editortabsleft.ControlAdded += Editortabsleft_ControlAdded;
            this.editortabsleft.TabClosed += Editortabsleft_TabClosed;

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
            this.editortabsright.MouseClick += new MouseEventHandler(this.editortabs_MouseClick);
            this.editortabsright.ControlAdded += Editortabsleft_ControlAdded;

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

        private void Editortabsleft_TabClosed(object sender, TabControlEventArgs e)
        {
            FixEditorSplitters();
        }

        private void Editortabsleft_ControlAdded(object sender, ControlEventArgs e)
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
            if (this.LastEditing.Tag != null)
            {
                Member MemberInfo = (Member)this.LastEditing.Tag;
                ILELanguage Language = GetBoundLangType(MemberInfo.GetExtension());
                if (Language == ILELanguage.RPG)
                {
                    SetStatus("Converting RPG in " + MemberInfo.GetMember());
                    LastEditing.ConvertSelectedRPG();
                }
            }
        }
        
        private void cLFormatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LastEditing.Tag != null)
            {
                Member MemberInfo = (Member)this.LastEditing.Tag;
                ILELanguage Language = Editor.GetBoundLangType(MemberInfo.GetExtension());
                if (Language == ILELanguage.CL)
                {
                    SetStatus("Formatting CL in " + MemberInfo.GetMember());
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
                Member MemberInfo = (Member)this.LastEditing.Tag;
                lib = MemberInfo.GetLibrary();
                spf = MemberInfo.GetObject();
                mbr = MemberInfo.GetMember();
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
            if (editortabsleft.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabsleft.SelectedTab.Tag;
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileMember(MemberInfo);
                }).Start();
            }
        }

        private void compileAnyHandle(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (editortabsleft.SelectedTab.Tag != null)
            {
                Member MemberInfo = (Member)editortabsleft.SelectedTab.Tag;
                new Thread((ThreadStart)delegate
                {
                    IBMiUtils.CompileMember(MemberInfo, clickedItem.Text);
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
                Member MemberInfo = (Member)editortabsleft.SelectedTab.Tag;
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

        public static void OpenMember(Member member)
        {
            string TabText = member.GetLibrary() + "/" + member.GetObject() + "(" + member.GetMember() + ")";
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

        private void AddMemberEditor(Member MemberInfo, ILELanguage Language = ILELanguage.None)
        {
            string pageName = MemberInfo.GetLibrary() + "/" + MemberInfo.GetObject() + "(" + MemberInfo.GetMember() + ")";
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
            tabPage.ImageIndex = 0;
            tabPage.ToolTipText = MemberInfo.GetLibrary() + "/" + MemberInfo.GetObject() + "(" + MemberInfo.GetMember() + ")";
            SourceEditor srcEdit = new SourceEditor(MemberInfo.GetLocalFile(), Language, MemberInfo.GetRecordLength());
            srcEdit.SetReadOnly(!MemberInfo.IsEditable());
            srcEdit.BringToFront();
            srcEdit.Dock = DockStyle.Fill;
            srcEdit.Tag = MemberInfo;

            tabPage.Tag = MemberInfo;
            tabPage.Controls.Add(srcEdit);
            editortabsleft.TabPages.Add(tabPage);

            SwitchToTab(OpenTab.TAB_SIDE.Left, editortabsleft.TabPages.Count - 1);
        }
        
        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMember newMemberForm = new NewMember();
            newMemberForm.ShowDialog();
            if (newMemberForm.created)
            {
                new Thread((ThreadStart)delegate {

                    string resultFile = IBMiUtils.DownloadMember(newMemberForm._lib, newMemberForm._spf, newMemberForm._mbr, null, (newMemberForm._type == "*NONE" ? "" : newMemberForm._type));

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
            switch (fromTabs.Name)
            {
                case "editortabsleft":
                    side = OpenTab.TAB_SIDE.Left;
                    break;
                case "editortabsright":
                    side = OpenTab.TAB_SIDE.Right;
                    break;
            }

            if (e.Button == MouseButtons.Right)
            {
                for (int ix = 0; ix < editortabsleft.TabCount; ++ix)
                {
                    if (editortabsleft.GetTabRect(ix).Contains(e.Location))
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
            if (editortabsleft.SelectedTab.Tag != null)
            {
                SourceEditor sourceCode = (SourceEditor)editortabsleft.SelectedTab.Controls[0];
                sourceCode.Zoom(+1f);
            }
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (editortabsleft.SelectedTab.Tag != null)
            {
                SourceEditor sourceCode = (SourceEditor)editortabsleft.SelectedTab.Controls[0];
                sourceCode.Zoom(-1f);
            }
        }
        #endregion
        
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
