using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using FluentFTP;
using Timer = System.Timers.Timer;

namespace ILEditor.Classes
{
	internal class IBMi
	{
		public static  Config    CurrentSystem;
		private static FtpClient Client;

		public static readonly Dictionary<string, string> FTPCodeMessages = new Dictionary<string, string>
		{
			{
				"425",
				"Not able to open data connection. This might mean that your system is blocking either: FTP, port 20 or port 21. Please allow these through the Windows Firewall. Check the Welcome screen for a 'Getting an FTP error?' and follow the instructions."
			},
			{"426", "Connection closed; transfer aborted. The file may be locked."},
			{"426T", "Member was saved but characters have been truncated as record length has been reached."},
			{"426L", "Member was not saved due to a possible lock."},
			{"426F", "Member was not found. Perhaps it was deleted."},
			{"530", "Configuration username and password incorrect."}
		};

		public static string FTPFile = "";

		public static void HandleError(string Code, string Message)
		{
			var errorMessageText = "";
			switch (Code)
			{
				case "200":
					errorMessageText = "425";

					break;

				case "425":
				case "426":
				case "530":
				case "550":
					errorMessageText = Code;

					switch (Code)
					{
						case "426":
							if (Message.Contains("truncated"))
								errorMessageText = "426T";

							else if (Message.Contains("Unable to open or create"))
								errorMessageText = "426L";

							else if (Message.Contains("not found"))
								errorMessageText = "426F";

							break;
						case "550":
							if (Message.Contains("not created in"))
								errorMessageText = "550NC";

							break;
					}

					break;
			}

			if (FTPCodeMessages.ContainsKey(errorMessageText))
				MessageBox.Show(FTPCodeMessages[errorMessageText], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private static FtpDataConnectionType GetFtpDataConnectionType(string Type)
		{
			if (Enum.TryParse(Type, out FtpDataConnectionType result))
				return result;

			return FtpDataConnectionType.AutoActive;
		}

		public static bool IsConnected()
		{
			if (Client != null)
				return Client.IsConnected;

			return false;
		}

		public static bool Connect(bool OfflineMode = false, string promptedPassword = "")
		{
			var      result = false;
			try
			{
				FTPFile = IBMiUtils.GetLocalFile("QTEMP", "FTPLOG", DateTime.Now.ToString("MMddTHHmm"), "txt");
				FtpTrace.AddListener(new TextWriterTraceListener(FTPFile));
				FtpTrace.LogUserName = false; // hide FTP user names
				FtpTrace.LogPassword = false; // hide FTP passwords
				FtpTrace.LogIP       = false; // hide FTP server IP addresses

				var password = "";

				var remoteSystem = CurrentSystem.GetValue("system").Split(':');

				if (promptedPassword == "")
					password = Password.Decode(CurrentSystem.GetValue("password"));
				else
					password = promptedPassword;

				Client = new FtpClient(remoteSystem[0], CurrentSystem.GetValue("username"), password);

				if (OfflineMode == false)
				{
					Client.UploadDataType   = FtpDataType.ASCII;
					Client.DownloadDataType = FtpDataType.ASCII;

					//FTPES is configurable
					if (CurrentSystem.GetValue("useFTPES") == "true")
						Client.EncryptionMode = FtpEncryptionMode.Explicit;

					//Client.DataConnectionType = FtpDataConnectionType.AutoPassive; //THIS IS THE DEFAULT VALUE
					Client.DataConnectionType = GetFtpDataConnectionType(CurrentSystem.GetValue("transferMode"));
					Client.SocketKeepAlive    = true;

					if (remoteSystem.Length == 2)
						Client.Port = int.Parse(remoteSystem[1]);

					Client.ConnectTimeout = 5000;
					Client.Connect();

					//Change the user library list on connection
					RemoteCommand(
						$"CHGLIBL LIBL({CurrentSystem.GetValue("datalibl").Replace(',', ' ')}) CURLIB({CurrentSystem.GetValue("curlib")})");

					var timer = new Timer();
					timer.Interval =  60000;
					timer.Elapsed  += KeepAliveFunc;
					timer.Start();
				}

				result = true;
			}
			catch (Exception e)
			{
				MessageBox.Show("Unable to connect to " + CurrentSystem.GetValue("system") + " - " + e.Message,
					"Cannot Connect",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

			return result;
		}

		public static void Disconnect()
		{
			if (Client.IsConnected)
				Client.Disconnect();
		}

		private static void KeepAliveFunc(object sender, ElapsedEventArgs e)
		{
			var showError = !Client.IsConnected;
			if (Client.IsConnected)
				try
				{
					Client.Execute("NOOP");
					showError = false;
				}
				catch
				{
					showError = true;
				}

			if (showError)
				Editor.TheEditor.SetStatus("Warning! You lost connection " + CurrentSystem.GetValue("system") + "!");
		}

		public static string GetSystem()
		{
			if (Client == null)
				return "";

			if (Client.IsConnected)
				return Client.SystemType;

			return "";

		}

		//Returns false if successful
		public static bool DownloadFile(string Local, string Remote)
		{
			bool result;
			try
			{
				if (Client.IsConnected)
					result = !Client.DownloadFile(Local, Remote, true);
				else
					return true; //error
			}
			catch (Exception e)
			{
				if (e.InnerException is FtpCommandException ftpCmdErr)
				{
					HandleError(ftpCmdErr.CompletionCode, ftpCmdErr.Message);
				}

				result = true;
			}

			return result;
		}

		//Returns true if successful
		public static bool UploadFile(string Local, string Remote)
		{
			if (Client.IsConnected)
				return Client.UploadFile(Local, Remote, FtpExists.Overwrite);

			return false;
		}

		//Returns true if successful
		public static bool RemoteCommand(string Command, bool ShowError = true)
		{
			if (Client.IsConnected)
			{
				var inputCmd = "RCMD " + Command;

				//IF THIS CRASHES CLIENT DISCONNECTS!!!
				var reply = Client.Execute(inputCmd);

				if (ShowError)
					HandleError(reply.Code, reply.ErrorMessage);

				return reply.Success;
			}

			return false;
		}

		public static string RemoteCommandResponse(string Command)
		{
			if (Client.IsConnected)
			{
				var inputCmd = "RCMD " + Command;
				var reply    = Client.Execute(inputCmd);

				if (reply.Success)
					return "";

				return reply.ErrorMessage;
			}

			return "Not connected.";
		}

		//Returns true if successful
		public static bool RunCommands(string[] Commands)
		{
			var result = true;
			if (Client.IsConnected)
				foreach (var command in Commands)
					if (RemoteCommand(command) == false)
					{
						result = false;
						break;
					}
			else
				result = false;

			return result;
		}

		public static bool FileExists(string remoteFile)
		{
			return Client.FileExists(remoteFile);
		}

		public static bool DirExists(string remoteDir)
		{
			try
			{
				return Client.DirectoryExists(remoteDir);
			}
			catch (Exception ex)
			{
				Editor.TheEditor.SetStatus(ex.Message + " - please try again.");

				return false;
			}
		}

		public static FtpListItem[] GetListing(string remoteDir)
		{
			return Client.GetListing(remoteDir);
		}

		public static string RenameDir(string remoteDir, string newName)
		{
			var pieces = remoteDir.Split('/');
			pieces[pieces.Length - 1] = newName;
			newName                   = string.Join("/", pieces);

			if (Client.MoveDirectory(remoteDir, string.Join("/", pieces)))
				return newName;

			return remoteDir;
		}

		public static string RenameFile(string remoteFile, string newName)
		{
			var pieces = remoteFile.Split('/');
			pieces[pieces.Length - 1] = newName;
			newName                   = string.Join("/", pieces);

			if (Client.MoveFile(remoteFile, newName))
				return newName;

			return remoteFile;
		}

		public static void DeleteDir(string remoteDir)
		{
			Client.DeleteDirectory(remoteDir, FtpListOption.AllFiles);
		}

		public static void DeleteFile(string remoteFile)
		{
			Client.DeleteFile(remoteFile);
		}

		public static void SetWorkingDir(string RemoteDir)
		{
			Client.SetWorkingDirectory(RemoteDir);
		}

		public static void CreateDirectory(string RemoteDir)
		{
			Client.CreateDirectory(RemoteDir);
		}

		public static void UploadFiles(string RemoteDir, string[] Files)
		{
			Client.UploadFiles(Files, RemoteDir, FtpExists.Overwrite, true);
		}
	}
}