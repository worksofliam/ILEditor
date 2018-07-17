using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.UserTools
{
    public partial class TaskList : DockContent
    {
        private string FileName = "";
        public TaskList()
        {
            InitializeComponent();
        }
        
        public void Display(string Name, TaskItem[] Items)
        {
            this.FileName = Name;
            tasks.Items.Clear();
            string Keyword;

            foreach (TaskItem Item in Items)
            {
                Keyword = Item.Text.Substring(0, 4);
                tasks.Items.Add(new ListViewItem(new[] { Item.Text, Item.Line.ToString() }, Array.IndexOf(Program.TaskKeywords, Keyword)));
            }
        }

        private void SelectTask(string File, int Line)
        {
            DockContent theTab = Editor.TheEditor.GetTabByName(File, true);

            if (theTab != null)
            {
                SourceEditor SourceEditor = Editor.TheEditor.GetTabEditor(theTab);

                SourceEditor.Focus();
                SourceEditor.GotoLine(Line, 0);
            }
            else
            {
                MessageBox.Show("Unable to open location. Please open the source manually first and then try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tasks.SelectedItems.Count > 0)
            {
                SelectTask(this.FileName, int.Parse(tasks.SelectedItems[0].SubItems[1].Text) - 1);
            }
        }
    }
}
