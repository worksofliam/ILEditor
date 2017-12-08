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
        public static readonly string SYNTAXDIR = Environment.GetEnvironmentVariable("APPDATA") + @"\ILEditor\langs\";

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

            Application.Run(new Splash());

            Config = new Config(SOURCEDIR + @"\config");
            Config.DoEditorDefaults();

            Directory.CreateDirectory(SYSTEMSDIR);
            Directory.CreateDirectory(SOURCEDIR);

            if (Config.GetValue("version") != getVersion())
            {
                //Update any files due to ILEditor upgrade
                Directory.Delete(SYNTAXDIR, true);

                Config.SetValue("version", getVersion());
            }

            if (!Directory.Exists(SYNTAXDIR))
            {
                Directory.CreateDirectory(SYNTAXDIR);
                File.WriteAllText(Program.SYNTAXDIR + "lightRPG.xml", Properties.Resources.lightRPG);
                File.WriteAllText(Program.SYNTAXDIR + "lightCPP.xml", Properties.Resources.lightCPP);
                File.WriteAllText(Program.SYNTAXDIR + "lightCOBOL.xml", Properties.Resources.lightCOBOL);
                File.WriteAllText(Program.SYNTAXDIR + "lightCL.xml", Properties.Resources.lightCL);
                File.WriteAllText(Program.SYNTAXDIR + "lightSQL.xml", Properties.Resources.lightSQL);

                File.WriteAllText(Program.SYNTAXDIR + "darkRPG.xml", Properties.Resources.darkRPG);
                File.WriteAllText(Program.SYNTAXDIR + "darkCPP.xml", Properties.Resources.darkCPP);
                File.WriteAllText(Program.SYNTAXDIR + "darkCOBOL.xml", Properties.Resources.darkCOBOL);
                File.WriteAllText(Program.SYNTAXDIR + "darkCL.xml", Properties.Resources.darkCL);
                File.WriteAllText(Program.SYNTAXDIR + "darkSQL.xml", Properties.Resources.darkSQL);
            }

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
