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
            type.Text = type.Text.Trim();
            if (type.Text.Trim() != "")
            {
                if (!IBMi.CurrentSystem.GetValue("CMPTYPES").Contains(type.Text))
                {
                    IBMi.CurrentSystem.SetValue("CMPTYPES", IBMi.CurrentSystem.GetValue("CMPTYPES") + "|" + type.Text);
                    this.Close();
                }
            }
        }
    }
}
