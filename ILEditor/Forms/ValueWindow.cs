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
    public partial class ValueWindow : Form
    {
        public Boolean Successful;
        public string Value;
        public ValueWindow(string Title, string Desc, int MaxLength)
        {
            InitializeComponent();
            Successful = false;
            this.Text = Title;
            label1.Text = Desc;
            value.MaxLength = MaxLength;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Value = value.Text;
            Successful = true;
            this.Close();
        }
    }
}
