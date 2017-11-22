using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Forms;
using ILEditor.Classes;

namespace ILEditor
{
    static class Program
    {
        public static readonly string SYSTEMSDIR = Environment.GetEnvironmentVariable("ProgramData") + @"\ileditor";
        public static readonly string SOURCEDIR = Environment.GetEnvironmentVariable("APPDATA") + @"\idle";
        public static readonly string SYNTAXDIR = Environment.GetEnvironmentVariable("APPDATA") + @"\idle\langs\";
        public static readonly string ACSPATH = Environment.GetEnvironmentVariable("APPDATA") + @"\idle\acspath";

        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            HostSelect Selector = new HostSelect();

            Application.Run(new Splash());
            
            Directory.CreateDirectory(SYSTEMSDIR);
            Directory.CreateDirectory(SOURCEDIR);
            Directory.CreateDirectory(SYNTAXDIR);

            if (!File.Exists(Program.SYNTAXDIR + "RPG.xml"))
                File.WriteAllText(Program.SYNTAXDIR + "RPG.xml", Properties.Resources.RPGSyntax);

            if (!File.Exists(Program.SYNTAXDIR + "SQL.xml"))
                File.WriteAllText(Program.SYNTAXDIR + "SQL.xml", Properties.Resources.SQLSyntax);

            if (!File.Exists(Program.SYNTAXDIR + "CPP.xml"))
                File.WriteAllText(Program.SYNTAXDIR + "CPP.xml", Properties.Resources.CPPSyntax);

            if (!File.Exists(Program.SYNTAXDIR + "CL.xml"))
                File.WriteAllText(Program.SYNTAXDIR + "CL.xml", Properties.Resources.CLSyntax);

            if (!File.Exists(Program.SYNTAXDIR + "COBOL.xml"))
                File.WriteAllText(Program.SYNTAXDIR + "COBOL.xml", Properties.Resources.COBOLSyntax);

            if (!File.Exists(ACSPATH))
                File.WriteAllText(ACSPATH, "false");

            Application.Run(Selector);
            if (Selector.SystemSelected)
                Application.Run(new Editor());
        }
    }
}
