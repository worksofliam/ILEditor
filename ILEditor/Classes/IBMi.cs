using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using FluentFTP;
using Timer = System.Timers.Timer;

namespace ILEditor.Classes
{
	internal static class IBMi
	{
		public static  Config    CurrentSystem;
		private static FtpClient _client;

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

		public static bool IsConnected => _client?.IsConnected == true;

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

		public static bool Connect(bool OfflineMode = false, string promptedPassword = "")
		{
			var result = false;
			try
			{
				FTPFile = IBMiUtils.GetLocalFile("QTEMP", "FTPLOG", DateTime.Now.ToString("MMddTHHmm"), "txt");
				FtpTrace.AddListener(new TextWriterTraceListener(FTPFile));
				FtpTrace.LogUserName = false; // hide FTP user names
				FtpTrace.LogPassword = false; // hide FTP passwords
				FtpTrace.LogIP       = false; // hide FTP server IP addresses

				var password = "";

				var remoteSystem = CurrentSystem.GetValue("system").Split(':');

				if (promptedPassword == string.Empty)
					password = Password.Decode(CurrentSystem.GetValue("password"));
				else
					password = promptedPassword;

				_client = new FtpClient(remoteSystem[0], CurrentSystem.GetValue("username"), password);

				if (OfflineMode == false)
				{
					_client.UploadDataType   = FtpDataType.ASCII;
					_client.DownloadDataType = FtpDataType.ASCII;

					//FTPES is configurable
					if (CurrentSystem.GetValue("useFTPES") == "true")
						_client.EncryptionMode = FtpEncryptionMode.Explicit;

					//Client.DataConnectionType = FtpDataConnectionType.AutoPassive; //THIS IS THE DEFAULT VALUE
					_client.DataConnectionType = GetFtpDataConnectionType(CurrentSystem.GetValue("transferMode"));
					_client.SocketKeepAlive    = true;

					if (remoteSystem.Length == 2)
						_client.Port = int.Parse(remoteSystem[1]);

					_client.ConnectTimeout = 5000;
					_client.Connect();

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
			if (_client.IsConnected)
				_client.Disconnect();
		}

		private static void KeepAliveFunc(object sender, ElapsedEventArgs e)
		{
			var showError = !_client.IsConnected;
			if (_client.IsConnected)
				try
				{
					_client.Execute("NOOP");
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
			if (_client?.IsConnected == true)
				return _client.SystemType;

			return string.Empty;
		}

		//Returns false if successful
		public static bool DownloadFile(string Local, string Remote)
		{
			bool result;
			try
			{
				if (_client.IsConnected)
					result = !_client.DownloadFile(Local, Remote, true);
				else
					return true; //error
			}
			catch (Exception e)
			{
				if (e.InnerException is FtpCommandException ftpCmdErr)
					HandleError(ftpCmdErr.CompletionCode, ftpCmdErr.Message);

				result = true;
			}

			return result;
		}

		//Returns true if successful
		public static bool UploadFile(string Local, string Remote)
		{
			if (_client.IsConnected)
				return _client.UploadFile(Local, Remote, FtpExists.Overwrite);

			return false;
		}

		//Returns true if successful
		public static bool RemoteCommand(string Command, bool ShowError = true)
		{
			if (!_client.IsConnected)
				return false;

			var inputCmd = "RCMD " + Command;

			//IF THIS CRASHES CLIENT DISCONNECTS!!!
			var reply = _client.Execute(inputCmd);

			if (ShowError)
				HandleError(reply.Code, reply.ErrorMessage);

			return reply.Success;
		}

		public static string RemoteCommandResponse(string Command)
		{
			if (!_client.IsConnected)
				return "Not connected.";

			var inputCmd = "RCMD " + Command;
			var reply    = _client.Execute(inputCmd);

			if (reply.Success)
				return string.Empty;

			return reply.ErrorMessage;
		}

		//Returns true if successful
		public static bool RunCommands(string[] Commands)
		{
			if (_client.IsConnected)
				return Commands.All(command => RemoteCommand(command));

			return false;
		}

		public static bool FileExists(string remoteFile)
		{
			return _client.FileExists(remoteFile);
		}

		public static bool DirExists(string remoteDir)
		{
			try
			{
				return _client.DirectoryExists(remoteDir);
			}
			catch (Exception ex)
			{
				Editor.TheEditor.SetStatus(ex.Message + " - please try again.");

				return false;
			}
		}

		public static FtpListItem[] GetListing(string remoteDir)
		{
			return _client.GetListing(remoteDir);
		}

		public static string RenameDir(string remoteDir, string newName)
		{
			var pieces = remoteDir.Split('/');
			pieces[pieces.Length - 1] = newName;
			newName                   = string.Join("/", pieces);

			if (_client.MoveDirectory(remoteDir, string.Join("/", pieces)))
				return newName;

			return remoteDir;
		}

		public static string RenameFile(string remoteFile, string newName)
		{
			var pieces = remoteFile.Split('/');
			pieces[pieces.Length - 1] = newName;
			newName                   = string.Join("/", pieces);

			if (_client.MoveFile(remoteFile, newName))
				return newName;

			return remoteFile;
		}

		public static void DeleteDir(string remoteDir)
		{
			_client.DeleteDirectory(remoteDir, FtpListOption.AllFiles);
		}

		public static void DeleteFile(string remoteFile)
		{
			_client.DeleteFile(remoteFile);
		}

		public static void SetWorkingDir(string RemoteDir)
		{
			_client.SetWorkingDirectory(RemoteDir);
		}

		public static void CreateDirectory(string RemoteDir)
		{
			_client.CreateDirectory(RemoteDir);
		}

		public static void UploadFiles(string RemoteDir, string[] Files)
		{
			_client.UploadFiles(Files, RemoteDir, FtpExists.Overwrite, true);
		}
	}
}