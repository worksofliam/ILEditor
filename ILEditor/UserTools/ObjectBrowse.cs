using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class ObjectBrowse : DockContent
	{
		private readonly List<ListViewItem> curItems = new List<ListViewItem>();

		private readonly Dictionary<string, int> IconKeys = new Dictionary<string, int>
		{
			{"*PGM", 0}, {"*SRVPGM", 1}, {"*MODULE", 2}, {"*BNDDIR", 3}
		};

		private ILEObject[] Objects;

		public ObjectBrowse()
		{
			InitializeComponent();
			Text = "Object Browser";
		}

		public void UpdateListing(string Lib)
		{
			var gothread = new Thread((ThreadStart) delegate
			{
				curItems.Clear();

				Invoke((MethodInvoker) delegate
				{
					objectList.Items.Clear();
					objectList.Items.Add(new ListViewItem("Loading...", 2));
				});

				Objects = IBMiUtils.GetObjectList(Lib, "*PGM *SRVPGM *MODULE *BNDDIR");

				Invoke((MethodInvoker) delegate { objectList.Items.Clear(); });

				if (Objects != null)
				{
					foreach (var Object in Objects)
					{
						var curItem = new ListViewItem(new string[4]
							{
								Object.Name, Object.Extension, Object.Type, Object.Text
							},
							0);

						curItem.Tag = Object;

						if (IconKeys.ContainsKey(Object.Type))
							curItem.ImageIndex = IconKeys[Object.Type];
						else
							curItem.ImageIndex = -1;

						curItems.Add(curItem);
					}

					Invoke((MethodInvoker) delegate
					{
						objectList.Items.AddRange(curItems.ToArray());
						programcount.Text = Objects.Length + " object" + (Objects.Length == 1 ? "" : "s");
					});
				}
				else
				{
					Invoke((MethodInvoker) delegate
					{
						objectList.Items.Add(new ListViewItem("No objects found!", 1));
						programcount.Text = "0 objects";
					});
				}
			});

			gothread.Start();
		}

		private void fetchButton_Click(object sender, EventArgs e)
		{
			library.Text = library.Text.Trim();

			if (!IBMiUtils.IsValueObjectName(library.Text))
			{
				MessageBox.Show("Library name is not valid.");

				return;
			}

			Text = library.Text + " [Listing]";
			UpdateListing(library.Text);
		}

	#region rightclick

		private ILEObject currentRightClick;

		private void objectList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			if (objectList.FocusedItem.Bounds.Contains(e.Location))
			{
				currentRightClick = (ILEObject) objectList.FocusedItem.Tag;
				objectRightClick.Show(Cursor.Position);
			}
		}

		private void objectRightClick_Opening(object sender, CancelEventArgs e)
		{
			if (currentRightClick == null)
				return;

			objectInformationToolStripMenuItem.Enabled = true;
			openSourceToolStripMenuItem.Enabled        = false;
			updateToolStripMenuItem.Enabled            = false;

			switch (currentRightClick.Type)
			{
				case "*BNDDIR":
					openSourceToolStripMenuItem.Enabled = true;

					break;
				case "*PGM":
				case "*SRVPGM":
					updateToolStripMenuItem.Enabled = true;

					break;
				default:
					openSourceToolStripMenuItem.Enabled = currentRightClick.SrcMbr != "";

					break;
			}
		}

		private void openSourceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (currentRightClick == null)
				return;

			switch (currentRightClick.Type)
			{
				case "*BNDDIR":
					Editor.TheEditor.AddTool(new BindingDirectory(currentRightClick.Library, currentRightClick.Name));

					break;
				default:
					Editor.OpenSource(new RemoteSource("",
						currentRightClick.SrcLib,
						currentRightClick.SrcSpf,
						currentRightClick.SrcMbr,
						currentRightClick.Extension));

					break;
			}
		}

		private void objectInformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (currentRightClick != null)
				new ObjectInformation(currentRightClick).Show();
		}

		private void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (currentRightClick != null)
				new UpdateProgram(currentRightClick, Objects).Show();
		}

	#endregion
	}
}