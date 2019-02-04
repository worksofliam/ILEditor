using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Properties;
using Newtonsoft.Json;

namespace ILEditor.Forms
{
	public partial class FindReferences : Form
	{
		public FindReferences()
		{
			InitializeComponent();
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void fetch_Click(object sender, EventArgs e)
		{
			if (IBMiUtils.IsValueObjectName(lib.Text) && IBMiUtils.IsValueObjectName(pgm.Text))
			{
				var objects = IBMiUtils.GetProgramReferences(lib.Text, pgm.Text);

				var json = JsonConvert.SerializeObject(objects,
					Formatting.None,
					new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});

				var file = IBMiUtils.GetLocalFile("QTEMP", "Diagram", lib.Text + pgm.Text.Trim('*'), "html");
				var html = Resources.diagram;

				html = html.Replace("!JSONHERE!", json);
				html = html.Replace("!STYLE!", style.SelectedItem.ToString());

				File.WriteAllText(file, html);
				Process.Start(file);

				Close();
			}
			else
			{
				//TODO: Error message
				MessageBox.Show("Unable to create Object Diagram.",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}
	}
}