using ILEditor.Classes;
using ILEditor.UserTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class OpenMember : Form
    {
        public OpenMember()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;

            if (!IBMiUtils.IsValueObjectName(lib.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(spf.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(mbr.Text))
                isValid = false;

            if (isValid)
            {
                Editor.OpenMember(new Member("", lib.Text, spf.Text, mbr.Text, type.Text, true));
                this.Close();
            }
            else
                MessageBox.Show("Member information not valid.");
        }
    }
}
