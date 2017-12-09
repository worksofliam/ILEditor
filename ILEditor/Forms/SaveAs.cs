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
    public partial class SaveAs : Form
    {
        public bool Success = false;
        public string Lib = "";
        public string Spf = "";
        public string Mbr = "";
        public SaveAs()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            Boolean valid = true;
            this.Lib = lib.Text.Trim();
            this.Spf = spf.Text.Trim();
            this.Mbr = mbr.Text.Trim();

            if (!IBMiUtils.IsValueObjectName(this.Lib))
                valid = false;

            if (!IBMiUtils.IsValueObjectName(this.Spf))
                valid = false;

            if (!IBMiUtils.IsValueObjectName(this.Mbr))
                valid = false;

            if (!valid)
            {
                MessageBox.Show("Member information not valid.");
            }
            else
            {
                this.Success = true;
                this.Close();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
