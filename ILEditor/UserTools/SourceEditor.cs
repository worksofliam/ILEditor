using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.IO;
using ILEditor.Classes;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ILEditor.Classes.LanguageTools;
using System.Threading;
using ICSharpCode.AvalonEdit;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Search;
using FindReplace;

namespace ILEditor.UserTools
{
    public enum ILELanguage
    {
        None,
        CL,
        CPP,
        RPG,
        SQL,
        COBOL
    }

    public partial class SourceEditor : UserControl
    {
        private TextEditor textEditor = null;
        private ILELanguage Language;
        private int RcdLen;

        public SourceEditor(String LocalFile, ILELanguage Language = ILELanguage.None, int RecordLength = 0)
        {
            InitializeComponent();

            //https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting

            this.Language = Language;
            this.RcdLen = RecordLength;

            textEditor = new TextEditor();
            textEditor.ShowLineNumbers = true;
            textEditor.Text = File.ReadAllText(LocalFile);

            textEditor.FontFamily = new System.Windows.Media.FontFamily(IBMi.CurrentSystem.GetValue("FONT"));
            textEditor.FontSize = float.Parse(IBMi.CurrentSystem.GetValue("ZOOM"));

            textEditor.TextChanged += TextEditor_TextChanged;
            textEditor.TextArea.Caret.PositionChanged += TextEditorTextAreaCaret_PositionChanged;

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

            //SearchPanel.Install(textEditor);
            SearchReplacePanel.Install(textEditor);

            string lang = "";
            bool DarkMode = (Program.Config.GetValue("darkmode") == "true");

            if (DarkMode)
                lang += "dark";
            else
                lang += "light";

            switch (Language)
            {
                case ILELanguage.RPG:
                    lang += "RPG.xml";
                    break;
                case ILELanguage.SQL:
                    lang += "SQL.xml";
                    break;
                case ILELanguage.CPP:
                    lang += "CPP.xml";
                    break;
                case ILELanguage.CL:
                    lang += "CL.xml";
                    break;
                case ILELanguage.COBOL:
                    lang += "COBOL.xml";
                    break;
            }

            if (DarkMode)
            {
                textEditor.Background = System.Windows.Media.Brushes.Black;
                textEditor.Foreground = System.Windows.Media.Brushes.White;
            }

            if (File.Exists(Program.SYNTAXDIR + lang))
            {
                Stream xshd_stream = File.OpenRead(Program.SYNTAXDIR + lang);
                XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
                // Apply the new syntax highlighting definition.
                textEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                xshd_reader.Close();
                xshd_stream.Close();
            }

            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;
            host.Child = textEditor;
            this.Controls.Add(host);
        }
        
        public void SetReadOnly(bool ReadOnly)
        {
            textEditor.IsReadOnly = ReadOnly;
        }

        public string GetText()
        {
            return textEditor.Text;
        }

        public void GotoLine(int line, int col)
        {
            line++; col++;
            int pos = textEditor.Document.GetOffset(line, col);
            textEditor.ScrollToLine(line);
            textEditor.CaretOffset = pos;
            textEditor.Focus();
        }

        public void Zoom(float change)
        {
            if (textEditor.FontSize + change > 5 && textEditor.FontSize + change < 100)
            {
                textEditor.FontSize += change;
                IBMi.CurrentSystem.SetValue("ZOOM", textEditor.FontSize.ToString());
            }
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            if (!this.Parent.Text.EndsWith("*"))
            {
                this.Parent.Text += "*";
            }


        }
        
        private void TextEditorTextAreaCaret_PositionChanged(object sender, EventArgs e)
        {
            DocumentLine line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
            int col = textEditor.CaretOffset - line.Offset;
            Editor.TheEditor.SetStatus($"Ln: {line.LineNumber}    Col: {col}");
        }


        #region RPG

        public void ConvertSelectedRPG()
        {
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
                        lines[i] = freeForm;
                    }
                }

                textEditor.SelectedText = String.Join(Environment.NewLine, lines);
            }

        }
        #endregion

        #region CL

        public void FormatCL()
        {
            string[] Lines = textEditor.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            textEditor.Clear();
            int length = (RcdLen > 0 ? RcdLen : 80);
            textEditor.AppendText(String.Join(Environment.NewLine, CLFile.CorrectLines(Lines, length)));
        }
        #endregion
    }
}
