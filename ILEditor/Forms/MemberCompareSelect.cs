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
    public partial class MemberCompareSelect : Form
    {
        public MemberCompareSelect(string lib = "", string spf = "", string mbr = "")
        {
            InitializeComponent();
            libA.Text = lib;
            spfA.Text = spf;
            mbrA.Text = mbr;

            if (mbr != "")
                libB.Focus();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void compare_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;

            if (!IBMiUtils.IsValueObjectName(libA.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(spfA.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(mbrA.Text))
                isValid = false;

            if (!IBMiUtils.IsValueObjectName(libB.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(spfB.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(mbrB.Text))
                isValid = false;

            if (!isValid)
            {
                MessageBox.Show("Member information not valid.");
                return;
            }

            string FileA = IBMiUtils.DownloadMember(libA.Text, spfA.Text, mbrA.Text);
            string FileB = IBMiUtils.DownloadMember(libB.Text, spfB.Text, mbrB.Text);

            if (FileA == "" || FileB == "")
            {
                MessageBox.Show("Unable to download members.");
                return;
            }

            new MemberCompareDisplay(
                new RemoteSource(FileA, libA.Text, spfA.Text, mbrA.Text, ""),
                new RemoteSource(FileB, libA.Text, spfA.Text, mbrB.Text, "")
            ).ShowDialog();

            this.Close();
        }
    }
}
