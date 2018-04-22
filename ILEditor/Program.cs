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
using System.Drawing;
using CefSharp;

namespace ILEditor
{
    static class Program
    {
        //Directories
        public static readonly string APPDIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ILEditorData");
        public static readonly string SYSTEMSDIR = Path.Combine(APPDIR, "systems"); //Directory
        public static readonly string SOURCEDIR = Path.Combine(APPDIR, "source"); //Directory
        public static readonly string CONFIGFILE = Path.Combine(APPDIR, "config"); //Config file
        public static readonly string PanelsXML = Path.Combine(APPDIR, "panels.xml");

        public static readonly string[] TaskKeywords = new[] { "TODO", "HACK" };

        public static string LAST_BUILD = ""; //Used for F5 key for local project build


        //Config
        public static Config Config;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string promptedPassword = "";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            HostSelect Selector;
            PasswordPrompt Prompter;

            Directory.CreateDirectory(SYSTEMSDIR);
            Directory.CreateDirectory(SOURCEDIR);

            Config = new Config(CONFIGFILE);
            Config.DoEditorDefaults();

            bool Connected = false;
            while (Connected == false)
            {
                Selector = new HostSelect();
                Application.Run(Selector);

                if (Selector.SystemSelected)
                {
                    if (IBMi.CurrentSystem.GetValue("password") == "")
                    {
                        Prompter = new PasswordPrompt(IBMi.CurrentSystem.GetValue("alias"), IBMi.CurrentSystem.GetValue("username"));
                        Prompter.ShowDialog();
                        if (Prompter.Success)
                            promptedPassword = Prompter.GetResult();
                    }

                    Connected = IBMi.Connect(Selector.OfflineModeSelected(), promptedPassword);

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
                                Connected = IBMi.Connect(false, promptedPassword);
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

            if (UserTools.ObjectDiagram.ChromiumActive)
                Cef.Shutdown();
        }

        public static String getVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
