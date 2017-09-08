using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
    public partial class LicenceKey : Form
    {
        public LicenceKey()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Licence.IsValid(key.Text))
            {
                MessageBox.Show("Thanks for authenticating!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Not valid!");
            }
        }
    }
}
