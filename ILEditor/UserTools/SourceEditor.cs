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
using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Windows.Input;


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
		string word_pattern = "%{0,1}?(\\w|-|@)+";
		CompletionWindow completionWindow;
		HashSet<String> autocompleteSet = new HashSet<String>();

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
			string autocomplete_word_list = "";
            switch (Language)
            {
                case ILELanguage.RPG:
                    lang = "RPG.xml";
					autocomplete_word_list = "RPG.TXT";
                    break;
                case ILELanguage.SQL:
                    lang = "SQL.xml";
                    break;
                case ILELanguage.CPP:
                    lang = "CPP.xml";
					autocomplete_word_list = "CPP.TXT";
					break;
                case ILELanguage.CL:
                    lang = "CL.xml";
					autocomplete_word_list = "CL.TXT";
					break;
                case ILELanguage.COBOL:
                    lang = "COBOL.xml";
                    break;
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

			AutocompleteSetInitialLoad(autocomplete_word_list, textEditor.Text);
        }

		void AutocompleteSetInitialLoad(String autocomplete_word_list, string editor_text )
		{
			//From Resources files
			String[] lines = File.ReadAllLines(Program.SYNTAXDIR + autocomplete_word_list);
			foreach (String line in lines)
			{
				String[] parts = line.Split('|');
				autocompleteSet.Add(parts[0]);
			}
			//From Editor Text
			foreach (Match match in Regex.Matches(editor_text, this.word_pattern, RegexOptions.IgnoreCase))
			{
				string cur_word_upper = match.Value.ToUpper();
				if (autocompleteSet.Contains(cur_word_upper))
				{
					autocompleteSet.Remove(cur_word_upper);
				}
				autocompleteSet.Add(match.Value);
			}

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


			DocumentLine line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
			int col = textEditor.CaretOffset - line.Offset;
			Editor.TheEditor.SetStatus(line.LineNumber.ToString() + ", " + col.ToString());


			int begin = TextUtilities.GetNextCaretPosition(textEditor.Document, textEditor.CaretOffset, System.Windows.Documents.LogicalDirection.Backward, CaretPositioningMode.WordStart);
			int end = textEditor.CaretOffset;
			string cur_word = textEditor.Document.GetText(begin, (end - begin));
		
			string cur_word_upper = cur_word.ToUpper();
			//To account for spaces, newlines et al...
			if (cur_word.Length > 0)
			{
				//If the word was previously loades in uppercase delete an use our version
				if (autocompleteSet.Contains(cur_word_upper))
				{
					autocompleteSet.Remove(cur_word_upper);
				}
				//Add new words to our set
				autocompleteSet.Add(cur_word);
			}

			completionWindow = new CompletionWindow(textEditor.TextArea);
			IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;


			string[] result = new string[autocompleteSet.Count];
			autocompleteSet.CopyTo(result);

			data.Clear();
			Array.Sort(result);

			foreach (var word in result)
			{
				String trimmed_word = word.Trim();
				if ((cur_word.Length > 0) &&
					(trimmed_word.StartsWith(cur_word, StringComparison.OrdinalIgnoreCase) &&
					(!trimmed_word.Equals(cur_word, StringComparison.OrdinalIgnoreCase))))
				{
					data.Add(new AutoCompleteData(trimmed_word, trimmed_word));
				}
			}
			if (data.Any())
			{
				completionWindow.Show();

				completionWindow.Closed += delegate
				{
					completionWindow = null;
				};
			}
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
