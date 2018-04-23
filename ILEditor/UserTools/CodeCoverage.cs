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
                tests.Items.Add(Test);
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
    }
}
