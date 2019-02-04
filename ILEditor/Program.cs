using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ILEditor.Classes;
using ILEditor.Forms;

namespace ILEditor
{
	internal static class Program
	{
		//Directories
		public static readonly Encoding Encoding = Encoding.GetEncoding("ISO-8859-1");

		public static readonly string APPDIR =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ILEditorData");

		public static readonly string SYSTEMSDIR = Path.Combine(APPDIR, "systems"); //Directory
		public static readonly string SOURCEDIR  = Path.Combine(APPDIR, "source");  //Directory
		public static readonly string DUMPSDIR   = Path.Combine(APPDIR, "dumps");   //Directory
		public static readonly string CONFIGFILE = Path.Combine(APPDIR, "config");  //Config file
		public static readonly string PanelsXML  = Path.Combine(APPDIR, "panels.xml");

		public static readonly string[] TaskKeywords = {"TODO", "HACK"};

		public static string LAST_BUILD = ""; //Used for F5 key for local project build

		//Config
		public static Config Config;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
		private static void Main()
		{
			var promptedPassword = "";

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Directory.CreateDirectory(SYSTEMSDIR);
			Directory.CreateDirectory(SOURCEDIR);
			Directory.CreateDirectory(DUMPSDIR);

			Config = new Config(CONFIGFILE);
			Config.DoEditorDefaults();

			var connected = false;
			while (connected == false)
			{
				var selector = new HostSelect();
				Application.Run(selector);

				if (selector.SystemSelected)
				{
					if (IBMi.CurrentSystem.GetValue("password") == "")
					{
						var prompter = new PasswordPrompt(IBMi.CurrentSystem.GetValue("alias"),
							IBMi.CurrentSystem.GetValue("username"));

						prompter.ShowDialog();
						if (prompter.Success)
							promptedPassword = prompter.GetResult();
					}

					connected = IBMi.Connect(selector.OfflineModeSelected(), promptedPassword);

					if (connected)
					{
						if (Config.GetValue("srcdat_agreement") == "false")
						{
							MessageBox.Show(
								"Thanks for using ILEditor. This is a notice to tell you that when editing source members, the SRCDAT value is not retained. This is due to a restriction in our connection method. By using ILEditor you agree to our LICENCE, found on the ILEditor GitHub repository. Please seek ILEditor GitHub issues for further information.",
								"ILEditor Notice",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information);

							Config.SetValue("srcdat_agreement", "true");
							Config.SaveConfig();
						}

						try
						{
							Application.Run(new Editor());
						}
						catch (Exception e)
						{
							File.WriteAllText(Path.Combine(DUMPSDIR, DateTime.Now.ToFileTime() + ".txt"), e.ToString());
							MessageBox.Show("There was an error. Crash dump created: " + DUMPSDIR);
						}

						IBMi.Disconnect();
					}
					else
					{
						//Basically, if it failed to connect when they're using FTPES - offer them a FTP connection
						if (IBMi.CurrentSystem.GetValue("useFTPES") == "true")
							MessageBox.Show("Failed to connect. Perhaps try disabling FTPES and then connecting again.",
								"Connection",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information);
					}
				}
				else
				{
					connected = true; //End loop and close
				}
			}
		}

		public static string GetVersion()
		{
			return Assembly.GetEntryAssembly().GetName().Version.ToString();
		}
	}
}