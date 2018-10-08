using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class CreateStreamFile : Form
    {
        public RemoteSource result = null;
        public CreateStreamFile(string PrePath = "")
        {
            InitializeComponent();

            stmfPath.Text = PrePath;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void open_Click(object sender, EventArgs e)
        {
            stmfPath.Text = stmfPath.Text.Trim();

            if (stmfPath.Text == "")
            {
                MessageBox.Show("Path cannot be blank");
            }
            else
            {
                if (!IBMi.FileExists(stmfPath.Text))
                {
                    string filetemp = Path.Combine(IBMiUtils.GetLocalDir("IFS"), Path.GetFileName(stmfPath.Text));
                    File.Create(filetemp).Close();
                    result = new RemoteSource(filetemp, stmfPath.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Chosen path already exists.");
                }
            }
        }
    }
}
