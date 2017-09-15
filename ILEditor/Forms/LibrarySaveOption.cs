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
    public partial class LibrarySaveOption : Form
    {
        public LibrarySaveOption()
        {
            InitializeComponent();
            string[] items = IBMi.CurrentSystem.GetValue("LIBSAVE").Split('|');
            string lib;
            foreach(string item in items)
            {
                if (item == "") continue;
                lib = IBMi.CurrentSystem.GetValue("LIB_" + item);
                localliblist.Rows.Add(new object[2] { item, lib });
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAndCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> keys = new List<string>();
            string name;
            foreach (DataGridViewRow row in localliblist.Rows)
            {
                if (!row.IsNewRow)
                {
                    name = row.Cells[0].Value.ToString().ToUpper();
                    keys.Add(name);
                    IBMi.CurrentSystem.SetValue("LIB_" + name, row.Cells[1].Value.ToString().ToUpper());
                }
            }

            IBMi.CurrentSystem.SetValue("LIBSAVE", String.Join("|", keys));
            this.Close();
        }
    }
}
