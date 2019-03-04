using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class QsysBrowser : DockContent
	{
		private SPFSelect _window;

		public QsysBrowser()
		{
			InitializeComponent();
			Text = "QSYS Browser";
		}

		private void AddItem(string value)
		{
			var items = IBMi.CurrentSystem.GetValue("TREE_LIST").Split('|').ToList();
			if (!items.Contains(value) && AddSpf(value))
			{
				items.Add(value);
				IBMi.CurrentSystem.SetValue("TREE_LIST", string.Join("|", items));
			}
		}

		private bool AddSpf(string Value)
		{
			var added = false;

			Value = Value.ToUpper();
			var path = Value.Split('/');

			if (IBMiUtils.IsValueObjectName(path[0]) && IBMiUtils.IsValueObjectName(path[1]))
			{
				TreeNode lib;
				if (objectList.Nodes.ContainsKey(path[0]))
				{
					lib = objectList.Nodes[path[0]];
				}
				else
				{
					lib                    = new TreeNode(path[0]) {Name = path[0], ImageIndex = 1};
					lib.SelectedImageIndex = lib.ImageIndex;
					objectList.Nodes.Add(lib);
				}

				if (!lib.Nodes.ContainsKey(path[0]))
				{
					var spf = new TreeNode(path[1]) {Name = path[1], Tag = string.Join("/", Value), ImageIndex = 2};
					spf.SelectedImageIndex = spf.ImageIndex;
					spf.Nodes.Add("Loading..");

					//assign tag here also
					lib.Nodes.Add(spf);
					added = true;
				}
			}

			return added;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			var value = "";
			_window = new SPFSelect();
			_window.ShowDialog();

			if (!_window.Successful)
				return;

			if (_window.Spf != "")
			{
				value = (_window.Lib + "/" + _window.Spf).ToUpper();
				AddItem(value);
			}
			else
			{
				var gothread = new Thread((ThreadStart) delegate
				{
					var objects = IBMiUtils.GetSpfList(_window.Lib);
					if (objects != null)
						foreach (var obj in objects)
						{
							value = (obj.Library + "/" + obj.Name).ToUpper();

							Invoke((MethodInvoker) delegate { AddItem(value); });
						}
				});

				gothread.Start();
			}
		}

		private void RequestRefresh(TreeNode node)
		{
			TreeNode       mbr;
			List<TreeNode> items;
			string[]       path;

			if (!(node.Tag is string))
				return;

			var gothread = new Thread((ThreadStart) delegate
			{
				path  = node.Tag.ToString().Split('/');
				items = new List<TreeNode>();
				var members = IBMiUtils.GetMemberList(path[0], path[1]);

				if (members != null)
				{
					foreach (var member in members)
					{
						mbr = new TreeNode(member.Name +
						                   (member.Extension == "" ? "" : "." + member.Extension.ToLower()) +
						                   (member.Text == "" ? "" : " - " + member.Text))
						{
							Tag = member, ImageIndex = 3
						};

						mbr.SelectedImageIndex = mbr.ImageIndex;
						items.Add(mbr);
					}

					if (members.Length == 0)
						items.Add(new TreeNode("No members found."));
				}
				else
				{
					items.Add(new TreeNode("No members found."));
				}

				Invoke((MethodInvoker) delegate
				{
					node.Nodes.Clear();
					node.Nodes.AddRange(items.ToArray());
				});
			});

			gothread.Start();
		}

		private void objectList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			var node = e.Node;
			RequestRefresh(node);
		}

		private void objectList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag is RemoteSource member)
				Editor.OpenSource(member);
		}

		private void TreeBrowse_Load(object sender, EventArgs e)
		{
			var items = IBMi.CurrentSystem.GetValue("TREE_LIST").Split('|').ToList();

			foreach (var item in items)
			{
				if (item == "")
					continue;

				AddSpf(item);
			}
		}

		private void objectList_KeyDown(object sender, KeyEventArgs e)
		{
			var selectedNode = objectList.SelectedNode;

			if (selectedNode == null)
				return;

			switch (e.KeyCode)
			{
				case Keys.Delete:
					if (selectedNode.Tag != null)
					{
						var path = selectedNode.Tag.ToString();
						if (path.Contains("/"))
						{
							var confirmResult = MessageBox.Show("Are you sure to delete this shortcut?",
								"Delete shortcut",
								MessageBoxButtons.YesNo);

							if (confirmResult == DialogResult.Yes)
							{
								var items = IBMi.CurrentSystem.GetValue("TREE_LIST").Split('|').ToList();
								items.Remove(path);
								IBMi.CurrentSystem.SetValue("TREE_LIST", string.Join("|", items));
								objectList.Nodes.Remove(selectedNode);
							}
						}
					}

					break;
				case Keys.F5:
					if (selectedNode.Tag == null)
						foreach (TreeNode node in selectedNode.Nodes)
							RequestRefresh(node);
					else if (selectedNode.Tag is string)
						RequestRefresh(selectedNode);

					break;
			}
		}
	}
}