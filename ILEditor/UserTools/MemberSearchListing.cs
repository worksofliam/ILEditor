using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class MemberSearchListing : DockContent
	{
		private readonly string Library;
		private readonly string SearchValue;
		private readonly bool   Sensitive;
		private readonly string SPF;

		public MemberSearchListing(string Lib, string Spf, string Value, bool CaseSensitive = false)
		{
			InitializeComponent();

			Text = "'" + Value + "' Search";

			Library   = Lib;
			SPF       = Spf;
			Sensitive = CaseSensitive;

			if (Sensitive)
				SearchValue = Value;
			else
				SearchValue = Value.ToUpper();

			StartSearch();
		}

		private void StartSearch()
		{
			new Thread((ThreadStart) delegate
			{
				var treeOut = new List<TreeNode>();
				var members = Directory.GetFiles(IBMiUtils.GetLocalDir(Library, SPF));

				if (members.Length == 0)
				{
					treeOut.Add(new TreeNode("No local members for " + Library + "/" + SPF, 0, 0));
				}
				else
				{
					foreach (var member in members)
					{
						var name        = Path.GetFileNameWithoutExtension(Path.GetFileName(member)).ToUpper();
						var extension   = Path.GetExtension(member).Substring(1).ToUpper();
						var currentFile = new TreeNode(name, 2, 2);
						var currentLine = 1;
						foreach (var line in File.ReadAllLines(member))
						{
							var contains = false;
							if (Sensitive)
								contains = line.Contains(SearchValue);
							else
								contains = line.ToUpper().Contains(SearchValue);

							if (contains)
							{
								var currentResult = new TreeNode("Line " + currentLine, 3, 3);
								currentResult.Tag = new RemoteSource(member, Library, SPF, name, extension);
								currentFile.Nodes.Add(currentResult);
							}

							currentLine++;
						}

						if (currentFile.Nodes.Count > 0)
							treeOut.Add(currentFile);
					}

					if (treeOut.Count == 0)
						treeOut.Add(new TreeNode("No results found for '" + SearchValue + "' in " + Library + "/" + SPF,
							0,
							0));
					else
						treeOut.Insert(0,
							new TreeNode("Results for '" + SearchValue + "' in " + Library + "/" + SPF, 1, 1));
				}

				Invoke((MethodInvoker) delegate
				{
					searchResults.Nodes.Clear();
					searchResults.Nodes.AddRange(treeOut.ToArray());
				});
			}).Start();
		}

		private void searchResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag is RemoteSource member)
				Editor.OpenSource(member);
		}
	}
}