using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms.ProjectWindows
{
    public partial class BuildProject : Form
    {
        public BuildProject()
        {
            InitializeComponent();

            foreach (Project proj in Project.Projects)
                projList.Items.Add(proj.GetName());

            projList.SelectedItem = Program.LAST_BUILD;
        }

        private void build_Click(object sender, EventArgs e)
        {
            if (projList.SelectedItem != null)
            {
                string SelectedProject = projList.SelectedItem.ToString();
                Program.LAST_BUILD = SelectedProject;

                if (IBMi.IsConnected())
                {
                    Editor.TheEditor.SetStatus("Starting build for " + SelectedProject);
                    new Thread((ThreadStart)delegate
                    {
                        Project.PreProjectBuild(); //Clears messages
                        Project.GetProject(SelectedProject).Build();

                        this.Invoke((MethodInvoker)delegate
                        {
                            Editor.TheEditor.AddTool(SelectedProject + " build", new BuildResult(Project.GetBuildMessages()), true);
                        });
                    }).Start();
                }
                else
                {
                    MessageBox.Show("Cannot build project while in offline mode.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            this.Close();
        }
    }
}
