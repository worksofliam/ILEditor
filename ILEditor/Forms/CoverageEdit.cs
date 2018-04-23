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
    public partial class CoverageEdit : Form
    {
        private CoverageTest Test;
        public CoverageEdit(CoverageTest Test = null)
        {
            InitializeComponent();

            if (Test != null)
            {
                covcmd.Text = Test.Command;

                foreach(ILEObject module in Test.Modules)
                    customModules.Items.Add(module.Library + '/' + module.Name + ' ' + module.Type);
            }
        }

        public CoverageTest GetTest() => this.Test;

        #region Modules
        private void addMod_Click(object sender, EventArgs e)
        {
            string module = customModule.Text.Trim();
            string type = (moduleType.SelectedItem != null ? moduleType.SelectedItem.ToString() : "");
            string value = "";
            if (module != "" && type != "")
            {
                if (!module.Contains("/"))
                    module = "*LIBL/" + module;

                value = module + ' ' + type;
                if (!customModules.Items.Contains(value))
                    customModules.Items.Add(value);

                customModule.Text = "";
                customModule.Focus();
            }
        }

        private void customModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addMod.PerformClick();
        }

        private void delMod_Click(object sender, EventArgs e)
        {
            if (customModules.SelectedItem != null)
                customModules.Items.RemoveAt(customModules.SelectedIndex);
        }

        private void customModules_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                delMod.PerformClick();
        }
        #endregion

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string[] data, path;

            this.Test = new CoverageTest(covname.Text);
            this.Test.Command = covcmd.Text;
            
            foreach(String item in customModules.Items)
            {
                data = item.Split(' ');
                path = data[0].Split('/');
                this.Test.Modules.Add(new ILEObject(path[0], path[1], data[1]));
            }

            CoverageTest.Tests[covname.Text] = this.Test;

            this.Close();
        }
    }
}
