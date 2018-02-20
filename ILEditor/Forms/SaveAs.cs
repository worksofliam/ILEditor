using ILEditor.Classes;
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
    public partial class SaveAs : Form
    {
        public bool Success = false;
        public SourceSelectBox SourceInfo() => sourceSelectBox;
        public SaveAs(RemoteSource MemberInfo = null)
        {
            InitializeComponent();

            if (MemberInfo != null)
            {
                switch (MemberInfo.GetFS())
                {
                    case FileSystem.IFS:
                        sourceSelectBox.SetSource(MemberInfo.GetRemoteFile());
                        sourceSelectBox.SetSource("", "", MemberInfo.GetName());
                        break;
                    case FileSystem.QSYS:
                        sourceSelectBox.SetSource("", MemberInfo.GetObject(), MemberInfo.GetName());
                        break;
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (!sourceSelectBox.isValid())
            {
                MessageBox.Show("Source information not valid.");
            }
            else
            {
                this.Success = true;
                this.Close();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
