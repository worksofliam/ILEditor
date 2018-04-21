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
                //TODO: Convert to JSON
                string json = JsonConvert.SerializeObject(
                    Objects, 
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                );

                //TODO
            }
            else
            {
                //TODO: Error message
            }
        }
    }
}
