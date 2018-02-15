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
            List<string> items = IBMi.CurrentSystem.GetValue("CMPTYPES").Split('|').ToList();
            type.Text = type.Text.Trim();
            if (type.Text.Trim() != "")
            {
                if (!items.Contains(type.Text))
                {
                    items.Add(type.Text);
                    IBMi.CurrentSystem.SetValue("CMPTYPES", String.Join("|", items));
                    this.Close();
                }
            }
        }
    }
}
