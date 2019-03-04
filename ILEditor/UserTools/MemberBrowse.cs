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
	public partial class MemberBrowse : DockContent
	{
		private readonly List<ListViewItem> curItems = new List<ListViewItem>();

		public MemberBrowse(string Lib = "", string Obj = "")
		{
			InitializeComponent();
			Text = "Member Browser";

			if (Lib != "" && Obj != "")
			{
				library.Text = Lib;
				spf.Text     = Obj;
			}
		}

		private void MemberBrowse_Load(object sender, EventArgs e)
		{
			if (library.Text != "" && spf.Text != "")
				fetchButton.PerformClick();
		}

		public void UpdateListing(string Lib, string Obj)
		{
			var gothread = new Thread((ThreadStart) delegate
			{
				bool noMembers;

				curItems.Clear();

				Invoke((MethodInvoker) delegate
				{
					memberList.Items.Clear();
					memberList.Items.Add(new ListViewItem("Loading...", 2));
				});

				var members = IBMiUtils.GetMemberList(Lib, Obj);

				Invoke((MethodInvoker) delegate { memberList.Items.Clear(); });

				if (members != null)
				{
					noMembers = members.Length == 0;
					if (!noMembers)
					{
						foreach (var member in members)
						{
							var curItem = new ListViewItem(new[] {member.Name, member.Extension, member.Text}, 0);

							curItem.Tag = member;

							curItems.Add(curItem);
						}

						Invoke((MethodInvoker) delegate
						{
							memberList.Items.AddRange(curItems.ToArray());
							membercount.Text = members.Length + " member" + (members.Length == 1 ? "" : "s");
						});
					}
				}
				else
				{
					noMembers = true;
				}

				if (noMembers)
					Invoke((MethodInvoker) delegate
					{
						memberList.Items.Add(new ListViewItem("No members found!", 1));
						membercount.Text = "0 members";
					});

				Invoke((MethodInvoker) delegate { addmember.Enabled = true; });
			});

			gothread.Start();
		}

		private void fetchButton_Click(object sender, EventArgs e)
		{
			library.Text = library.Text.Trim();
			spf.Text     = spf.Text.Trim();

			if (!IBMiUtils.IsValueObjectName(library.Text))
			{
				MessageBox.Show("Library name is not valid.");

				return;
			}

			if (library.Text.ToUpper() == "*ALL")
			{
				MessageBox.Show("Library name is not valid.");

				return;
			}

			if (!IBMiUtils.IsValueObjectName(spf.Text))
			{
				MessageBox.Show("Object name is not valid.");

				return;
			}

			Text = library.Text + "/" + spf.Text + " [Listing]";
			UpdateListing(library.Text, spf.Text);
			Welcome.JustOpened(library.Text, spf.Text);
		}

		private void memberList_DoubleClick(object sender, EventArgs e)
		{
			if (memberList.SelectedItems.Count != 1)
				return;

			var selection = memberList.SelectedItems[0];
			if (selection.Tag != null)
			{
				var member = (RemoteSource) selection.Tag;

				Editor.OpenSource(member);
			}
		}

		private void AddMember_Click(object sender, EventArgs e)
		{
			var newMemberForm = new NewMember(library.Text.Trim(), spf.Text.Trim());
			newMemberForm.ShowDialog();

			if (newMemberForm.IsCreated)
			{
				var curItem =
					new ListViewItem(new string[3] {newMemberForm.Mbr, newMemberForm.Type, newMemberForm.MemberText},
						0);

				curItem.Tag = new RemoteSource("",
					library.Text.Trim(),
					spf.Text.Trim(),
					newMemberForm.Mbr,
					newMemberForm.Type);

				memberList.Items.Add(curItem);
			}

			newMemberForm.Dispose();
		}

	#region rightclick

		private RemoteSource currentRightClick;

		private void memberList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			if (memberList.FocusedItem.Bounds.Contains(e.Location))
			{
				currentRightClick = (RemoteSource) memberList.FocusedItem.Tag;
				compileRightclick.Show(Cursor.Position);
			}
		}

		private void compileRightClick_Opening(object sender, CancelEventArgs e)
		{
			compileOtherToolStripMenuItem.DropDownItems.Clear();
			var compiles = new List<ToolStripMenuItem>();
			if (currentRightClick != null)
			{
				var memberInfo = currentRightClick;
				var items      = IBMi.CurrentSystem.GetValue("TYPE_" + memberInfo.Extension).Split('|');
				foreach (var item in items)
				{
					if (item.Trim() == "")
						continue;

					compiles.Add(new ToolStripMenuItem(item, null, compileAnyHandle));
				}
			}

			compileToolStripMenuItem.Enabled      = compiles.Count > 0;
			compileOtherToolStripMenuItem.Enabled = compiles.Count > 0;
			compileOtherToolStripMenuItem.DropDownItems.AddRange(compiles.ToArray());
		}

		private void compileAnyHandle(object sender, EventArgs e)
		{
			var clickedItem = (ToolStripMenuItem) sender;
			if (currentRightClick != null)
				IBMiUtils.CompileSource(currentRightClick, clickedItem.Text);
		}

		private void compileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (currentRightClick != null)
				new Thread((ThreadStart) delegate { IBMiUtils.CompileSource(currentRightClick); }).Start();
		}

	#endregion
	}
}