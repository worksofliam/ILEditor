using ILEditor.Classes;
using ILEditor.UserTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            
            AddTool(new UserTools.Welcome());
            AddTool(new UserTools.UserToolList(), DockState.DockLeft);
        }

        public void SetStatus(string Text)
        {
            //TODO: Change text on editor window 
        }
        public void SetColumnLabel(string Text)
        {
            //TODO: Change text for column
        }

        public void AddTool(DockContent Content, DockState dock = DockState.Document, Boolean Replace = false)
        {
            //TODO: Replace
            Content.Show(dockPanel1, dock);
        }

        public void OpenSource(RemoteSource Source)
        {
            string resultFile = "";
            string text = "";
            Thread gothread = new Thread((ThreadStart)delegate {
                switch (Source.GetFS())
                {
                    case FileSystem.QSYS:
                        resultFile = IBMiUtils.DownloadMember(Source.GetLibrary(), Source.GetObject(), Source.GetName(), Source.GetExtension());
                        text = Source.GetLibrary() + "/" + Source.GetObject() + "(" + Source.GetName() + ")"; ;
                        break;
                    case FileSystem.IFS:
                        resultFile = IBMiUtils.DownloadFile(Source.GetRemoteFile());
                        text = Path.GetFileName(Source.GetName() + "." + Source.GetExtension());
                        break;
                }

                if (resultFile != "")
                {
                    Source._Local = resultFile;
                    SourceEditor sourcePanel = new SourceEditor(Source.GetLocalFile(), GetBoundLangType(Source.GetExtension()), Source.GetRecordLength());

                    sourcePanel.Tag = Source;
                    sourcePanel.Text = text;

                    Source.Lock();
                    dockPanel1.Invoke((MethodInvoker)delegate
                    {
                        TheEditor.AddTool(sourcePanel, DockState.Document, true);
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

            });
            gothread.SetApartmentState(ApartmentState.STA);
            gothread.Start();
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
    }
}
