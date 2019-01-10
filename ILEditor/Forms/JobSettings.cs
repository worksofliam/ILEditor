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

namespace ILEditor.Forms
{
    public partial class JobSettings : Form
    {
        public JobSettings()
        {
            InitializeComponent();
        }

        private void libraryList_Load(object sender, EventArgs e)
        {
            this.Text = "Library List for " + IBMi.CurrentSystem.GetValue("system");

            string[] libl = IBMi.CurrentSystem.GetValue("datalibl").Split(',');
            foreach (string lib in libl)
            {
                libraryListBox.Items.Add(lib);
            }

            curlib.Text = IBMi.CurrentSystem.GetValue("curlib");

            homeDir.Text = IBMi.CurrentSystem.GetValue("homeDir");
            buildLib.Text = IBMi.CurrentSystem.GetValue("buildLib");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            label2.Update();

            //Add a default library
            if (libraryListBox.Items.Count == 0)
                libraryListBox.Items.Add("SYSTOOLS");

            string s = "";
            foreach (string item in libraryListBox.Items)
            {
                if (IBMiUtils.IsValueObjectName(item.Trim()))
                {
                    s += item.Trim() + ',';
                }
                else
                {
                    label2.Text = "Invalid library: " + item.Trim();
                    label2.Update();
                    return;
                }
            }

            if (!IBMiUtils.IsValueObjectName(curlib.Text.Trim()))
            {
                label2.Text = "Invalid current library.";
                label2.Update();
                return;
            }

            string origLibl = IBMi.CurrentSystem.GetValue("datalibl");
            string origCur = IBMi.CurrentSystem.GetValue("curlib");

            IBMi.CurrentSystem.SetValue("datalibl", s.Remove(s.Length - 1, 1)); //Remove last comma
            IBMi.CurrentSystem.SetValue("curlib", curlib.Text.Trim()); //Remove last comma

            IBMi.CurrentSystem.SetValue("homeDir", homeDir.Text);
            IBMi.CurrentSystem.SetValue("buildLib", buildLib.Text);

            Boolean Success = IBMi.RemoteCommand($"CHGLIBL LIBL({ IBMi.CurrentSystem.GetValue("datalibl").Replace(',', ' ')}) CURLIB({ IBMi.CurrentSystem.GetValue("curlib") })");

            if (!Success)
            {
                IBMi.CurrentSystem.SetValue("datalibl", origLibl);
                IBMi.CurrentSystem.SetValue("curlib", origCur);

                MessageBox.Show("Library list contains invalid libraries.", "Library list", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (newLibrary.Text != "")
            {
                if (!libraryListBox.Items.Contains(newLibrary.Text))
                {
                    libraryListBox.Items.Add(newLibrary.Text);
                    newLibrary.Text = "";
                }
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int index = libraryListBox.SelectedIndex;
            if (index >= 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        libraryListBox.Items.RemoveAt(index);
                        break;
                    case Keys.Down:
                        MoveItem(1);
                        break;
                    case Keys.Up:
                        MoveItem(-1);
                        break;
                }
            }
        }

        public void MoveItem(int direction)
        {
            // Checking selected item
            if (libraryListBox.SelectedItem == null || libraryListBox.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = libraryListBox.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= libraryListBox.Items.Count)
                return; // Index out of range - nothing to do

            object selected = libraryListBox.SelectedItem;

            // Removing removable element
            libraryListBox.Items.Remove(selected);
            // Insert it in new position
            libraryListBox.Items.Insert(newIndex, selected);
            // Restore selection
            if (direction == 1) libraryListBox.SetSelected(newIndex - 1, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = libraryListBox.SelectedIndex;
            if (index >= 0)
                libraryListBox.Items.RemoveAt(index);
        }
    }
}
