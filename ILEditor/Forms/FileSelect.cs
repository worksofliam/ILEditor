using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class FileSelect : Form
	{
		private string Command;
		public  bool   Success;

		public FileSelect()
		{
			InitializeComponent();
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void select_Click(object sender, EventArgs e)
		{
			var valid   = true;
			var Lib     = lib.Text.Trim();
			var Obj     = obj.Text.Trim();
			var type    = sqlType.Text.Trim().ToUpper();
			var options = new List<string>();

			if (!IBMiUtils.IsValueObjectName(Lib))
				valid = false;

			if (!IBMiUtils.IsValueObjectName(Lib))
				valid = false;

			if (!valid)
			{
				MessageBox.Show("Member information not valid.");

				return;
			}

			if (type == "VIEW")
				options.Add("index_instead_of_view_option => ''1''");

			options.Add("REPLACE_OPTION => ''1''");

			Command = "RUNSQL SQL('CALL QSYS2.GENERATE_SQL(''" +
			          Obj +
			          "'', ''" +
			          Lib +
			          "'', ''" +
			          type +
			          "'', " +
			          string.Join(", ", options) +
			          ")')";

			Success = true;
			Close();
		}

		public string GetCommand()
		{
			return Command;
		}
	}
}