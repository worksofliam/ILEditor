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
            string Type = sqlType.Text.Trim();

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
                this.Command = "QUOTE RCMD RUNSQL SQL('CALL QSYS2.GENERATE_SQL(''" + Obj + "'', ''" + Lib + "'', ''" + Type + "'', REPLACE_OPTION => ''0'')')";
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
