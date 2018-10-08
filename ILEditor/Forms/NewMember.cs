using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class NewMember : Form
    {
        public Boolean created;
        public string _lib;
        public string _spf;
        public string _mbr;
        public string _type;
        public string _text;

        public NewMember(string Lib = "", string Spf = "")
        {
            InitializeComponent();

            lib.Text = Lib;
            spf.Text = Spf;
        }

        private void create_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;
            string Command = "", Local;

            if (!IBMiUtils.IsValueObjectName(lib.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(spf.Text))
                isValid = false;
            if (!IBMiUtils.IsValueObjectName(mbr.Text))
                isValid = false;

            if (isValid)
            {
                _lib = lib.Text.Trim();
                _spf = spf.Text.Trim();
                _mbr = mbr.Text.Trim();
                _type = (type.Text.Trim() == "" ? "*NONE" : type.Text.Trim());
                if (IBMi.IsConnected())
                {
                    _text = (text.Text.Trim() == "" ? "*BLANK" : "'" + text.Text.Trim() + "'");

                    Command = "ADDPFM FILE(" + _lib + "/" + _spf + ") MBR(" + _mbr + ") TEXT(" + _text + ") SRCTYPE(" + _type + ")";
                    if (IBMi.RemoteCommand(Command)) //No error
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Member not created.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (_type == "*NONE") _type = "";
                    Local = IBMiUtils.GetLocalFile(_lib, _spf, _mbr, _type);

                    if (!File.Exists(Local))
                    {
                        File.Create(Local).Close();
                        created = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Local member not created as already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Provided member information not valid.", "Invalid member.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
