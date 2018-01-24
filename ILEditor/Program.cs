using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Forms;
using ILEditor.Classes;
using System.Deployment.Application;
using System.Reflection;

namespace ILEditor
{
    static class Program
    {
        //Directories
        public static readonly string SYSTEMSDIR_Old = Environment.GetEnvironmentVariable("ProgramData") + @"\ileditor";
        public static readonly string SOURCEDIR_Old = Environment.GetEnvironmentVariable("APPDATA") + @"\ILEditor";
        public static readonly string CONFIGDIR_Old = SOURCEDIR_Old + @"\config";

        public static readonly string APPDIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ILEditorData");
        public static readonly string SYSTEMSDIR = Path.Combine(APPDIR, "systems"); //Directory
        public static readonly string SOURCEDIR = Path.Combine(APPDIR, "source"); //Directory
        public static readonly string CONFIGFILE = Path.Combine(APPDIR, "config"); //Config file
        public static readonly string PROJDIR = Path.Combine(APPDIR, "projects"); //Directory

        public static string LAST_BUILD = ""; //Used for F5 key for local project build


        //Config
        public static Config Config;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HostSelect Selector;

            if (!Directory.Exists(APPDIR))
            {
                Directory.CreateDirectory(APPDIR);
                if (Directory.Exists(SYSTEMSDIR_Old))
                    Directory.Move(SYSTEMSDIR_Old, SYSTEMSDIR);

                if (File.Exists(CONFIGDIR_Old))
                    File.Move(CONFIGDIR_Old, CONFIGFILE);

                if (Directory.Exists(SOURCEDIR_Old))
                    Directory.Move(SOURCEDIR_Old, SOURCEDIR);
            }

            Directory.CreateDirectory(SYSTEMSDIR);
            Directory.CreateDirectory(SOURCEDIR);
            Directory.CreateDirectory(PROJDIR);

            Config = new Config(CONFIGFILE);
            Config.DoEditorDefaults();

            bool Connected = false;
            while (Connected == false)
            {
                Selector = new HostSelect();
                Application.Run(Selector);

                if (Selector.SystemSelected)
                {
                    if (Password.Decode(IBMi.CurrentSystem.GetValue("password")) == "")
                    {
                        MessageBox.Show("ILEditor has been updated to encrypt local passwords. Please update your password in the Connection Settings.", "Password Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        new Connection().ShowDialog();
                    }

                    Connected = IBMi.Connect(Selector.OfflineModeSelected());

                    if (Connected)
                    {
                        Application.Run(new Editor());
                        IBMi.Disconnect();
                    }
                    else
                    {
                        //Basically, if it failed to connect when they're using FTPES - offer them a FTP connection
                        if (IBMi.CurrentSystem.GetValue("useFTPES") == "true")
                        {
                            DialogResult Result = MessageBox.Show("Would you like to try and connect again using a plain FTP connection? This will change the systems settings.", "Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (Result == DialogResult.Yes)
                            {
                                IBMi.CurrentSystem.SetValue("useFTPES", "false");
                                Connected = IBMi.Connect();
                                if (Connected)
                                {
                                    Application.Run(new Editor());
                                    IBMi.Disconnect();
                                }
                            }
                        }
                    }
                }
                else
                {
                    Connected = true; //End loop and close
                }
            }
        }

        public static String getVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
