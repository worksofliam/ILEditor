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
using System.Threading;
using System.Diagnostics;

namespace ILEditor.UserTools
{
    public partial class ProjectExplorer : UserControl
    {
        private string SelectedProject, SelectedFolder;

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
                if (ProjectDir.Trim() != "") 
                    if (Directory.Exists(ProjectDir))
                        Project.Projects.Add(new Project(ProjectDir));
                    else
                        MessageBox.Show("Tried loading '" + ProjectDir + "' but the directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                            //Load project settings
                            new ProjectSettings(value).ShowDialog();
                            break;
                        case "BLD":
                            //Build project
                            SelectedProject = value;
                            Program.LAST_BUILD = SelectedProject;

                            new BuildProject().Show();
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

        private void projView_MouseClick(object sender, MouseEventArgs e)
        {
            string tag = "", value = "";
            if (e.Button == MouseButtons.Right)
            {
                if (projView.SelectedNode != null)
                {
                    if (projView.SelectedNode.Parent == null)
                    {
                        SelectedProject = projView.SelectedNode.Text;
                        projRightClick.Show(Cursor.Position);
                    }
                    else if (projView.SelectedNode.Tag != null)
                    {
                        //Other node selected
                        tag = projView.SelectedNode.Tag as string;

                        if (tag != null)
                        {
                            value = tag.Substring(4);
                            if (tag.Substring(0, 3) == "FIL")
                            {
                                fileRightClick.Show(Cursor.Position);
                            }
                        }
                    }
                    else
                    {
                        switch (projView.SelectedNode.Text)
                        {
                            case "Headers":
                            case "Source":
                                SelectedProject = projView.SelectedNode.Parent.Text;
                                SelectedFolder = projView.SelectedNode.Text;
                                folderRightClick.Show(Cursor.Position);
                                break;
                        }
                    }
                }
            }
        }
        
        private void removeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to remove " + SelectedProject + "? This will not delete the project directory.", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

            if (result == DialogResult.Yes)
            {
                List<string> projects = IBMi.CurrentSystem.GetValue("PROJECTS").Split('|').ToList();
                projects.Remove(Project.GetProject(SelectedProject).GetDirectory());
                IBMi.CurrentSystem.SetValue("PROJECTS", String.Join("|", projects));
                ReloadProjects();
            }
        }
        
        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Project.GetProject(SelectedProject).GetDirectory());
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projView.SelectedNode != null)
            {
                projView.SelectedNode.BeginEdit();
            }
        }

        private static readonly string[] ValidExtentions = new[]
        {
            "SQLRPGLE", "RPGLE", "C", "SQLC", "CMD", "CLLE", "DSPF", "SQL", "DSPF", "H"
        };

        private void projView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
            if (e.Node.Tag != null)
            {
                if ((e.Node.Tag as string).Substring(0, 3) == "FIL") e.CancelEdit = false;
            }
        }

        private void projView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null) return;

            string[] NewName = e.Label.Split('.');

            e.CancelEdit = true;
            
            if (NewName.Length == 2)
            {
                if (ValidExtentions.Contains(NewName[1].ToUpper()))
                {
                    if (IBMiUtils.IsValueObjectName(NewName[1]))
                    {
                        bool FileExists = false;
                        
                        foreach (TreeNode node in e.Node.Parent.Nodes)
                        {
                            if (node.Text == e.Label.ToUpper())
                                FileExists = true;
                        }
                        
                        if (!FileExists)
                        {
                            string FileDir = "", currentFile = (e.Node.Tag as string).Substring(4);

                            FileDir = Path.GetDirectoryName(currentFile);
                            FileDir = Path.Combine(FileDir, e.Label);

                            File.Move(currentFile, FileDir);

                            e.Node.Tag = "FIL:" + FileDir;
                            e.CancelEdit = false;
                        }
                        else
                        {
                            MessageBox.Show(e.Label + " already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show(e.Label + " is not a valid name,", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //Error
                    }
                }
                else
                {
                    MessageBox.Show(e.Label + " is not a valid extention.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //Error
                }
            }
            else
            {
                MessageBox.Show("New name requires a name and extention.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Error
            }
        }
        
        private void viewCompileErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project SelectedProj = Project.GetProject(SelectedProject);
            string ModName = projView.SelectedNode.Text.Split('.')[0];
            Editor.TheEditor.AddTool("Error Listing", new ErrorListing(Project.GetBuildLibrary(), ModName), true);
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CrtDir = Path.Combine(Project.GetProject(SelectedProject).GetDirectory(), SelectedFolder, "NEWFILE.ext");
            File.Create(CrtDir).Close();

            TreeNode newNode = projView.SelectedNode.Nodes.Add("NEWFILE.ext");
            newNode.ImageIndex = 4; newNode.SelectedImageIndex = 4;
            newNode.Tag = "FIL:" + CrtDir;

            projView.SelectedNode.Expand();

            newNode.BeginEdit();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadProjects();
        }

        private void ImportProject(string ProjPath)
        {
            if (File.Exists(Path.Combine(ProjPath, "project.ileprj")))
            {
                List<string> projects = IBMi.CurrentSystem.GetValue("PROJECTS").Split('|').ToList();
                projects.Add(ProjPath);
                IBMi.CurrentSystem.SetValue("PROJECTS", String.Join("|", projects));
            }
            else
            {
                MessageBox.Show("Selected folder is not a valid ILEditor project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void importProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ImportProject(fbd.SelectedPath);
                    ReloadProjects();
                }
            }
        }

        private void importProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string ProjPath in Directory.GetDirectories(fbd.SelectedPath))
                        ImportProject(ProjPath);

                    ReloadProjects();
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projView.SelectedNode != null)
            {
                string FilePath = (projView.SelectedNode.Tag as string).Substring(4);
                DialogResult result = MessageBox.Show("Are you sure you want to delete " + Path.GetFileName(FilePath) + "?", "Delete file", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    File.Delete(FilePath);
                    projView.SelectedNode.Remove();
                }
            }
        }
    }
}
