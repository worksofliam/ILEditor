using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Classes;
using System.IO;

namespace ILEditor.Forms.ProjectWindows
{
    public partial class NewProjectWindow : Form
    {
        public NewProjectWindow()
        {
            InitializeComponent();
            projDir.Text = Program.PROJDIR;
        }

        private void findDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    projDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            Project.InitLang lang = Project.InitLang.None;
            Project.Type projType = Project.Type.PGM;

            if (objName.Text.Trim() == "")
            {
                MessageBox.Show("Object name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ile_pgm.Checked)
                projType = Project.Type.PGM;
            else if (ile_static.Checked)
                projType = Project.Type.MOD;
            else if (ile_dynamic.Checked)
                projType = Project.Type.SRVPGM;

            if (lang_rpg.Checked)
                lang = Project.InitLang.RPG;
            else if (lang_c.Checked)
                lang = Project.InitLang.C;

            if (projName.Text.Trim() == "")
            {
                MessageBox.Show("Project name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(projDir.Text))
            {
                MessageBox.Show("Selected directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ProjectDirectory = Path.Combine(projDir.Text, projName.Text);
            if (Directory.Exists(ProjectDirectory))
            {
                MessageBox.Show("Folder '" + projName.Text + "' already exists in the chosen directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Project newProject = new Project(ProjectDirectory, objName.Text, projType);
            newProject.Init(lang);

            //Add it to current project lists
            Project.Projects.Add(newProject);

            //Add it the system projects list
            List<string> projects = IBMi.CurrentSystem.GetValue("PROJECTS").Split('|').ToList();
            projects.Add(ProjectDirectory);
            IBMi.CurrentSystem.SetValue("PROJECTS", String.Join("|", projects));

            this.Close();
        }
    }
}
