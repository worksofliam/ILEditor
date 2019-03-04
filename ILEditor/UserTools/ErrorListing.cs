using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class ErrorListing : DockContent
	{
		private readonly string _library;
		private readonly string _object;

		public ErrorListing(string library = "", string obj = "")
		{
			InitializeComponent();
			_library = library;
			_object  = obj;
		}

		private void ErrorListing_Load(object sender, EventArgs e)
		{
			var gothread = new Thread(() =>
			{
				ErrorHandle.GetErrors(_library, _object);
				PublishErrors();
			});

			gothread.Start();
		}

		public void PublishErrors()
		{
			Invoke((MethodInvoker) delegate
			{
				var totalErrors = 0;

				if (ErrorHandle.WasSuccessful())
				{
					treeView1.Nodes.Clear(); //Clear out the nodes

					//Add the errors
					foreach (var fileId in ErrorHandle.GetFileIDs())
					{
						var curNode = new TreeNode(ErrorHandle.GetFilePath(fileId));
						foreach (var error in ErrorHandle.GetErrors(fileId))
							if (error.Sev >= 20)
							{
								totalErrors += 1;
								var curErr = curNode.Nodes.Add((error.Code == "" ? "" : error.Code + ": ") +
								                               error.Data.Trim() +
								                               " (" +
								                               error.Line +
								                               ")");

								if (error.Code != "")
									curErr.Tag = error.Line.ToString() + ',' + error.Column;

								curErr.ImageIndex         = 1;
								curErr.SelectedImageIndex = 1;
							}

						//Only add a node if there is something to display
						if (curNode.Nodes.Count > 0)
						{
							curNode.ImageIndex         = 0;
							curNode.SelectedImageIndex = 0;
							treeView1.Nodes.Add(curNode);
						}
					}

					if (totalErrors == 0)
						treeView1.Nodes.Add(new TreeNode("No errors found for " + _library + "/" + _object + ".",
							2,
							2));

					if (treeView1.Nodes.Count <= 1)
						treeView1.ExpandAll();
				}

				toolStripStatusLabel1.Text = "Total errors: " + totalErrors;
				toolStripStatusLabel2.Text = ErrorHandle.DoName();
				toolStripStatusLabel3.Text = DateTime.Now.ToString("h:mm:ss tt");
			});
		}

		private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag == null)
				return;

			var data = e.Node.Tag.ToString().Split(',');

			//string error;

			var line = int.Parse(data[0]) - 1;
			var col  = int.Parse(data[1]);

			//error = e.Node.Text;
			if (col > 0)
				col--;

			var name = e.Node.Parent.Text;
			if (name.Substring(0, 1) == "/")
			{
				name = name.Split('/').Last();
			}
			else
			{
				name = name.Split('(').Last();
				name = name.Substring(0, name.Length - 1);
			}

			OnSelectError(name, line, col);
		}

		//todo the code of this method is almost identical to code of method SelectTask in TaskList.cs.
		private void OnSelectError(string File, int Line, int Col)
		{
			var theTab = Editor.TheEditor.GetTabByTitle(File, true);

			if (theTab != null)
			{
				var sourceEditor = Editor.TheEditor.GetTabEditor(theTab);

				sourceEditor.Focus();
				sourceEditor.GotoLine(Line, Col);
			}
			else
			{
				MessageBox.Show("Unable to open error. Please open the source manually first and then try again.",
					"Information",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
		}
	}
}