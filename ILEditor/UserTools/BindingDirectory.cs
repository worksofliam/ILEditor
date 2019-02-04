using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class BindingDirectory : DockContent
	{
		private readonly string Library;
		private readonly string Object;

		public BindingDirectory(string Lib, string Obj)
		{
			InitializeComponent();
			UpdateListing(Lib, Obj);

			Text = Lib + "/" + Obj + " Binding Directory";

			Library = Lib;
			Object  = Obj;
		}

		public void UpdateListing(string Lib, string Obj)
		{
			var gothread = new Thread((ThreadStart) delegate
			{
				var rows    = new List<ListViewItem>();
				var entries = IBMiUtils.GetBindingDirectory(Lib, Obj);
				if (entries != null)
				{
					foreach (var entry in entries)
					{
						var item = new ListViewItem(new string[6]
						{
							entry.Name,
							entry.Type,
							entry.Library,
							entry.Activation,
							entry.CreationDate,
							entry.CreationTime
						});

						item.Tag = entry;
						rows.Add(item);
					}

					Invoke((MethodInvoker) delegate
					{
						entriesList.Items.Clear();
						entriesList.Items.AddRange(rows.ToArray());
					});
				}
				else
				{
					MessageBox.Show("Unable to obtain binding directory!");
				}
			});

			gothread.Start();
		}

		private void entriesList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData != Keys.Delete || entriesList.SelectedItems.Count <= 0)
				return;

			var selected = entriesList.SelectedItems[0];
			if (selected.Tag != null)
			{
				var entry = (BindingEntry) selected.Tag;
				var command = "RMVBNDDIRE BNDDIR(" +
				              entry.BindingLib +
				              "/" +
				              entry.BindingObj +
				              ") OBJ((" +
				              entry.Library +
				              "/" +
				              entry.Name +
				              " " +
				              entry.Type +
				              "))";

				var result = MessageBox.Show("Are you sure you want to delete this binding entry?",
					"Deleting Binding Entry",
					MessageBoxButtons.YesNo);

				if (result == DialogResult.Yes)
				{
					var gothread = new Thread((ThreadStart) delegate
					{
						if (IBMi.RunCommands(new string[1] {command}) == false)
							Invoke((MethodInvoker) delegate { selected.Remove(); });
					});

					gothread.Start();
				}
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (!IBMiUtils.IsValueObjectName(objectName.Text))
			{
				MessageBox.Show("Object name is not valid.");
				objectName.Focus();

				return;
			}

			if (objectType.Text == "")
			{
				MessageBox.Show("Object type is not valid.");
				objectType.Focus();

				return;
			}

			if (IBMiUtils.IsValueObjectName(objectLib.Text) == false && objectLib.Text != "*LIBL")
			{
				MessageBox.Show("Object library name is not valid.");
				objectLib.Focus();

				return;
			}

			if (objectActivation.Text == "")
			{
				MessageBox.Show("Object activation is not valid.");
				objectType.Focus();

				return;
			}

			var entry = new BindingEntry
			{
				BindingLib   = Library,
				BindingObj   = Object,
				Name         = objectName.Text.Trim(),
				Library      = objectLib.Text.Trim(),
				Type         = objectType.Text,
				Activation   = objectActivation.Text,
				CreationDate = "",
				CreationTime = ""
			};

			var command = "ADDBNDDIRE BNDDIR(" +
			              Library +
			              "/" +
			              Object +
			              ") OBJ((" +
			              entry.Library +
			              "/" +
			              entry.Name +
			              " " +
			              entry.Type +
			              " " +
			              entry.Activation +
			              "))";

			var gothread = new Thread((ThreadStart) delegate
			{
				if (IBMi.RunCommands(new string[1] {command}) == false)
				{
					var item = new ListViewItem(new string[6]
					{
						entry.Name,
						entry.Type,
						entry.Library,
						entry.Activation,
						entry.CreationDate,
						entry.CreationTime
					});

					item.Tag = entry;
					Invoke((MethodInvoker) delegate { entriesList.Items.Add(item); });
				}
				else
				{
					MessageBox.Show("Unable to create binding entry.");
				}
			});

			gothread.Start();
		}
	}
}