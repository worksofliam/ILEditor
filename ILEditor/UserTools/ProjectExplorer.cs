using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Forms.ProjectWindows;
using ILEditor.Classes;
using System.IO;

namespace ILEditor.UserTools
{
    public partial class ProjectExplorer : UserControl
    {
        public ProjectExplorer()
        {
            InitializeComponent();
            ReloadProjects();
        }

        public void ReloadProjects()
        {
            TreeNode ProjectNode, HeadersNode, SourceNode;
            string[] ProjectDirectories = IBMi.CurrentSystem.GetValue("PROJECTS").Split('|');

            Project.Projects.Clear();

            foreach (string ProjectDir in ProjectDirectories)
                if (ProjectDir.Trim() != "") //ALSO CHECK IF DIR EXISTS
                    Project.Projects.Add(new Project(ProjectDir));

            projView.Nodes.Clear();
                
            foreach (Project Proj in Project.Projects)
            {
                ProjectNode = new TreeNode(Proj.GetName());
                ProjectNode.Nodes.Add(new TreeNode("Project Settings"));
                ProjectNode.Nodes.Add(new TreeNode("Project Build"));

                HeadersNode = new TreeNode("Headers");
                foreach (string FilePath in Proj.GetHeaderFiles())
                    HeadersNode.Nodes.Add(Path.GetFileName(FilePath));

                SourceNode = new TreeNode("Source");
                foreach (string FilePath in Proj.GetSourceFiles())
                    SourceNode.Nodes.Add(Path.GetFileName(FilePath));

                ProjectNode.Nodes.Add(HeadersNode);
                ProjectNode.Nodes.Add(SourceNode);

                projView.Nodes.Add(ProjectNode);
            }
        }

        private void newProject_ButtonClick(object sender, EventArgs e)
        {
            new NewProjectWindow().ShowDialog();
            ReloadProjects();
        }

    }
}
