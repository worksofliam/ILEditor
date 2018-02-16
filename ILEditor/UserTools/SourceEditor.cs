using System;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.IO;
using ILEditor.Classes;
using ILEditor.Classes.LanguageTools;
using System.Threading;
using ICSharpCode.AvalonEdit;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using FindReplace;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Media;
using ILEditor.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.AvalonEdit.Search;

namespace ILEditor.UserTools
{

    public enum EditorAction
    {
        Zoom_In,
        Zoom_Out,
        Save,
        Save_As,
        Comment_Out_Selected,
        Convert_Selected_RPG,
        Format_CL,
        Undo,
        Redo,
        Dupe_Line,
        OutlineUpdate,
        ParseCode
    }

    public enum Language
    {
        None,
        CL,
        CPP,
        RPG,
        SQL,
        COBOL,
        Python
    }
    
    public partial class SourceEditor : DockContent
    {
        private static bool CurrentSaving = false;

        private TextEditor textEditor = null;
        private Language Language;
        private int RcdLen;
        private string LocalPath;
        private bool ReadOnly;
        private Function[] Functions;
        
        public SourceEditor(String LocalFile, Language Language = Language.None, int RecordLength = 0, bool isReadOnly = false)
        {
            InitializeComponent();
            
            //https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting

            this.Language = Language;
            this.RcdLen = RecordLength;
            this.LocalPath = LocalFile;
            this.ReadOnly = isReadOnly;
        }

        public void CreateForm()
        {
            textEditor = new TextEditor();
            textEditor.ShowLineNumbers = true;
            //textEditor.Encoding = Encoding.GetEncoding(1252);
            //textEditor.Encoding = Encoding.GetEncoding(28591);
            //textEditor.Encoding = Encoding.GetEncoding("IBM437");
            textEditor.Encoding = Encoding.GetEncoding("iso-8859-1");
            textEditor.Text = File.ReadAllText(this.LocalPath, textEditor.Encoding);

            textEditor.FontFamily = new System.Windows.Media.FontFamily(IBMi.CurrentSystem.GetValue("FONT"));
            textEditor.FontSize = float.Parse(IBMi.CurrentSystem.GetValue("ZOOM"));

            textEditor.TextChanged += TextEditor_TextChanged;
            textEditor.TextArea.Caret.PositionChanged += TextEditorTextAreaCaret_PositionChanged;
            textEditor.GotFocus += TextEditor_GotFocus;

            textEditor.Options.ConvertTabsToSpaces = true;
            textEditor.Options.EnableTextDragDrop = false;
            textEditor.Options.IndentationSize = int.Parse(IBMi.CurrentSystem.GetValue("INDENT_SIZE"));
            textEditor.Options.ShowSpaces = (IBMi.CurrentSystem.GetValue("SHOW_SPACES") == "true");
            textEditor.Options.HighlightCurrentLine = (IBMi.CurrentSystem.GetValue("HIGHLIGHT_CURRENT_LINE") == "true");

            textEditor.Options.AllowScrollBelowDocument = true;

            if (this.RcdLen > 0)
            {
                textEditor.Options.ShowColumnRuler = true;
                textEditor.Options.ColumnRulerPosition = this.RcdLen;
            }

            textEditor.IsReadOnly = this.ReadOnly;

            if (this.ReadOnly)
                SearchPanel.Install(textEditor);
            else
                SearchReplacePanel.Install(textEditor);

            string lang = "";
            bool DarkMode = (Program.Config.GetValue("darkmode") == "true");

            if (DarkMode)
                lang += "dark";
            else
                lang += "light";

            switch (Language)
            {
                case Language.SQL:
                case Language.RPG:
                case Language.CPP:
                case Language.CL:
                case Language.COBOL:
                case Language.Python:
                    lang += Language.ToString();
                    break;
                case Language.None:
                    lang = "";
                    break;
            }

            if (DarkMode)
            {
                textEditor.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1E1E1E");
                textEditor.Foreground = System.Windows.Media.Brushes.White;
            }

            if (lang != "")
                using (Stream s = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.ResourceManager.GetString(lang))))
                using (XmlTextReader reader = new XmlTextReader(s))
                    textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;
            host.Child = textEditor;
            this.Controls.Add(host);

