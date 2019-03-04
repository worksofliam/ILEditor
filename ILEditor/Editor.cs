using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ILEditor.Classes;
using ILEditor.Forms;
using ILEditor.UserTools;
using WeifenLuo.WinFormsUI.Docking;
using ToolStripRenderer = ILEditor.Classes.ToolStripRenderer;

namespace ILEditor
{
	public partial class Editor : Form
	{
		public static bool         DarkMode;
		public static Editor       TheEditor;
		public static SourceEditor LastEditing;
		public static TaskList     TaskList;

		public Editor()
		{
			InitializeComponent();
			TheEditor = this;

			FileCache.Import();

			Text += ' ' + Program.GetVersion() + " (" + IBMi.CurrentSystem.GetValue("alias") + ")";
			if (!IBMi.IsConnected)
				Text += " - Offline Mode";

			if (IBMi.IsConnected)
				if (IBMi.CurrentSystem.GetValue("lastOffline") == "true")
				{
					var result =
						MessageBox.Show(
							"Looks like your last session was in Offline Mode. Would you like the launch the SPF Push tool?",
							"Notice",
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Information);

					if (result == DialogResult.Yes)
						new PushWindow().ShowDialog();
				}

			IBMi.CurrentSystem.SetValue("lastOffline", (IBMi.IsConnected == false).ToString().ToLower());

			DarkMode = Program.Config.GetValue("darkmode") == "true";

			if (DarkMode)
				dockingPanel.Theme = new VS2015DarkTheme();
			else
				dockingPanel.Theme = new VS2015LightTheme();

			ApplyControlTheme(toolStrip);
			ApplyControlTheme(menuStrip);
			ApplyControlTheme(statusStrip);

			if (File.Exists(Program.PanelsXML))
				try
				{
					dockingPanel.LoadFromXml(Program.PanelsXML, GetContentFromPersistString);
				}
				catch
				{
					File.Delete(Program.PanelsXML);
				}
			else
				AddTool(new UserToolList(), DockState.DockLeft, true);

			AddTool(new Welcome(), DockState.Document, true);

			TaskList = new TaskList();

			AddTool(TaskList, DockState.DockBottomAutoHide, true);

			dockingPanel.ActiveContentChanged += DockingPanel_ActiveContentChanged;
		}

		public void SetStatus(string text)
		{
			statusText.Text = text;
		}

		public void SetColumnLabel(string text)
		{
			columnText.Text = text;
		}

		public void AddTool(DockContent Content, DockState dock = DockState.Document, bool Replace = false)
		{
			DockPanel content = null;

			if (Replace)
				for (var i = 0; i < dockingPanel.Panes.Count; i++)
				{
					var currentPane = dockingPanel.Panes[i];
					if (Content.Text == currentPane.CaptionText)
					{
						content = currentPane.DockPanel;
						dock    = currentPane.DockState;
						currentPane.Dispose();
					}
				}

			if (content == null)
				Content.Show(dockingPanel, dock);
			else
				Content.Show(content, dock);
		}

		public static void ApplyControlTheme(Control content)
		{
			if (DarkMode)
			{
				if (content is ToolStrip toolStrip)
					toolStrip.Renderer = new ToolStripRenderer();

				content.BackColor = Color.FromArgb(45, 45, 48);
				content.ForeColor = Color.White;

				foreach (Control childControl in content.Controls)
					ApplyControlTheme(childControl);
			}
		}

