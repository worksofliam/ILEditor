using ILEditor.Classes;
using ILEditor.UserTools;
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
    public partial class OpenMember : Form
    {
        public OpenMember()
        {
            InitializeComponent();
        }

        public Boolean OpenMember_(string Lib, string Obj, string Mbr, string Ext, Boolean Editing)
        {
            Boolean result = false;
            string TabText = Lib + "/" + Obj + "(" + Mbr + ")";
            int TabIndex = Editor.TheEditor.EditorContains(TabText);
            if (Editor.TheEditor.EditorContains(TabText) == -1)
            {
                string resultFile = IBMiUtils.DownloadMember(Lib, Obj, Mbr);

                if (resultFile != "")
                {
                    Editor.TheEditor.AddMemberEditor(new Member(resultFile, Lib, Obj, Mbr, Ext, Editing), MemberBrowse.GetBoundLangType(Ext));
                    result = true;
                }
                else
                {
                    MessageBox.Show("Unable to download member " + Lib + "/" + Obj + "." + Mbr + ". Please check it exists and that you have access to the remote system.");
                }
                    
            }
            else
            {
                Editor.TheEditor.SwitchToTab(TabIndex);
                result = true;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;

            if (!IBMiUtils.IsValueObjectName(lib.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(spf.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(mbr.Text))
                isValid = false;

            if (isValid)
            {
                if (OpenMember_(lib.Text, spf.Text, mbr.Text, type.Text, true))
                    this.Close();
            }
            else
                MessageBox.Show("Member information not valid.");
        }
    }
}
