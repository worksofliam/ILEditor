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
    public partial class CreateDirectory : Form
    {
        public CreateDirectory(string Path = "")
        {
            InitializeComponent();

            path.Text = Path;
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (path.Text == "")
            {
                MessageBox.Show("Path cannot be blank");
            }
            else
            {
                if (!IBMi.DirExists(path.Text))
                {
                    IBMi.CreateDirecory(path.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Chosen path already exists.");
                }
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