		public static void OpenSource(RemoteSource Source)
		{
			SourceEditor sourcePanel;
			var          resultFile = "";
			var          text       = Source.Name + (Source.Extension != "" ? '.' + Source.Extension.ToLower() : "");

			TheEditor.SetStatus("Fetching file " + text + "...");

			new Thread((ThreadStart) delegate
			{
				switch (Source.FileSystem)
				{
					case FileSystem.QSYS:
						resultFile = IBMiUtils.DownloadMember(Source.Library,
							Source.Object,
							Source.Name,
							Source.Extension);

						break;
					case FileSystem.IFS:
						resultFile = IBMiUtils.DownloadFile(Source.RemoteFile);

						break;
				}

				if (resultFile != "")
				{
					TheEditor.SetStatus("Opening file " + text + "...");

					Source.LocalFile = resultFile;
					Source.Lock();

					sourcePanel = new SourceEditor(Source.LocalFile,
						GetBoundLangType(Source.Extension),
						Source.RecordLength,
						!Source.IsEditable);

					sourcePanel.Tag  = Source;
					sourcePanel.Text = text;

					switch (Source.FileSystem)
					{
						case FileSystem.QSYS:
							sourcePanel.ToolTipText =
								Source.Library +
								"/" +
								Source.Object +
								":" +
								Source.Name +
								"." +
								Source.Extension.ToLower();

							break;
						case FileSystem.IFS:
							sourcePanel.ToolTipText = Source.RemoteFile;

							break;
					}

					TheEditor.Invoke((MethodInvoker) (() => TheEditor.AddTool(sourcePanel, DockState.Document, false)));
				}
				else
				{
					switch (Source.FileSystem)
					{
						case FileSystem.QSYS:
							MessageBox.Show("Unable to download member " +
							                Source.Library +
							                "/" +
							                Source.Object +
							                "." +
							                Source.Name +
							                ". Please check it exists and that you have access to the remote system.");

							break;
					}
				}
			}).Start();
		}

		public static void OpenExistingSource(RemoteSource Source)
		{
			var text = Source.Name + (Source.Extension != "" ? '.' + Source.Extension.ToLower() : "");

			if (File.Exists(Source.LocalFile))
			{
				var sourcePanel = new SourceEditor(Source.LocalFile,
					GetBoundLangType(Source.Extension),
					Source.RecordLength,
					!Source.IsEditable);

				sourcePanel.Tag         = Source;
				sourcePanel.Text        = text;
				sourcePanel.ToolTipText = Source.LocalFile;

				Source.Lock();
				TheEditor.AddTool(sourcePanel, DockState.Document);
			}
			else
			{
				MessageBox.Show("There was an error opening the local file. '" + Source.LocalFile + "' does not exist");
			}
		}

		public static void OpenLocalSource(string   FilePath,
		                                   Language Language,
		                                   string   Title    = null,
		                                   bool     ReadOnly = false)
		{
			var text = Path.GetFileName(FilePath);

			if (File.Exists(FilePath))
			{
				var sourcePanel = new SourceEditor(FilePath, Language, 0, ReadOnly);

				if (Title != null)
					sourcePanel.Text = Title;
				else
					sourcePanel.Text = text;

				sourcePanel.ToolTipText = FilePath;

				TheEditor.AddTool(sourcePanel, DockState.Document);
			}
			else
			{
				MessageBox.Show("There was an error opening the local file. '" + text + "' does not exist");
			}
		}

		public DockContent GetTabByTitle(string title, bool focus = false)
		{
			foreach (var pane in dockingPanel.Panes)
			{
				foreach (var window in pane.Contents.OfType<DockContent>())
					if (window.Text.StartsWith(title))
					{
						if (focus)
							window.Activate();

						return window;
					}
			}

			return null;
		}

		public SourceEditor GetTabEditor(DockContent Tab)
		{
			if (Tab is SourceEditor editor)
				return editor;

			return null;
		}

		private void dockingPanel_ContentRemoved(object sender, DockContentEventArgs e)
		{
			var panel = sender as DockPanel;

			if (panel?.ActiveContent is SourceEditor sourceEditor)
			{
				LastEditing = null;
				var src = sourceEditor.Tag as RemoteSource;
				src?.Unlock();
			}
		}

		private void DockingPanel_ActiveContentChanged(object sender, EventArgs e)
		{
			var previousEditor = GetTabEditor(dockingPanel.ActiveContent as DockContent);

			if (previousEditor != null)
			{
				LastEditing = previousEditor;
				LastEditing.DoAction(EditorAction.TasksUpdate);
			}
		}

		private void Editor_FormClosing(object sender, FormClosingEventArgs e)
		{
			FileCache.Export();
			dockingPanel.SaveAsXml(Program.PanelsXML);
		}

		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(CompileOptions).ToString())
				return new CompileOptions();

			if (persistString == typeof(IFSBrowser).ToString())
				return new IFSBrowser();

			if (persistString == typeof(MemberBrowse).ToString())
				return new MemberBrowse();

			if (persistString == typeof(ObjectBrowse).ToString())
				return new ObjectBrowse();

			if (persistString == typeof(QsysBrowser).ToString())
				return new QsysBrowser();

