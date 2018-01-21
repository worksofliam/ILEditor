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
                ProjectNode = new TreeNode(Proj.GetName(), 0, 0);
                ProjectNode.Nodes.Add(new TreeNode("Project Settings", 1, 1) { Tag = "EDT:" + Proj.GetName() });
                ProjectNode.Nodes.Add(new TreeNode("Project Build", 2, 2) { Tag = "BLD:" + Proj.GetName() });

                HeadersNode = new TreeNode("Headers", 3, 3);
                foreach (string FilePath in Proj.GetHeaderFiles())
                    HeadersNode.Nodes.Add(new TreeNode(Path.GetFileName(FilePath), 4, 4) { Tag = "FIL:" + FilePath });

                SourceNode = new TreeNode("Source", 3, 3);
                foreach (string FilePath in Proj.GetSourceFiles())
                    SourceNode.Nodes.Add(new TreeNode(Path.GetFileName(FilePath), 4, 4) { Tag = "FIL:" + FilePath });

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

        private void projView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string tag = "", value = "";
            if (projView.SelectedNode != null)
            {
                tag = projView.SelectedNode.Tag as string;

                if (tag != null)
                {
                    value = tag.Substring(4);
                    switch (tag.Substring(0, 3))
                    {
                        case "EDT":
                            //Load edit screen
                            break;
                        case "BLD":
                            //Build project
                            break;
                        case "FIL":
                            //File
                            Editor.OpenLocal(value);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
