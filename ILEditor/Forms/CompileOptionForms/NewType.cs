using System;
using System.Linq;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms.CompileOptionForms
{
	public partial class NewType : Form
	{
		public NewType()
		{
			InitializeComponent();
		}

		private void create_Click(object sender, EventArgs e)
		{
			var items = IBMi.CurrentSystem.GetValue("CMPTYPES").Split('|').ToList();
			type.Text = type.Text.Trim();
			if (type.Text.Trim() != string.Empty && !items.Contains(type.Text))
			{
				items.Add(type.Text);
				IBMi.CurrentSystem.SetValue("CMPTYPES", string.Join("|", items));
				Close();
			}
		}
	}
}