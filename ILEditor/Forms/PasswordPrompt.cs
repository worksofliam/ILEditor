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
    public partial class PasswordPrompt : Form
    {
        public bool Success = false;
        public string GetResult() => this.password.Text;

        public PasswordPrompt(string System, string User)
        {
            InitializeComponent();
            userText.Text += " " + User + "@" + System;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void select_Click(object sender, EventArgs e)
        {
            if (password.Text != "")
            {
                Success = true;
                this.Close();
            }
        }
    }
}
