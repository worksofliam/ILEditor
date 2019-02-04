using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Search;
using ILEditor.Classes;
using ILEditor.Classes.LanguageTools;
using ILEditor.Forms;
using ILEditor.Properties;
using WeifenLuo.WinFormsUI.Docking;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ILEditor.UserTools
{
	public enum EditorAction
	{
		Zoom_In,
		Zoom_Out,
		Save,
		Save_As,
		Comment_Out_Selected,
		Format_CL,
		Undo,
		Redo,
		Dupe_Line,
		ParseCode,
		TasksUpdate
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
		private static   bool     CurrentSaving;
		private readonly Language Language;
		private readonly string   LocalPath;
		private readonly int      RcdLen;
		private readonly bool     ReadOnly;

		private TaskItem[] Tasks;

		private TextEditor textEditor;

		public SourceEditor(string   LocalFile,
		                    Language Language     = Language.None,
		                    int      RecordLength = 0,
		                    bool     isReadOnly   = false)
		{
			InitializeComponent();

			this.Language = Language;
			RcdLen        = RecordLength;
			LocalPath     = LocalFile;
			ReadOnly      = isReadOnly;
		}

		public void CreateForm()
		{
			textEditor                 = new TextEditor();
			textEditor.ShowLineNumbers = true;

			//textEditor.Encoding = Encoding.GetEncoding(1252);
			//textEditor.Encoding = Encoding.GetEncoding(28591);
			//textEditor.Encoding = Encoding.GetEncoding("IBM437");
			textEditor.Encoding = Program.Encoding;
			textEditor.Text     = File.ReadAllText(LocalPath, textEditor.Encoding);

			textEditor.FontFamily = new FontFamily(IBMi.CurrentSystem.GetValue("FONT"));
			textEditor.FontSize   = float.Parse(IBMi.CurrentSystem.GetValue("ZOOM"));

			textEditor.TextChanged                    += TextEditor_TextChanged;
			textEditor.TextArea.Caret.PositionChanged += TextEditorTextAreaCaret_PositionChanged;
			textEditor.GotFocus                       += TextEditor_GotFocus;

			if (IBMi.CurrentSystem.GetValue("CHARACTER_ASSIST") == "true")
				textEditor.TextArea.TextEntered += TextArea_TextEntered;

			textEditor.Options.ConvertTabsToSpaces  = IBMi.CurrentSystem.GetValue("CONV_TABS") == "true";
			textEditor.Options.EnableTextDragDrop   = false;
			textEditor.Options.IndentationSize      = int.Parse(IBMi.CurrentSystem.GetValue("INDENT_SIZE"));
			textEditor.Options.ShowSpaces           = IBMi.CurrentSystem.GetValue("SHOW_SPACES") == "true";
			textEditor.Options.HighlightCurrentLine = IBMi.CurrentSystem.GetValue("HIGHLIGHT_CURRENT_LINE") == "true";

			if (LocalPath.EndsWith("makefile"))
				textEditor.Options.ConvertTabsToSpaces = false;

			textEditor.Options.AllowScrollBelowDocument = true;

			if (RcdLen > 0)
			{
				textEditor.Options.ShowColumnRuler     = true;
				textEditor.Options.ColumnRulerPosition = RcdLen;
			}

			textEditor.IsReadOnly = ReadOnly;

			if (ReadOnly)
				SearchPanel.Install(textEditor);
			else
				SearchReplacePanel.Install(textEditor);

			var lang = "";

			if (Editor.DarkMode)
				lang += "dark";
			else
				lang += "light";

			if (LocalPath.EndsWith("makefile"))
				lang += "makefile";
			else
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

			if (Editor.DarkMode)
			{
				textEditor.Background = (SolidColorBrush) new BrushConverter().ConvertFromString("#1E1E1E");
				textEditor.Foreground = Brushes.White;
				textEditor.TextArea.TextView.LinkTextForegroundBrush =
					(SolidColorBrush) new BrushConverter().ConvertFromString("#5151FF");
			}

			if (lang != "")
				using (Stream s = new MemoryStream(Encoding.ASCII.GetBytes(Resources.ResourceManager.GetString(lang))))
				{
					using (var reader = new XmlTextReader(s))
					{
						textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
					}
				}

			var host = new ElementHost {Dock = DockStyle.Fill, Child = textEditor};
			Controls.Add(host);

			DoAction(EditorAction.ParseCode);
		}

		private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
		{
			FollowingCharacter(e.Text);
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
			line++;
			col++;
			if (line > 0)
			{
				var pos = textEditor.Document.GetOffset(line, col);
				textEditor.ScrollToLine(line);
				textEditor.CaretOffset = pos;
				textEditor.Focus();
			}
		}

		private void TextEditor_TextChanged(object sender, EventArgs e)
		{
			if (!Text.EndsWith("*"))
				Text += "*";
		}

		private void TextEditorTextAreaCaret_PositionChanged(object sender, EventArgs e)
		{
			var line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
			var col  = textEditor.CaretOffset - line.Offset;
			Editor.TheEditor.SetColumnLabel($"Ln: {line.LineNumber} Col: {col}");
		}

		private void TextEditor_GotFocus(object sender, RoutedEventArgs e)
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
				case EditorAction.Format_CL:
					FormatCl();

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
				case EditorAction.TasksUpdate:
					TaskListUpdate();

					break;
			}
		}

		private void Parse()
		{
			var items      = new List<TaskItem>();
			var lineNumber = 0;

			var code = "";
			Invoke((MethodInvoker) delegate { code = GetText(); });

			// code may be ""
			foreach (var line in code.Split(new[] {Environment.NewLine}, StringSplitOptions.None))
			{
				lineNumber++;

				foreach (var keyword in Program.TaskKeywords)
				{
					var charIndex = line.IndexOf("//" + keyword);
					if (charIndex >= 0)
						items.Add(new TaskItem {Line = lineNumber, Text = line.Substring(charIndex + 2)});
				}
			}

			Tasks = items.ToArray();
		}

		private void TaskListUpdate()
		{
			Invoke((MethodInvoker) delegate { Editor.Tasklist.Display(Text, Tasks); });
		}

		private void DuplicateLine()
		{
			var line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
			textEditor.Select(line.Offset, line.Length);
			var value = textEditor.SelectedText;
			value += Environment.NewLine + value;

			textEditor.SelectedText    = value;
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
			if (Text.EndsWith("*"))
				return;

			if (!CurrentSaving)
			{
				if (!(Tag is RemoteSource memberInfo))
					return;

				var saveAsWindow = new SaveAs(memberInfo);
				saveAsWindow.ShowDialog();

				if (!saveAsWindow.Success)
					return;

				Thread gothread = null;
				CurrentSaving = true;
				Editor.TheEditor.SetStatus("Saving " + memberInfo.GetName() + "..");
				switch (saveAsWindow.SourceInfo().GetFS())
				{
					case FileSystem.QSYS:
						gothread = new Thread((ThreadStart) delegate
						{
							var uploadResult = IBMiUtils.UploadMember(memberInfo.GetLocalFile(),
								saveAsWindow.SourceInfo().GetLibrary(),
								saveAsWindow.SourceInfo().GetSPF(),
								saveAsWindow.SourceInfo().GetMember());

							if (uploadResult == false)
								MessageBox.Show("Failed to upload to " + memberInfo.GetName() + ".");

							Invoke((MethodInvoker) delegate
							{
								Editor.TheEditor.SetStatus(
									memberInfo.GetName() + " " + (uploadResult ? "" : "not ") + "saved.");
							});

							CurrentSaving = false;
						});

						break;

					case FileSystem.IFS:
						gothread = new Thread((ThreadStart) delegate
						{
							var uploadResult = IBMiUtils.UploadFile(memberInfo.GetLocalFile(),
								saveAsWindow.SourceInfo().GetIFSPath());

							if (uploadResult == false)
								MessageBox.Show("Failed to upload to " +
								                memberInfo.GetName() +
								                "." +
								                memberInfo.GetExtension() +
								                ".");

							Invoke((MethodInvoker) delegate
							{
								Editor.TheEditor.SetStatus(
									memberInfo.GetName() +
									"." +
									memberInfo.GetExtension() +
									" " +
									(uploadResult ? "" : "not ") +
									"saved.");
							});

							CurrentSaving = false;
						});

						break;
				}

				gothread?.Start();
			}
			else
			{
				MessageBox.Show("You must save the source before you can Save-As.");
			}
		}

		private void Save()
		{
			var sourceInfo   = (RemoteSource) Tag;
			var uploadResult = true;

			if (sourceInfo != null)
			{
				if (sourceInfo.IsEditable())
				{
					if (Language == Language.CL)
						if (IBMi.CurrentSystem.GetValue("CL_FORMAT_ON_SAVE") == "true")
							DoAction(EditorAction.Format_CL);

					if (!CurrentSaving)
					{
						CurrentSaving = true;

						Editor.TheEditor.SetStatus("Saving " + sourceInfo.GetName() + "..");
						var gothread = new Thread((ThreadStart) delegate
						{
							FileCache.EditsAdd(sourceInfo.GetLibrary(), sourceInfo.GetObject(), sourceInfo.GetName());
							Invoke((MethodInvoker) delegate
							{
								File.WriteAllText(sourceInfo.GetLocalFile(), GetText(), textEditor.Encoding);
							});

							switch (sourceInfo.GetFS())
							{
								case FileSystem.QSYS:
									uploadResult = IBMiUtils.UploadMember(sourceInfo.GetLocalFile(),
										sourceInfo.GetLibrary(),
										sourceInfo.GetObject(),
										sourceInfo.GetName());

									break;
								case FileSystem.IFS:
									uploadResult = IBMiUtils.UploadFile(sourceInfo.GetLocalFile(),
										sourceInfo.GetRemoteFile());

									break;
							}

							if (uploadResult == false)
							{
							}
							else
							{
								Invoke((MethodInvoker) delegate
								{
									if (Text.EndsWith("*"))
										Text = Text.Substring(0, Text.Length - 1);
								});
							}

							Invoke((MethodInvoker) delegate
							{
								Editor.TheEditor.SetStatus(
									sourceInfo.GetName() + " " + (uploadResult ? "" : "not ") + "saved.");
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
				File.WriteAllText(LocalPath, GetText(), textEditor.Encoding);
				if (Text.EndsWith("*"))
					Text = Text.Substring(0, Text.Length - 1);

				Editor.TheEditor.SetStatus("File saved locally.");
			}

			DoAction(EditorAction.ParseCode);
			DoAction(EditorAction.TasksUpdate);
		}

		private void CommentOutSelected()
		{
			if (ReadOnly)
				return;

			if (textEditor.SelectionLength == 0)
			{
				var line = textEditor.Document.GetLineByOffset(textEditor.CaretOffset);
				textEditor.Select(line.Offset, line.Length);
			}

			var lines = textEditor.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

			int index;
			switch (Language)
			{
				case Language.RPG:
					for (var i = 0; i < lines.Length; i++)
						if (string.IsNullOrWhiteSpace(lines[i]))
						{
							index    = GetSpaces(lines[i]);
							lines[i] = lines[i].Insert(index, "//");
						}

					textEditor.SelectedText = string.Join(Environment.NewLine, lines);

					break;

				case Language.CL:
				case Language.CPP:
					if (lines.Length == 1 && Language != Language.CL)
					{
						index    = GetSpaces(lines[0]);
						lines[0] = lines[0].Insert(index, "//");
					}
					else
					{
						lines[0]                = "/*" + lines[0];
						lines[lines.Length - 1] = lines[lines.Length - 1] + "*/";
					}

					textEditor.SelectedText = string.Join(Environment.NewLine, lines);

					break;

				case Language.SQL:
					for (var i = 0; i < lines.Length; i++)
						if (string.IsNullOrWhiteSpace(lines[i]))
						{
							index    = GetSpaces(lines[i]);
							lines[i] = lines[i].Insert(index, "--");
						}

					textEditor.SelectedText = string.Join(Environment.NewLine, lines);

					break;
			}
		}

		private static int GetSpaces(string line)
		{
			var index = 0;

			foreach (var c in line)
				if (c == ' ')
					index++;
				else
					break;

			return index;
		}

	#region TextEditors

		private void FormatCl()
		{
			if (ReadOnly)
				return;

			var lines = textEditor.Text.Split(new[] {"\n", "\r"}, StringSplitOptions.None);
			textEditor.SelectAll();
			textEditor.SelectedText = "";
			var length = RcdLen > 0 ? RcdLen : 80;
			textEditor.AppendText(string.Join(Environment.NewLine, CLFile.CorrectLines(lines, length)));
		}

		private void FollowingCharacter(string Char)
		{
			var nextChar = string.Empty;
			if (textEditor.CaretOffset < textEditor.Document.TextLength)
				nextChar = textEditor.Document.Text.Substring(textEditor.CaretOffset, 1);

			var closingChar = string.Empty;

			switch (Char)
			{
				case "'":
					closingChar = "'";

					break;

				case "\"":
					closingChar = "\"";

					break;

				case "(":
					closingChar = ")";

					break;

				case "{":
					closingChar = "}";

					break;
			}

			if (nextChar != closingChar)
			{
				textEditor.Document.Insert(textEditor.CaretOffset, closingChar);
				textEditor.CaretOffset -= 1;
			}
		}

	#endregion
	}
}