using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class ServiceGenerator : Form
    {
        public ServiceGenerator()
        {
            InitializeComponent();
        }

        private void addproc_Click(object sender, EventArgs e)
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
            {
                procedures.Items.RemoveAt(procedures.SelectedIndex);
            }
        }

        private void procname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addproc.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void generate_Click(object sender, EventArgs e)
        {
            string Library = srvpgmlib.Text.Trim();
            if (Library == "*CURLIB") Library = IBMi.CurrentSystem.GetValue("curlib");

            if (!IBMiUtils.IsValueObjectName(Library))
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

            DialogResult result = MessageBox.Show("Generating this source will overwrite any existing source. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                CompileObjects();
            }
        }

        private List<string> Ref;
        private List<string> Src;
        private List<string> Binder;

        private void GenerateRPGSource()
        {
            Boolean IsTotalFree = false;
            Ref = new List<string>();
            Src = new List<string>();

            string W = (IsTotalFree ? "" : "        ");

            if (IsTotalFree)
            {
                Ref.Add("**FREE");
                Ref.Add("");

                Src.Add("**FREE");
                Src.Add("");
            }

            Src.Add(W + "Ctl-Opt NOMAIN;");
            Src.Add("");

            foreach (String Procedure in procedures.Items)
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
            Binder = new List<string>();

            Binder.Add("STRPGMEXP PGMLVL(*CURRENT) SIGNATURE('" + srvpgmnam.Text + "')");
            foreach (String Procedure in procedures.Items)
            {
                Binder.Add("  EXPORT SYMBOL('" + Procedure.ToUpper() + "') ");
            }
            Binder.Add("ENDPGMEXP");
        }

        private void CompileObjects()
        {
            GenerateRPGSource();
            GenerateBinderSource();

            List<string> Commands = new List<string>();
            string ReferenceFile = IBMiUtils.GetLocalFile(srvpgmlib.Text, protspf.Text, srvpgmnam.Text);
            string ModuleFile = IBMiUtils.GetLocalFile(srvpgmlib.Text, modsrc.Text, srvpgmnam.Text);
            string BinderFile = IBMiUtils.GetLocalFile(srvpgmlib.Text, bndsrc.Text, srvpgmnam.Text);

            File.WriteAllLines(ReferenceFile, Ref);
            File.WriteAllLines(ModuleFile, Src);
            File.WriteAllLines(BinderFile, Binder);
            
            Commands.Add("cd /QSYS.lib");

            Commands.Add("put \"" + ReferenceFile + "\" \"" + srvpgmlib.Text + ".lib/" + protspf.Text + ".file/" + srvpgmnam.Text + ".mbr\"");
            Commands.Add("put \"" + ModuleFile + "\" \"" + srvpgmlib.Text + ".lib/" + modsrc.Text + ".file/" + srvpgmnam.Text + ".mbr\"");
            Commands.Add("put \"" + BinderFile + "\" \"" + srvpgmlib.Text + ".lib/" + bndsrc.Text + ".file/" + srvpgmnam.Text + ".mbr\"");

            Commands.Add("CRTRPGMOD MODULE(" + srvpgmlib.Text + "/" + srvpgmnam.Text + ") SRCFILE(" + srvpgmlib.Text + "/" + modsrc.Text + ") OPTION(*EVENTF) DBGVIEW(*SOURCE)");
            Commands.Add("CRTSRVPGM SRVPGM(" + srvpgmlib.Text + "/" + srvpgmnam.Text + ") MODULE(" + srvpgmlib.Text + "/" + srvpgmnam.Text + ") SRCFILE(" + srvpgmlib.Text + "/" + bndsrc.Text + ") SRCMBR(" + srvpgmnam.Text + ")");

            if (IBMi.RunCommands(Commands.ToArray()) == false)
            {
                MessageBox.Show("Service program has been generated. Don't forget to add your service program to your binding directory. New source members will not have their associated types.");
            }
            else
            {
                MessageBox.Show("Failed to generate service program.");
            }
        }
    }
}
