using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Classes;
using System.Threading;

namespace ILEditor.UserTools
{
    public partial class MemberSearchListing : UserControl
    {
        private string Library;
        private string SPF;
        private string SearchValue;
        private Boolean Sensitive;

        public MemberSearchListing(string Lib, string Spf, string Value, Boolean CaseSensitive = false)
        {
            InitializeComponent();
            Library = Lib;
            SPF = Spf;
            Sensitive = CaseSensitive;

            if (Sensitive)
                SearchValue = Value;
            else
                SearchValue = Value.ToUpper();

            StartSearch();
        }

        private void StartSearch()
        {
            new Thread((ThreadStart)delegate
            {
                List<TreeNode> TreeOut = new List<TreeNode>();
                TreeNode CurrentFile, currentResult;
                int currentLine = 0;
                Boolean Contains = false;
                string Name, Extension;
                string[] Members = Directory.GetFiles(IBMiUtils.GetLocalDir(Library, SPF));

                if (Members.Length == 0)
                {
                    TreeOut.Add(new TreeNode("No local members for " + Library + "/" + SPF, 0, 0));
                }
                else
                {
                    foreach (string Member in Members)
                    {
                        Name = Path.GetFileNameWithoutExtension(Path.GetFileName(Member)).ToUpper();
                        Extension = Path.GetExtension(Member).Substring(1).ToUpper();
                        CurrentFile = new TreeNode(Name, 2, 2); currentLine = 1;
                        foreach (string Line in File.ReadAllLines(Member))
                        {
                            Contains = false;
                            if (Sensitive)
                                Contains = Line.Contains(SearchValue);
                            else
                                Contains = Line.ToUpper().Contains(SearchValue);

                            if (Contains)
                            {
                                currentResult = new TreeNode("Line " + currentLine.ToString(), 3, 3);
                                currentResult.Tag = new RemoteSource(Member, Library, SPF, Name, Extension);
                                CurrentFile.Nodes.Add(currentResult);
                            }

                            currentLine++;
                        }

                        if (CurrentFile.Nodes.Count > 0)
                            TreeOut.Add(CurrentFile);
                    }

                    if (TreeOut.Count == 0)
                    {
                        TreeOut.Add(new TreeNode("No results found for '" + SearchValue + "' in " + Library + "/" + SPF, 0, 0));
                    }
                    else
                    {
                        TreeOut.Insert(0, new TreeNode("Results for '" + SearchValue + "' in " + Library + "/" + SPF, 1, 1));
                    }
                }

                this.Invoke((MethodInvoker)delegate
                {
                    searchResults.Nodes.Clear();
                    searchResults.Nodes.AddRange(TreeOut.ToArray());
                });

            }).Start();
        }

        private void searchResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null) { }
            else
            {
                if (e.Node.Tag is RemoteSource)
                {
                    RemoteSource member = (RemoteSource)e.Node.Tag;
                    Editor.OpenSource(member);
                }
            }
        }
    }
}
