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
        public TaskList()
        {
            InitializeComponent();
        }

        public void Display(TaskItem[] Items)
        {
            tasks.Items.Clear();
            string Keyword;

            foreach (TaskItem Item in Items)
            {
                Keyword = Item.Text.Substring(0, 4);
                tasks.Items.Add(new ListViewItem(new[] { Item.Text, Item.Line.ToString() }, Array.IndexOf(Program.TaskKeywords, Keyword)));
            }
        }
    }
}
