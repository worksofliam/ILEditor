using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
	public partial class SpoolListing : DockContent
	{
		public SpoolListing()
		{
			InitializeComponent();

			Text = "Spool Listing";

			RefreshList();
		}

		public void RefreshList()
		{
			string lib = IBMi.CurrentSystem.GetValue("printerLib"), obj = IBMi.CurrentSystem.GetValue("printerObj");

			var spoolThread = new Thread((ThreadStart) delegate
			{
				var items   = new List<ListViewItem>();
				var listing = IBMiUtils.GetSpoolListing(lib, obj);

				if (listing != null)
					foreach (var spool in listing)
					{
						var curItem = new ListViewItem(new[]
							{
								spool.GetName(), spool.GetData(), spool.GetStatus(), spool.GetJob()
							},
							0);

						curItem.Tag = spool;
						items.Add(curItem);
					}
				else
					items.Add(new ListViewItem("No spool files found."));

				if (spoolList != null)
					Invoke((MethodInvoker) delegate
					{
						spoolList.Items.Clear();
						spoolList.Items.AddRange(items.ToArray());
					});
			});

			if (lib == "" || obj == "")
				MessageBox.Show(
					"You must setup the Output Queue in the Connection Settings for the spool file listing to work.",
					"Spool Listing",
					MessageBoxButtons.OK,
					MessageBoxIcon.Asterisk);
			else
				spoolThread.Start();
		}

		private void spoolList_DoubleClick(object sender, EventArgs e)
		{
			if (spoolList.SelectedItems.Count != 1)
				return;

			var selection = spoolList.SelectedItems[0];
			if (selection.Tag is SpoolFile spool)
				new Thread((ThreadStart) delegate
				{
					var spoolFile = spool.Download();

					if (spoolFile != "")
						Invoke((MethodInvoker) delegate
						{
							Editor.OpenLocalSource(spoolFile, Language.None, spool.GetName(), true);
						});
					else
						MessageBox.Show("Spool file was not downloaded. Please check the spool file exists.");
				}).Start();
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			RefreshList();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			var result =
				MessageBox.Show(
					"Are you sure you want to delete all spool files of user " +
					IBMi.CurrentSystem.GetValue("username") +
					"? This process can take some time.",
					"Continue",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Hand);

			if (result != DialogResult.Yes)
				return;

			Editor.TheEditor.SetStatus("Deleting all spool files..");
			new Thread((ThreadStart) delegate
			{
				if (IBMi.RemoteCommand("DLTSPLF FILE(*SELECT)"))
					Invoke((MethodInvoker) delegate
					{
						Editor.TheEditor.SetStatus("Spool files deleted.");
						spoolList.Items.Clear();
					});
				else
					MessageBox.Show("Failed to delete all spool files.");
			}).Start();
		}
	}
}