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

        //Config
        public static Config Config;

        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            HostSelect Selector = new HostSelect();

            //Application.Run(new Splash());

            Config = new Config(SOURCEDIR + @"\config");
            Config.DoEditorDefaults();

            Directory.CreateDirectory(SYSTEMSDIR);
            Directory.CreateDirectory(SOURCEDIR);

            Application.Run(Selector);
            if (Selector.SystemSelected)
            {
                if (Password.Decode(IBMi.CurrentSystem.GetValue("password")) == "")
                {
                    MessageBox.Show("ILEditor has been updated to encrypt local passwords. Please update your password in the Connection Settings.", "Password Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new Connection().ShowDialog();
                }

                Application.Run(new Editor());
            }
        }

        static String getVersion()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
