using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class FileSelect : Form
    {
        public bool Success = false;
        private string Command = null;

        public FileSelect()
        {
            InitializeComponent();
        }
        
        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void select_Click(object sender, EventArgs e)
        {
            Boolean valid = true;
            string Lib = lib.Text.Trim();
            string Obj = obj.Text.Trim();
            string Type = sqlType.Text.Trim().ToUpper();
            List<string> Options = new List<string>();

            if (!IBMiUtils.IsValueObjectName(Lib))
                valid = false;

            if (!IBMiUtils.IsValueObjectName(Lib))
                valid = false;
            
            if (!valid)
            {
                MessageBox.Show("Member information not valid.");
            }
            else
            {
                if (Type == "VIEW")
                    Options.Add("index_instead_of_view_option => ''1''");

                Options.Add("REPLACE_OPTION => ''1''");

                this.Command = "RUNSQL SQL('CALL QSYS2.GENERATE_SQL(''" + Obj + "'', ''" + Lib + "'', ''" + Type + "'', " + String.Join(", ", Options) + ")')";
                this.Success = true;
                this.Close();
            }
        }

        public string getCommand()
        {
            return this.Command;
        }
    }
}
