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

namespace ILEditor.Forms.ProjectWindows
{
    public partial class ProjectSettings : Form
    {
        Project SelectedProject;
        public ProjectSettings(string ProjectName)
        {
            InitializeComponent();

            this.Text = ProjectName + " Settings";

            SelectedProject = Project.GetProject(ProjectName);
            if (SelectedProject == null)
                this.Close();

            string[] PreBuildProjects = SelectedProject.GetLocalProjectDeps();

            projType.Text = SelectedProject.GetProjectType().ToString();

            commit.SelectedItem = SelectedProject.GetCommitmentControl();
            debugView.SelectedItem = SelectedProject.GetDebugView();

            //Load all projects are check them if they're deps
            foreach (Project proj in Project.Projects)
            {
                if (proj.GetName() == ProjectName) continue;

                projList.Items.Add(new ListViewItem(proj.GetName()) { Checked = PreBuildProjects.Contains(proj.GetName()) });
            }

            //Find if the list is missing any deps in the project list
            bool isFound = false;
            foreach (string Project in PreBuildProjects)
            {
                if (Project == "") continue;
                isFound = false;

                foreach (ListViewItem item in projList.Items)
                {
                    if (item.Text == Project)
                        isFound = true;
                }

                if (!isFound)
                {
                    MessageBox.Show(Project + " is a dependancy to " + ProjectName + " but is missing from your project list. Please import this project, otherwise you will not be able to make changes to this project dependancy list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    projList.Enabled = false;
                }
            }

            foreach (string Module in SelectedProject.GetStaticModules())
            {
                if (Module == "") continue;

                modList.Items.Add(Module);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (modList.SelectedItem != null)
                {
                    modList.Items.Remove(modList.SelectedItem);
                }
            }
        }

        private void addmod_Click(object sender, EventArgs e)
        {
            modPath.Text = modPath.Text.Trim();
            if (modPath.Text != "")
            {
                if (!modList.Items.Contains(modPath.Text))
                {
                    modList.Items.Add(modPath.Text);
                    modPath.Clear();
                }
            }
        }

        private void modPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addmod.PerformClick();
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            List<string> ProjectDeps = new List<string>();
            List<string> ModuleDeps = new List<string>();

            foreach (ListViewItem item in projList.Items)
                if (item.Checked)
                    ProjectDeps.Add(item.Text);

            foreach (string item in modList.Items)
                ModuleDeps.Add(item);

            SelectedProject.SetCommitmentControl(commit.SelectedItem.ToString());
            SelectedProject.SetDebugView(debugView.SelectedItem.ToString());

            if (projList.Enabled)
                SelectedProject.SetLocalProjectDeps(ProjectDeps.ToArray());
            SelectedProject.SetStaticModules(ModuleDeps.ToArray());

            this.Close();
        }
    }
}
