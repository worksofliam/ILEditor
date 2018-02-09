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
using System.IO;
using System.Diagnostics;

namespace ILEditor.Forms
{
    public partial class Connection : Form
    {
        private static readonly Dictionary<string, string> DataConnectionTypes = new Dictionary<string, string>()
        {
            { "AutoPassive", "Default option. This type of data connection attempts to use the EPSV command and if the server does not support EPSV it falls back to the PASV command before giving up unless you are connected via IPv6 in which case the PASV command is not supported." },
            { "PASV", "Passive data connection. EPSV is a better option if it's supported. Passive connections connect to the IP address dictated by the server which may or may not be accessible by the client for example a server behind a NAT device may give an IP address on its local network that is inaccessible to the client. Please note that IPv6 does not support this type data connection. If you ask for PASV and are connected via IPv6 EPSV will automatically be used in its place." },
            { "PASVEX", "Same as PASV except the host supplied by the server is ignored and the data connection is made to the same address that the control connection is connected to. This is useful in scenarios where the server supplies a private/non-routable network address in the PASV response. It's functionally identical to EPSV except some servers may not implement the EPSV command. Please note that IPv6 does not support this type data connection. If you ask for PASV and are connected via IPv6 EPSV will automatically be used in its place." },
            { "EPSV", "Extended passive data connection, recommended. Works the same as a PASV connection except the server does not dictate an IP address to connect to, instead the passive connection goes to the same address used in the control connection. This type of data connection supports IPv4 and IPv6." },
            { "AutoActive", "This type of data connection attempts to use the EPRT command and if the server does not support EPRT it falls back to the PORT command before giving up unless you are connected via IPv6 in which case the PORT command is not supported." },
            { "PORT", "Active data connection, not recommended unless you have a specific reason for using this type. Creates a listening socket on the client which requires firewall exceptions on the client system as well as client network when connecting to a server outside of the client's network. In addition the IP address of the interface used to connect to the server is the address the server is told to connect to which, if behind a NAT device, may be inaccessible to the server. This type of data connection is not supported by IPv6. If you specify PORT and are connected via IPv6 EPRT will automatically be used instead." },
            { "EPRT", "Extended active data connection, not recommended unless you have a specific reason for using this type. Creates a listening socket on the client which requires firewall exceptions on the client as well as client network when connecting to a server outside of the client's network. The server connects to the IP address it sees the client coming from. This type of data connection supports IPv4 and IPv6." }
            
        };

        public Connection()
        {
            string password = "";
            InitializeComponent();

            systemInfo.Text = IBMi.GetSystem().Replace(". ", ".\n");

            host.Text = IBMi.CurrentSystem.GetValue("system");
            user.Text = IBMi.CurrentSystem.GetValue("username");

            password = IBMi.CurrentSystem.GetValue("password");
            password = Password.Decode(password);
            pass.Text = password;

            fetchJobLog.Checked = (IBMi.CurrentSystem.GetValue("fetchJobLog") == "true");

            dataConnectionType.SelectedItem = IBMi.CurrentSystem.GetValue("transferMode");
            ftpes.Checked = (Program.Config.GetValue("useFTPES") == "true");

            homeDir.Text = IBMi.CurrentSystem.GetValue("homeDir");
            buildLib.Text = IBMi.CurrentSystem.GetValue("buildLib");
            tempSpf.Text = IBMi.CurrentSystem.GetValue("tempSpf");

            selectedFont.SelectedItem = IBMi.CurrentSystem.GetValue("FONT");
            cur_size.Text = IBMi.CurrentSystem.GetValue("ZOOM");
            indent_size.Value = decimal.Parse(IBMi.CurrentSystem.GetValue("INDENT_SIZE"));
            show_spaces.SelectedItem = IBMi.CurrentSystem.GetValue("SHOW_SPACES");
            highlight_line.SelectedItem = IBMi.CurrentSystem.GetValue("HIGHLIGHT_CURRENT_LINE");

            prntLib.Text = IBMi.CurrentSystem.GetValue("printerLib");
            prntObj.Text = IBMi.CurrentSystem.GetValue("printerObj");

            validACS.Checked = (Program.Config.GetValue("acspath") != "false");
            darkMode.Checked = (Program.Config.GetValue("darkmode") == "true");
            toolbarSide.SelectedItem = (Program.Config.GetValue("toolbarSide"));
        }

        private void save_Click(object sender, EventArgs e)
        {


            string password = "";
            IBMi.CurrentSystem.SetValue("system", host.Text.Trim());
            IBMi.CurrentSystem.SetValue("username", user.Text.Trim());
            password = pass.Text.Trim();
            password = Password.Encode(password);
            IBMi.CurrentSystem.SetValue("password", password);

            IBMi.CurrentSystem.SetValue("fetchJobLog", fetchJobLog.Checked.ToString().ToLower());
            
            IBMi.CurrentSystem.SetValue("transferMode", dataConnectionType.SelectedItem.ToString());
            IBMi.CurrentSystem.SetValue("useFTPES", ftpes.Checked.ToString().ToLower());

            IBMi.CurrentSystem.SetValue("homeDir", homeDir.Text);
            IBMi.CurrentSystem.SetValue("buildLib", buildLib.Text);
            IBMi.CurrentSystem.SetValue("tempSpf", tempSpf.Text);

            IBMi.CurrentSystem.SetValue("FONT", selectedFont.SelectedItem.ToString());
            IBMi.CurrentSystem.SetValue("INDENT_SIZE", indent_size.Value.ToString());
            IBMi.CurrentSystem.SetValue("SHOW_SPACES", show_spaces.SelectedItem.ToString());
            IBMi.CurrentSystem.SetValue("HIGHLIGHT_CURRENT_LINE", highlight_line.SelectedItem.ToString());

            IBMi.CurrentSystem.SetValue("printerLib", prntLib.Text);
            IBMi.CurrentSystem.SetValue("printerObj", prntObj.Text);

            //ACS value is handled differently (findACS_Click)
            Program.Config.SetValue("darkmode", darkMode.Checked.ToString().ToLower());
            Program.Config.SetValue("toolbarSide", toolbarSide.SelectedItem.ToString());
            this.Close();
        }

        private void findACS_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "Applications (*.exe)|*.exe";
            openFile.ShowDialog();
            validACS.Checked = File.Exists(openFile.FileName);
            if (validACS.Checked)
            {
                Program.Config.SetValue("acspath", openFile.FileName);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www-01.ibm.com/support/docview.wss?uid=nas8N1014798");
        }

        private void infoBox_SelectionChanged(object sender, EventArgs e)
        {
            infoBox.Text = DataConnectionTypes[dataConnectionType.SelectedItem.ToString().ToString()];
        }
    }
}
