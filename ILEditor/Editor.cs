using ILEditor.Classes;
using ILEditor.UserTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor
{
    public partial class Editor : Form
    {
        public static Editor TheEditor;
        public static UserTools.SourceEditor LastEditing;

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

            if (Program.Config.GetValue("darkmode") == "true")
                dockingPanel.Theme = new VS2015DarkTheme();
            else
                dockingPanel.Theme = new VS2015LightTheme();

            AddTool(new UserTools.Welcome());
            AddTool(new UserTools.UserToolList(), DockState.DockLeft);
        }

        public void SetStatus(string Text) => statusText.Text = Text;
        public void SetColumnLabel(string Text) => columnText.Text = Text;
        
        public void AddTool(DockContent Content, DockState dock = DockState.Document, Boolean Replace = false)
        {
            //TODO: Replace
            Content.Show(dockingPanel, dock);
        }

        public static void OpenSource(RemoteSource Source)
        {
            SourceEditor sourcePanel;
            string resultFile = "";
            string text = "";

            text = Path.GetFileName(Source.GetName() + "." + Source.GetExtension().ToLower());
            Editor.TheEditor.SetStatus("Fetching file " + text + "...");

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
                Editor.TheEditor.SetStatus("Opening file " + text + "...");
                Source._Local = resultFile;
                sourcePanel = new SourceEditor(Source.GetLocalFile(), GetBoundLangType(Source.GetExtension()), Source.GetRecordLength());

                sourcePanel.Tag = Source;
                sourcePanel.Text = text;

                Source.Lock();
                TheEditor.AddTool(sourcePanel, DockState.Document, true);
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
                
        }

        public Boolean EditorContains(string Title)
        {
            //TODO: Editor Contains where Page title is Title
            return false;
        }

        public void SwitchToTab(string Title)
        {
            //TODO: SWITCH TO TAB
        }

        public UserTools.SourceEditor GetTabEditor(string Title)
        {
            //TODO: Return sourcetab
            return null;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastEditing != null)
                LastEditing.Save();
        }
    }
}
