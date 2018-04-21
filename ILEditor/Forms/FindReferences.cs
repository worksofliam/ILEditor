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
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace ILEditor.Forms
{
    public partial class FindReferences : Form
    {
        public FindReferences()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fetch_Click(object sender, EventArgs e)
        {
            if (IBMiUtils.IsValueObjectName(lib.Text) && IBMiUtils.IsValueObjectName(pgm.Text))
            {
                List<ILEObject[]> Objects = IBMiUtils.GetProgramReferences(lib.Text, pgm.Text);

                string json = JsonConvert.SerializeObject(
                    Objects,
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                );

                string file = IBMiUtils.GetLocalFile("QTEMP", "Diagram", lib.Text, "html");
                string html = Properties.Resources.diagram.Replace("!JSONHERE!", json);

                File.WriteAllText(file, html);
                Process.Start(file);

                this.Close();
            }
            else
            {
                //TODO: Error message
                MessageBox.Show("Unable to create Object Diagram.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
