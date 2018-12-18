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

namespace ILEditor.Forms
{
    public partial class QuickFileSearch : Form
    {
        public QuickFileSearch()
        {
            InitializeComponent();
            nameValue.Focus();
        }

        private void fileValue_TextChanged(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                InitSearch(nameValue.Text);
            }).Start();
        }

        private void InitSearch(string Value)
        {
            string[] results = FileCache.Find(Value);
            
            this.Invoke((MethodInvoker)delegate
            {
                fileList.Items.Clear();
                fileList.Items.AddRange(results);
            });
        }

        private void QuickFileSearch_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fileValue_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (fileList.Items.Count > 0)
                    {
                        SelectFile(fileList.Items[0].ToString());
                    }
                    break;
                case Keys.Down:
                    fileList.Focus();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void fileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (fileList.SelectedItem != null)
            {
                SelectFile(fileList.SelectedItem.ToString());
            }
        }

        private void fileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (fileList.SelectedItem != null)
                {
                    SelectFile(fileList.SelectedItem.ToString());
                }
            }
        }

        private void SelectFile(string SourcePath)
        {
            if (SourcePath != "")
            {
                RemoteSource SourceFile;
                if (SourcePath.StartsWith("/"))
                {
                    SourceFile = new RemoteSource("", SourcePath);
                }
                else
                {
                    string type = FileCache.GetType(SourcePath);
                    string[] data = SourcePath.Split(new char[] { '/', '.' }, StringSplitOptions.RemoveEmptyEntries);

                    SourceFile = new RemoteSource("", data[0], data[1], data[2], type);
                }

                Editor.OpenSource(SourceFile);

                this.Close();
            }
        }
    }
}
