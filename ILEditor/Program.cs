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

            File.WriteAllText(Program.SYNTAXDIR + "RPG.xml", Properties.Resources.RPGSyntax);
            File.WriteAllText(Program.SYNTAXDIR + "SQL.xml", Properties.Resources.SQLSyntax);
            File.WriteAllText(Program.SYNTAXDIR + "CPP.xml", Properties.Resources.CPPSyntax);
            File.WriteAllText(Program.SYNTAXDIR + "CL.xml", Properties.Resources.CLSyntax);
            File.WriteAllText(Program.SYNTAXDIR + "COBOL.xml", Properties.Resources.COBOLSyntax);

            Application.Run(Selector);
            if (Selector.SystemSelected)
                Application.Run(new Editor());
        }
    }
}
