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
        public static readonly string SYSTEMSDIR = Environment.GetEnvironmentVariable("ProgramData") + @"\ileditor";
        public static readonly string SOURCEDIR = Environment.GetEnvironmentVariable("APPDATA") + @"\ILEditor";
        public static readonly string CONFIGDIR = SOURCEDIR + @"\config";

        //Config
        public static Config Config;

        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RLAConverterLib.Module.UIEntry = true;

            HostSelect Selector;
            HostSelect Selector = new HostSelect();

            //Application.Run(new Splash());

            Directory.CreateDirectory(SYSTEMSDIR);
            Directory.CreateDirectory(SOURCEDIR);

            Config = new Config(CONFIGDIR);
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

                    Connected = IBMi.Connect();

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

        static String getVersion()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