            DoAction(EditorAction.ParseCode);
            DoAction(EditorAction.OutlineUpdate);
        }

        private void SourceEditor_Load(object sender, EventArgs e)
        {
            CreateForm();
        }

        public string GetText()
        {
            return textEditor.Text;
        }

        public void GotoLine(int line, int col)
        {
            line++; col++;
            if (line > 0)
            {
                int pos = textEditor.Document.GetOffset(line, col);
                textEditor.ScrollToLine(line);
                textEditor.CaretOffset = pos;
                textEditor.Focus();
            }
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }
        
        private void TextEditorTextAreaCaret_PositionChanged(object sender, EventArgs e)
        {
            DocumentLine line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
            int col = textEditor.CaretOffset - line.Offset;
            Editor.TheEditor.SetColumnLabel($"Ln: {line.LineNumber} Col: {col}");
        }

        private void TextEditor_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor.LastEditing = this;
        }

        public void DoAction(EditorAction Action)
        {
            switch (Action)
            {
                case EditorAction.Comment_Out_Selected:
                    CommentOutSelected();
                    break;
                case EditorAction.Convert_Selected_RPG:
                    ConvertSelectedRPG();
                    break;
                case EditorAction.Format_CL:
                    FormatCL();
                    break;
                case EditorAction.Save:
                    Save();
                    break;
                case EditorAction.Save_As:
                    SaveAs();
                    break;
                case EditorAction.Zoom_In:
                    Zoom(+1f);
                    break;
                case EditorAction.Zoom_Out:
                    Zoom(-1f);
                    break;
                case EditorAction.Undo:
                    textEditor.Undo();
                    break;
                case EditorAction.Redo:
                    textEditor.Redo();
                    break;
                case EditorAction.Dupe_Line:
                    DuplicateLine();
                    break;
                case EditorAction.ParseCode:
                    Parse();
                    break;
                case EditorAction.OutlineUpdate:
                    OutlineUpdate();
                    break;
            }
        }

        private void Parse()
        {
            if (IBMi.CurrentSystem.GetValue("OUTLINE_VIEW_ENABLED") == "true")
            {
                switch (this.Language)
                {
                    case Language.RPG:
                        string code = "";
                        this.Invoke((MethodInvoker)delegate
                        {
                            code = GetText().ToUpper();
                        });
                        this.Functions = RPGParser.Parse(code);
                        break;
                }
            }
        }

        private void OutlineUpdate()
        {
            this.Invoke((MethodInvoker)delegate
            {
                Editor.OutlineView.Display(this.Text, this.Functions);
            });
        }

        private void DuplicateLine()
        {
            DocumentLine line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
            textEditor.Select(line.Offset, line.Length);
            string value = textEditor.SelectedText;
            value += Environment.NewLine + value;
            textEditor.SelectedText = value;
            textEditor.SelectionLength = 0;
        }

        private void Zoom(float change)
        {
            if (textEditor.FontSize + change > 5 && textEditor.FontSize + change < 100)
            {
                textEditor.FontSize += change;
                IBMi.CurrentSystem.SetValue("ZOOM", textEditor.FontSize.ToString());
            }
        }

        private void SaveAs()
        {
            if (!this.Text.EndsWith("*"))
            {
                if (!CurrentSaving)
                {
                    RemoteSource MemberInfo = this.Tag as RemoteSource;
                    if (MemberInfo != null)
                    {
                        switch (MemberInfo.GetFS())
                        {
                            case FileSystem.QSYS:
                                SaveAs SaveAsWindow = new SaveAs();
                                SaveAsWindow.ShowDialog();
                                if (SaveAsWindow.Success)
                                {
                                    CurrentSaving = true;

                                    Editor.TheEditor.SetStatus("Saving " + SaveAsWindow.Mbr + "..");
                                    Thread gothread = new Thread((ThreadStart)delegate
                                    {
                                        bool UploadResult = IBMiUtils.UploadMember(MemberInfo.GetLocalFile(), SaveAsWindow.Lib, SaveAsWindow.Spf, SaveAsWindow.Mbr);
                                        if (UploadResult == false)
                                            MessageBox.Show("Failed to upload to " + SaveAsWindow.Mbr + ".");

                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            Editor.TheEditor.SetStatus(SaveAsWindow.Mbr + " " + (UploadResult ? "" : "not ") + "saved.");
                                        });
                                        
                                        CurrentSaving = false;
                                    });

                                    gothread.Start();
                                }
                                else
                                {
                                    MessageBox.Show("Save as is not available for local source.");
                                }
                                break;

                            case FileSystem.IFS:
                                MessageBox.Show("Save as is not available for stream files yet.");
                                break;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("You must save the source before you can Save-As.");
            }
        }

        private void Save()
        {
            RemoteSource SourceInfo = (RemoteSource)this.Tag;
            bool UploadResult = true;

            if (SourceInfo != null)
            {
                if (SourceInfo.IsEditable())
                {
                    if (!CurrentSaving)
                    {
                        CurrentSaving = true;

                        Editor.TheEditor.SetStatus("Saving " + SourceInfo.GetName() + "..");
                        Thread gothread = new Thread((ThreadStart)delegate
                        {
                            MemberCache.EditsAdd(SourceInfo.GetLibrary(), SourceInfo.GetObject(), SourceInfo.GetName());
                            this.Invoke((MethodInvoker)delegate
                            {
                                File.WriteAllText(SourceInfo.GetLocalFile(), this.GetText(), textEditor.Encoding);
                            });

                            switch (SourceInfo.GetFS())
                            {
                                case FileSystem.QSYS:
                                    UploadResult = IBMiUtils.UploadMember(SourceInfo.GetLocalFile(), SourceInfo.GetLibrary(), SourceInfo.GetObject(), SourceInfo.GetName());
                                    break;
                                case FileSystem.IFS:
                                    UploadResult = IBMiUtils.UploadFile(SourceInfo.GetLocalFile(), SourceInfo.GetRemoteFile());
                                    break;
                            }
                            if (UploadResult == false)
                            {
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (this.Text.EndsWith("*"))
                                        this.Text = this.Text.Substring(0, this.Text.Length - 1);
                                });
                                DoAction(EditorAction.ParseCode);
                                DoAction(EditorAction.OutlineUpdate);
                            }

                            this.Invoke((MethodInvoker)delegate
                            {
                                Editor.TheEditor.SetStatus(SourceInfo.GetName() + " " + (UploadResult ? "" : "not ") + "saved.");
                            });

                            CurrentSaving = false;
                        });

                        gothread.Start();
                    }
                    else
                    {
                        Editor.TheEditor.SetStatus("Please wait until previous save has finished.");
                    }

                }
                else
                {
                    MessageBox.Show("This file is readonly.");
                }
            }
            else
            {
                File.WriteAllText(this.LocalPath, this.GetText(), textEditor.Encoding);
                if (this.Text.EndsWith("*"))
                    this.Text = this.Text.Substring(0, this.Text.Length - 1);
                Editor.TheEditor.SetStatus("File saved locally.");
            }
        }

        private void CommentOutSelected()
        {
            if (this.ReadOnly) return;

            if (textEditor.SelectionLength == 0)
            {
                DocumentLine line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
                textEditor.Select(line.Offset, line.Length);
            }

            string[] lines = textEditor.SelectedText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            int index = 0;
            switch (Language)
            {
                case Language.RPG:
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Trim() != "")
                        {
                            index = GetSpaces(lines[i]);
                            lines[i] = lines[i].Insert(index, "//");
                        }
                    }
                    textEditor.SelectedText = String.Join(Environment.NewLine, lines);
                    break;

                case Language.CL:
                case Language.CPP:
                    if (lines.Length == 1 && Language != Language.CL)
                    {
                        index = GetSpaces(lines[0]);
                        lines[0] = lines[0].Insert(index, "//");
                    }
                    else
                    {
                        lines[0] = "/*" + lines[0];
                        lines[lines.Length - 1] = lines[lines.Length - 1] + "*/";
                    }
                    textEditor.SelectedText = String.Join(Environment.NewLine, lines);
                    break;

                case Language.SQL:
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Trim() != "")
                        {
                            index = GetSpaces(lines[i]);
                            lines[i] = lines[i].Insert(index, "--");
                        }
                    }
                    textEditor.SelectedText = String.Join(Environment.NewLine, lines);
                    break;
            }
        }

        private static int GetSpaces(string line)
        {
            int index = 0;
            foreach(char c in line.ToCharArray())
            {
                if (c == ' ') index++;
                else break;
            }
            return index;
        }

        #region RPG

        private void ConvertSelectedRPG()
        {
            if (this.ReadOnly) return;

            if (textEditor.SelectedText == "")
            {
                MessageBox.Show("Please highlight the code you want to convert and then try the conversion again.", "Fixed-To-Free", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string[] lines = textEditor.SelectedText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string freeForm = "";

                for (int i = 0; i < lines.Length; i++)
                {
                    freeForm = RPGFree.getFree(lines[i]);
                    if (freeForm != "")
                    {
                        switch (freeForm.Trim())
                        {
                            case "*SAME;":
                                //Do nothing!
                                break;
                            case "*BLANK;":
                                lines[i] = "";
                                break;
                            default:
                                lines[i] = freeForm;
                                break;
                        }
                    }
                }

                textEditor.SelectedText = String.Join(Environment.NewLine, lines);
            }

        }

        #endregion

        #region CL

        private void FormatCL()
        {
            if (this.ReadOnly) return;

            string[] Lines = textEditor.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            textEditor.SelectAll();
            textEditor.SelectedText = "";
            int length = (RcdLen > 0 ? RcdLen : 80);
            textEditor.AppendText(String.Join(Environment.NewLine, CLFile.CorrectLines(Lines, length)));
        }
        #endregion

    }
}
