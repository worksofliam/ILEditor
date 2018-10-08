using ILEditor.Classes;
using ILEditor.UserTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class SourceCompareSelect : Form
    {
        public SourceCompareSelect()
        {
            InitializeComponent();

            if (Editor.LastEditing != null)
            {
                RemoteSource src = Editor.LastEditing.Tag as RemoteSource;

                if (src != null)
                {
                    switch (src.GetFS())
                    {
                        case FileSystem.QSYS:
                            newSourceBox.SetSource(src.GetLibrary(), src.GetObject(), src.GetName());
                            oldSourceBox.SetSource("", src.GetObject(), src.GetName());
                            break;
                        case FileSystem.IFS:
                            newSourceBox.SetSource(src.GetRemoteFile());
                            newSourceBox.SetTab(src.GetFS());
                            break;
                    }
                }
            }
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            if (!newSourceBox.isValid())
            {
                MessageBox.Show("New source information not valid.");
                return;
            }

            if (!oldSourceBox.isValid())
            {
                MessageBox.Show("Old source information not valid.");
                return;
            }

            string NewFile = "", OldFile = "";

            switch (newSourceBox.GetFS())
            {
                case FileSystem.IFS:
                    NewFile = IBMiUtils.DownloadFile(newSourceBox.GetIFSPath());
                    break;
                case Classes.FileSystem.QSYS:
                    NewFile = IBMiUtils.DownloadMember(newSourceBox.GetLibrary(), newSourceBox.GetSPF(), newSourceBox.GetMember());
                    break;
            }

            switch (oldSourceBox.GetFS())
            {
                case FileSystem.IFS:
                    OldFile = IBMiUtils.DownloadFile(oldSourceBox.GetIFSPath());
                    break;
                case Classes.FileSystem.QSYS:
                    OldFile = IBMiUtils.DownloadMember(oldSourceBox.GetLibrary(), oldSourceBox.GetSPF(), oldSourceBox.GetMember());
                    break;
            }

            if (NewFile == "" || OldFile == "")
            {
                MessageBox.Show("Unable to download members.");
                return;
            }

            Editor.TheEditor.AddTool(new DiffView(NewFile, OldFile));
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
