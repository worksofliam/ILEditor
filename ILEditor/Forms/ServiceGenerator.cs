using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class ServiceGenerator : Form
	{
		private List<string> Binder;

		private List<string> Ref;
		private List<string> Src;

		public ServiceGenerator()
		{
			InitializeComponent();
		}

		private void AddProc_Click(object sender, EventArgs e)
		{
			if (!procname.Text.Contains(" ") && procname.Text.Trim() != "")
			{
				procedures.Items.Add(procname.Text.Trim());
				procname.Clear();
			}
		}

		private void procedures_KeyDown(object sender, KeyEventArgs e)
		{
			if (procedures.SelectedItems.Count > 0)
				procedures.Items.RemoveAt(procedures.SelectedIndex);
		}

		private void ProcName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				addproc.PerformClick();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void generate_Click(object sender, EventArgs e)
		{
			var library = srvpgmlib.Text.Trim();
			if (library == "*CURLIB")
				library = IBMi.CurrentSystem.GetValue("curlib");

			if (!IBMiUtils.IsValueObjectName(library))
			{
				MessageBox.Show("Library name is invalid.");
				srvpgmlib.Focus();

				return;
			}

			if (!IBMiUtils.IsValueObjectName(srvpgmnam.Text))
			{
				MessageBox.Show("Service program name is invalid.");
				srvpgmnam.Focus();

				return;
			}

			if (procedures.Items.Count == 0)
			{
				MessageBox.Show("Please provide a list of procedures to generate");
				procname.Focus();

				return;
			}

			if (!IBMiUtils.IsValueObjectName(protspf.Text))
			{
				MessageBox.Show("Prototype SPF name invalid.");
				protspf.Focus();

				return;
			}

			if (!IBMiUtils.IsValueObjectName(modsrc.Text))
			{
				MessageBox.Show("Source SPF name invalid.");
				modsrc.Focus();

				return;
			}

			if (!IBMiUtils.IsValueObjectName(bndsrc.Text))
			{
				MessageBox.Show("Binder SPF name invalid.");
				bndsrc.Focus();

				return;
			}

			var result = MessageBox.Show("Generating this source will overwrite any existing source. Continue?",
				"Warning",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);

			if (result == DialogResult.Yes)
				CompileObjects();
		}

		private void GenerateRPGSource()
		{
			var IsTotalFree = false;
			Ref = new List<string>();
			Src = new List<string>();

			var W = IsTotalFree ? "" : "        ";

			if (IsTotalFree) // bug: expression is always false
			{
				Ref.Add("**FREE");
				Ref.Add("");

				Src.Add("**FREE");
				Src.Add("");
			}

			Src.Add(W + "Ctl-Opt NOMAIN;");
			Src.Add("");

			foreach (string Procedure in procedures.Items)
			{
				Ref.Add(W + "Dcl-Pr " + Procedure + ";");
				Ref.Add(W + "  //Parameters here");
				Ref.Add(W + "End-Pr;");
				Ref.Add("");

				Src.Add(W + "Dcl-Proc " + Procedure + " Export;");
				Src.Add(W + "  Dcl-Pi *N;");
				Src.Add(W + "  End-Pi;");
				Src.Add("");
				Src.Add(W + "  //Source code here");
				Src.Add(W + "  Return;");
				Src.Add(W + "End-Proc;");
				Src.Add("");
			}
		}

		private void GenerateBinderSource()
		{
			Binder = new List<string> {"STRPGMEXP PGMLVL(*CURRENT) SIGNATURE('" + srvpgmnam.Text + "')"};

			foreach (string procedure in procedures.Items)
				Binder.Add("  EXPORT SYMBOL('" + procedure.ToUpper() + "') ");

			Binder.Add("ENDPGMEXP");
		}

		private void CompileObjects()
		{
			GenerateRPGSource();
			GenerateBinderSource();

			var commands      = new List<string>();
			var referenceFile = IBMiUtils.GetLocalFile(srvpgmlib.Text, protspf.Text, srvpgmnam.Text);
			var moduleFile    = IBMiUtils.GetLocalFile(srvpgmlib.Text, modsrc.Text, srvpgmnam.Text);
			var binderFile    = IBMiUtils.GetLocalFile(srvpgmlib.Text, bndsrc.Text, srvpgmnam.Text);

			File.WriteAllLines(referenceFile, Ref);
			File.WriteAllLines(moduleFile, Src);
			File.WriteAllLines(binderFile, Binder);

			commands.Add("cd /QSYS.lib");

			commands.Add("put \"" +
			             referenceFile +
			             "\" \"" +
			             srvpgmlib.Text +
			             ".lib/" +
			             protspf.Text +
			             ".file/" +
			             srvpgmnam.Text +
			             ".mbr\"");

			commands.Add("put \"" +
			             moduleFile +
			             "\" \"" +
			             srvpgmlib.Text +
			             ".lib/" +
			             modsrc.Text +
			             ".file/" +
			             srvpgmnam.Text +
			             ".mbr\"");

			commands.Add("put \"" +
			             binderFile +
			             "\" \"" +
			             srvpgmlib.Text +
			             ".lib/" +
			             bndsrc.Text +
			             ".file/" +
			             srvpgmnam.Text +
			             ".mbr\"");

			commands.Add("CRTRPGMOD MODULE(" +
			             srvpgmlib.Text +
			             "/" +
			             srvpgmnam.Text +
			             ") SRCFILE(" +
			             srvpgmlib.Text +
			             "/" +
			             modsrc.Text +
			             ") OPTION(*EVENTF) DBGVIEW(*SOURCE)");

			commands.Add("CRTSRVPGM SRVPGM(" +
			             srvpgmlib.Text +
			             "/" +
			             srvpgmnam.Text +
			             ") MODULE(" +
			             srvpgmlib.Text +
			             "/" +
			             srvpgmnam.Text +
			             ") SRCFILE(" +
			             srvpgmlib.Text +
			             "/" +
			             bndsrc.Text +
			             ") SRCMBR(" +
			             srvpgmnam.Text +
			             ")");

			if (IBMi.RunCommands(commands.ToArray()) == false)
				MessageBox.Show(
					"Service program has been generated. Don't forget to add your service program to your binding directory. New source members will not have their associated types.");
			else
				MessageBox.Show("Failed to generate service program.");
		}
	}
}