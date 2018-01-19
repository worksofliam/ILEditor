using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using FluentFTP;
using System.Net.Sockets;

namespace ILEditor.Classes
{
    class IBMi
    {
        public static Config CurrentSystem;
        private static FtpClient Client;
        
        public readonly static Dictionary<string, string> FTPCodeMessages = new Dictionary<string, string>()
        {
            { "425", "Not able to open data connection. This might mean that your system is blocking either: FTP, port 20 or port 21. Please allow these through the Windows Firewall. Check the Welcome screen for a 'Getting an FTP error?' and follow the instructions." },
            { "426", "Connection closed; transfer aborted. The file may be locked." },
            { "426T", "Member was saved but characters have been truncated as record length has been reached." },
            { "426L", "Member was not saved due to a possible lock." },
            { "426F", "Member was not found. Perhaps it was deleted." },
            { "530", "Configuration username and password incorrect." }
        };

        public static void HandleError(string Code, string Message)
        {
            string ErrorMessageText = "";
            switch (Code)
            {
                case "200":
                    ErrorMessageText = "425";
                    break;

                case "425":
                case "426":
                case "530":
                case "550":
                    ErrorMessageText = Code;

                    switch (Code)
                    {
                        case "426":
                            if (Message.Contains("truncated"))
                                ErrorMessageText = "426T";

                            else if (Message.Contains("Unable to open or create"))
                                ErrorMessageText = "426L";

                            else if (Message.Contains("not found"))
                                ErrorMessageText = "426F";

                            break;
                        case "550":
                            if (Message.Contains("not created in"))
                                ErrorMessageText = "550NC";
                            break;
                    }

                    break;
            }

            if (FTPCodeMessages.ContainsKey(ErrorMessageText))
                MessageBox.Show(FTPCodeMessages[ErrorMessageText], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool IsConnected() => Client.IsConnected;
        public static string FTPFile = "";
        public static bool Connect(bool OfflineMode = false)
        {
            bool result = false;
            try
            {
                FTPFile = IBMiUtils.GetLocalFile("QTEMP", "FTPLOG", DateTime.Now.ToString("MMddTHHmm"), "txt");
                FtpTrace.AddListener(new TextWriterTraceListener(FTPFile));
                FtpTrace.LogUserName = false;   // hide FTP user names
                FtpTrace.LogPassword = false;   // hide FTP passwords
                FtpTrace.LogIP = false; 	// hide FTP server IP addresses

                string password = Password.Decode(CurrentSystem.GetValue("password"));
                Client = new FtpClient(CurrentSystem.GetValue("system"), CurrentSystem.GetValue("username"), password);

                if (OfflineMode == false)
                {
                    Client.UploadDataType = FtpDataType.ASCII;
                    Client.DownloadDataType = FtpDataType.ASCII;

                    //FTPES is configurable
                    if (IBMi.CurrentSystem.GetValue("useFTPES") == "true")
                        Client.EncryptionMode = FtpEncryptionMode.Explicit;

                    Client.ConnectTimeout = 5000;
                    Client.Connect();

                    //Change the user library list on connection
                    if (IBMi.CurrentSystem.GetValue("useuserlibl") != "true")
                        RemoteCommand($"CHGLIBL LIBL({ CurrentSystem.GetValue("datalibl").Replace(',', ' ')}) CURLIB({ CurrentSystem.GetValue("curlib") })");
                }

                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to connect to " + CurrentSystem.GetValue("system") + " - " + e.Message, "Cannot Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        public static void Disconnect()
        {
            if (Client.IsConnected)
                Client.Disconnect();
        }

        //Returns false if successful
        public static bool DownloadFile(string Local, string Remote)
        {
            bool Result = false;
            try
            {
                if (Client.IsConnected)
                    Result = !Client.DownloadFile(Local, Remote, true);
                else
                    return true; //error
            }
            catch (Exception e)
            {
                if (e.InnerException is FtpCommandException)
                {
                    FtpCommandException err = e.InnerException as FtpCommandException;
                    HandleError(err.CompletionCode, err.Message);
                }
                Result = true;
            }
            
            return Result;
        }

        //Returns true if successful
        public static bool UploadFile(string Local, string Remote)
        {
            if (Client.IsConnected)
                return Client.UploadFile(Local, Remote, FtpExists.Overwrite);
            else
                return false;
        }

        //Returns true if successful
        public static bool RemoteCommand(string Command, bool ShowError = true)
        {
            if (Client.IsConnected)
            {
                string inputCmd = "RCMD " + Command;
                FtpReply reply = Client.Execute(inputCmd);

                if (ShowError)
                    HandleError(reply.Code, reply.ErrorMessage);

                return reply.Success;
            }
            else
            {
                return false;
            }
        }

        //Returns true if successful
        public static bool RunCommands(string[] Commands)
        {
            bool result = true;
            if (Client.IsConnected)
            {
                foreach (string Command in Commands)
                {
                    if (RemoteCommand(Command) == false)
                        result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

    }
}
