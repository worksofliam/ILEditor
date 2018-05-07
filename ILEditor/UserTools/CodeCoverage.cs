using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ILEditor.Classes;

namespace ILEditor.UserTools
{
    public partial class CodeCoverage : DockContent
    {
        public CodeCoverage()
        {
            InitializeComponent();
            ReloadTests();
        }

        private void newcctest_Click(object sender, EventArgs e)
        {
            new Forms.CoverageEdit().ShowDialog();
            ReloadTests();
        }

        public void ReloadTests()
        {
            tests.Items.Clear();
            foreach (string Test in CoverageTest.Tests.Keys)
                tests.Items.Add(new ListViewItem(Test, 0));
        }

        private void tests_MouseClick(object sender, MouseEventArgs e)
        {
            if (tests.SelectedItems.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    rightClickTest.Show(Cursor.Position);
                }
            }
        }

        private void deleteTest_Click(object sender, EventArgs e)
        {
            if (tests.SelectedItems.Count > 0)
            {
                CoverageTest.Tests.Remove(tests.SelectedItems[0].Text);
                ReloadTests();
            }
        }

        private void editTest_Click(object sender, EventArgs e)
        {
            if (tests.SelectedItems.Count > 0)
            {
                new Forms.CoverageEdit(tests.SelectedItems[0].Text, CoverageTest.Tests[tests.SelectedItems[0].Text]).ShowDialog();
                ReloadTests();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Name = "", LocalFile = "";
            if (tests.SelectedItems.Count > 0)
            {
                Name = tests.SelectedItems[0].Text;

                Editor.TheEditor.SetStatus("Starting Code Coverage Test.");
                LocalFile = CoverageTest.Tests[Name].Run();
                Editor.TheEditor.SetStatus("Finished Code Coverage Test.");

                if (LocalFile == "")
                {
                    MessageBox.Show("There was an issue running a coverage test. Make sure QDEVTOOLS is on the library list and that the command is valid.", "Coverage Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
        }
    }
}
