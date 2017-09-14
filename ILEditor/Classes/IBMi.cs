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

        readonly static Dictionary<string, string> FTPCodeMessages = new Dictionary<string, string>()
        {
            { "425", "Not able to open data connection. This might mean that your system is blocking either: FTP, port 20 or port 21. Please allow these through the Windows Firewall. Check the Welcome screen for a 'Getting an FTP error?' and follow the instructions." },
            { "426", "Connection closed; transfer aborted." },
            { "530", "Configuration username and password incorrect." }
        };

        private static Boolean _NotConnected = false;
        private static string _Failed = "";

        private static Boolean _getList = false;
        private static List<string> _list = new List<string>();

        public static string[] GetListing()
        {
            return _list.ToArray();
        }

        public static Boolean RunCommands(string[] list)
        {
            Boolean result = true;

            string tempfile = Path.GetTempFileName();
            File.Move(tempfile, tempfile + ".ftp");
            tempfile += ".ftp";

            List<string> lines = new List<string>();

            lines.Add("user " + CurrentSystem.GetValue("username"));
            lines.Add(CurrentSystem.GetValue("password"));

            lines.Add("ASCII");
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
            result = RunFTP(tempfile);
            File.Delete(tempfile);

            return result;
        }

        private static Boolean RunFTP(string FileLoc)
        {
            _list.Clear();
            _NotConnected = false;
            _Failed = "";

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c FTP -n -s:\"" + FileLoc + "\" " + CurrentSystem.GetValue("system");
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
            
            if (_NotConnected)
            {
                MessageBox.Show("Not able to connect to " + CurrentSystem.GetValue("system"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (_Failed != "")
            {
                if (FTPCodeMessages.ContainsKey(_Failed))
                    MessageBox.Show(FTPCodeMessages[_Failed], "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return _Failed != "" || _NotConnected;
        }

        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (outLine.Data != null)
            {
                if (outLine.Data.Length >= 5)
                {
                    if (outLine.Data.Trim() == "Not connected.")
                    {
                        _NotConnected = true;
                    }
                    else
                    {
                        Console.WriteLine(outLine.Data);
                        switch (outLine.Data.Substring(0, 3))
                        {
                            case "125":
                                _getList = true;
                                break;
                            case "250":
                                _getList = false;
                                //_output.Add("> " + outLine.Data.Substring(4));
                                break;
                            case "150":
                                //_output.Add("> " + outLine.Data.Substring(4));
                                break;
                            case "425":
                            case "426":
                            case "530":
                            case "550":
                                _Failed = outLine.Data.Substring(0, 3);
                                //_output.Add("> " + outLine.Data.Substring(4));
                                break;
                            default:
                                if (_getList) _list.Add(outLine.Data);
                                break;
                        }
                    }
                }
            }
        }
    }
}
