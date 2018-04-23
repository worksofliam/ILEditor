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
    }
}