			if (persistString == typeof(TaskList).ToString())
				return new TaskList();

			if (persistString == typeof(UserToolList).ToString())
				return new UserToolList();

			return null;
		}

		public static BitmapImage ConvertBitmap(Bitmap src)
		{
			BitmapImage image;
			using (var ms = new MemoryStream())
			{
				src.Save(ms, ImageFormat.Bmp);
				image = new BitmapImage();
				image.BeginInit();
				ms.Seek(0, SeekOrigin.Begin);
				image.StreamSource = ms;
			}

			image.EndInit();

			return image;
		}

	#region SourceInfo

		public static readonly Dictionary<string, Language> LangTypes = new Dictionary<string, Language>
		{
			{"RPG", Language.RPG},
			{"RPGLE", Language.RPG},
			{"SQLRPGLE", Language.RPG},
			{"CL", Language.CL},
			{"CLLE", Language.CL},
			{"CLP", Language.CL},
			{"CMD", Language.CL},
			{"CPP", Language.CPP},
			{"SQLCPP", Language.CPP},
			{"C", Language.CPP},
			{"SQLC", Language.CPP},
			{"SQL", Language.SQL},
			{"CBL", Language.COBOL},
			{"COBOL", Language.COBOL},
			{"SQLCOBOL", Language.COBOL},
			{"CBLLE", Language.COBOL},
			{"SQLCBLLE", Language.COBOL},
			{"PYTHON", Language.Python},
			{"PY", Language.Python}
		};

		public static Language GetBoundLangType(string Obj)
		{
			Obj = Obj.ToUpper();

			if (LangTypes.ContainsKey(Obj))
				return LangTypes[Obj];

			return Language.None;
		}

	#endregion

	#region File Dropdown

		private void memberToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newMemberForm = new NewMember();

			newMemberForm.ShowDialog();
			if (newMemberForm.IsCreated)
			{
				var member = new RemoteSource("",
					newMemberForm.Lib,
					newMemberForm.Spf,
					newMemberForm.Mbr,
					newMemberForm.Type,
					true);

				OpenSource(member);
			}

			newMemberForm.Dispose();
		}

		private void streamFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var window = new CreateStreamFile();
			window.ShowDialog();

			if (window.Result != null)
				OpenExistingSource(window.Result);
		}

		private void sourcePhysicalFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new NewSPF().ShowDialog();
		}

		private void memberToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			new OpenSource(0).ShowDialog();
		}

		private void streamFileToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			new OpenSource(1).ShowDialog();
		}

		private void localFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var fileSelect = new OpenFileDialog())
			{
				var result = fileSelect.ShowDialog();
				if (result == DialogResult.OK) // Test result.
				{
					var ext = Path.GetExtension(fileSelect.FileName);
					if (ext?.StartsWith(".") == true)
						ext = ext.Substring(1);

					OpenLocalSource(fileSelect.FileName, GetBoundLangType(ext));
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Save);
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Save_As);
		}

		private void switchSystemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Restart();
		}

	#endregion

	#region Compile Dropdown

		private void CompileAnyHandle(object sender, EventArgs e)
		{
			if (LastEditing.Tag != null)
			{
				var clickedItem = (ToolStripMenuItem) sender;
				var sourceInfo  = (RemoteSource) LastEditing.Tag;
				new Thread(() => IBMiUtils.CompileSource(sourceInfo, clickedItem.Text)).Start();
			}
		}

		private void compileToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (LastEditing != null)
			{
				var sourceInfo = (RemoteSource) LastEditing.Tag;
				new Thread(() => IBMiUtils.CompileSource(sourceInfo)).Start();
			}
		}

		private void compileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			compileOptionsToolStripMenuItem.DropDownItems.Clear();
			var compiles   = new List<ToolStripMenuItem>();
			var sourceInfo = (RemoteSource) LastEditing?.Tag;
			if (sourceInfo != null)
			{
				var items = IBMi.CurrentSystem.GetValue("TYPE_" + sourceInfo.Extension.ToUpper()).Split('|');
				foreach (var item in items)
				{
					if (string.IsNullOrWhiteSpace(item))
						continue;

					compiles.Add(new ToolStripMenuItem(item, null, CompileAnyHandle));
				}
			}

			compileToolStripMenuItem1.Enabled       = compiles.Count > 0;
			compileOptionsToolStripMenuItem.Enabled = compiles.Count > 0;
			compileOptionsToolStripMenuItem.DropDownItems.AddRange(compiles.ToArray());
		}

	#endregion

	#region Tools dropdown

		private void openToolboxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddTool(new UserToolList(), DockState.DockLeft);
		}

		private void openWelcomeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddTool(new Welcome());
		}

		private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new Connection().ShowDialog();
		}

		private void libraryListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new JobSettings().ShowDialog();
		}

		private void start5250SessionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var path = Program.Config.GetValue("acspath");
			if (path == "false")
				MessageBox.Show("Please setup the ACS path in the Connection Settings.",
					"Notice",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			else
				try
				{
					Process.Start(path, " /plugin=5250 /sso /system=" + IBMi.CurrentSystem.GetValue("system"));
				}
				catch
				{
					MessageBox.Show("Something went wrong launching the 5250 session.",
						"Notice",
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation);
				}
		}

		private void startRemoteDebugACSToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var path = Program.Config.GetValue("acspath");
			if (path == "false")
				MessageBox.Show("Please setup the ACS path in the Connection Settings.",
					"Notice",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			else
				try
				{
					Process.Start(path, " /plugin=sysdbg /system=" + IBMi.CurrentSystem.GetValue("system"));
				}
				catch
				{
					MessageBox.Show("Something went wrong launching the debug session.",
						"Notice",
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation);
				}
		}

		private void quickMemberSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new QuickFileSearch().Show();
		}

		private void sourceDiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new SourceCompareSelect().ShowDialog();
		}

	#endregion

	#region Source dropdown

		private void sPFCloneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new CloneWindow().ShowDialog();
		}

		private void sPFPushToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new PushWindow().ShowDialog();
		}

		private void memberSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new MemberSearch().ShowDialog();
		}

		private void cLFormattingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (LastEditing == null)
				return;

			var sourceInfo = (RemoteSource) LastEditing.Tag;
			var language   = GetBoundLangType(sourceInfo.Extension);
			if (language == Language.CL)
			{
				SetStatus("Formatting CL in " + sourceInfo.Name);
				LastEditing.DoAction(EditorAction.Format_CL);
			}
		}

		private void generateSQLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selectFile = new FileSelect();
			selectFile.ShowDialog();

			if (selectFile.Success)
			{
				IBMiUtils.UsingQTEMPFiles(new[] {"Q_GENSQL"});
				if (IBMi.RemoteCommand(selectFile.GetCommand()))
					OpenSource(new RemoteSource("", "QTEMP", "Q_GENSQL", "Q_GENSQL", "SQL", false));
				else
					MessageBox.Show("Error generating SQL source.");
			}
		}

		private void quickCommentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Comment_Out_Selected);
		}

		private void duplicateLineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Dupe_Line);
		}

	#endregion

	#region Help dropdown

		private void aboutILEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new About().ShowDialog();
		}

		private void sessionFTPLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start(IBMi.FTPFile);
		}

	#endregion

	#region ToolStrip

		private void newMember_Click(object sender, EventArgs e)
		{
			memberToolStripMenuItem.PerformClick();
		}

		private void saveSource_Click(object sender, EventArgs e)
		{
			saveToolStripMenuItem.PerformClick();
		}

		private void liblButton_Click(object sender, EventArgs e)
		{
			libraryListToolStripMenuItem.PerformClick();
		}

		private void compileButton_Click(object sender, EventArgs e)
		{
			compileToolStripMenuItem1.PerformClick();
		}

		private void acsButton_Click(object sender, EventArgs e)
		{
			start5250SessionToolStripMenuItem.PerformClick();
		}

		private void dbgButton_Click(object sender, EventArgs e)
		{
			startRemoteDebugACSToolStripMenuItem.PerformClick();
		}

		private void zoomOutButton_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Zoom_Out);
		}

		private void zoomInButton_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Zoom_In);
		}

		private void undoButton_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Undo);
		}

		private void redoButton_Click(object sender, EventArgs e)
		{
			LastEditing?.DoAction(EditorAction.Redo);
		}

		private void commentButton_Click(object sender, EventArgs e)
		{
			quickCommentToolStripMenuItem.PerformClick();
		}

	#endregion
	}
}