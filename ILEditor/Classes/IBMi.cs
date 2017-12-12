using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace ILEditor.Classes
{
    class IBMi
    {
        public static Config CurrentSystem;
        public static Boolean _NotConnected = false;
        public static Boolean FTPFirewallIssue = false;

        public static Boolean RunCommands(string[] list)
        {
            string password = Password.Decode(CurrentSystem.GetValue("password"));
            Boolean result = true;

            string tempfile = Path.GetTempFileName();
            File.Move(tempfile, tempfile + ".ftp");
            tempfile += ".ftp";

            List<string> lines = new List<string>();

            lines.Add("user " + CurrentSystem.GetValue("username"));
            lines.Add(password);

            lines.Add("ASCII");
            if (IBMi.CurrentSystem.GetValue("useuserlibl") != "true")
                lines.Add($"QUOTE RCMD CHGLIBL LIBL({ CurrentSystem.GetValue("datalibl").Replace(',', ' ')}) CURLIB({ CurrentSystem.GetValue("curlib") })");
            foreach (string cmd in list)
            {
                if (cmd == null) continue;
                if (cmd.Trim() != "")
                {
                    lines.Add(cmd);
                }
            }
            lines.Add("quit");

            File.WriteAllLines(tempfile, lines.ToArray());

            FTPProcess ftp = new FTPProcess();
            result = ftp.RunFTP(tempfile);
            try
            {
                File.Delete(tempfile);
            } catch { }

            return result;
        }
        
    }

    class FTPProcess
    {
        public FTPProcess()
        {
        }

        readonly static Dictionary<string, string> FTPCodeMessages = new Dictionary<string, string>()
        {
            { "425", "Not able to open data connection. This might mean that your system is blocking either: FTP, port 20 or port 21. Please allow these through the Windows Firewall. Check the Welcome screen for a 'Getting an FTP error?' and follow the instructions." },
            { "426", "Connection closed; transfer aborted." },
            { "530", "Configuration username and password incorrect." }
        };

        private string _Failed = "";

        private Boolean _getList = false;
        private List<string> _list = new List<string>();

        public string[] GetListing()
        {
            return _list.ToArray();
        }

        public Boolean RunFTP(string FileLoc)
        {
            _list.Clear();
            IBMi._NotConnected = false;
            _Failed = "";

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c FTP -n -s:\"" + FileLoc + "\" " + IBMi.CurrentSystem.GetValue("system");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (IBMi.FTPFirewallIssue)
                _Failed = "425";

            if (IBMi._NotConnected)
            {
                MessageBox.Show("Not able to connect to " + IBMi.CurrentSystem.GetValue("system"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (_Failed != "")
            {
                if (FTPCodeMessages.ContainsKey(_Failed))
                    MessageBox.Show(FTPCodeMessages[_Failed], "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return _Failed != "" || IBMi._NotConnected;
        }

        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            string code = "";
            if (outLine.Data != null)
            {
                if (outLine.Data.Length >= 5)
                {
                    if (outLine.Data.Trim() == "Not connected.")
                    {
                        IBMi._NotConnected = true;
                    }
                    else
                    {
                        Console.WriteLine(outLine.Data);
                        code = outLine.Data.Substring(0, 3);
                        switch (code)
                        {
                            case "200":
                                IBMi.FTPFirewallIssue = true;
                                break;

                            case "125":
                                _getList = true;
                                break;
                            case "250":
                                _getList = false;
                                break;
                            case "425":
                            case "426":
                            case "530":
                            case "550":
                                _Failed = outLine.Data.Substring(0, 3);
                                //_output.Add("> " + outLine.Data.Substring(4));
                                break;
                            default:
                                IBMi.FTPFirewallIssue = false;
                                if (_getList) _list.Add(outLine.Data);
                                break;
                        }
                    }
                }
            }
        }
    }
}
