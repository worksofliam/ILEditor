using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Forms;
using ILEditor.Classes;
using NetFwTypeLib;
using FastColoredTextBoxNS;

namespace ILEditor
{
    static class Program
    {
        public static readonly string SYSTEMSDIR = Environment.GetEnvironmentVariable("ProgramData") + @"\ileditor";
        public static readonly string SOURCEDIR = Environment.GetEnvironmentVariable("APPDATA") + @"\idle";

        public static AutocompleteItem[] RPGKeywords;
        public static AutocompleteItem[] CPPKeywords;


        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            HostSelect Selector = new HostSelect();

            Application.Run(new Splash());

            RPGKeywords = AutoCompleteFileParse.ParseFile(Properties.Resources.RPG.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None));
            CPPKeywords = AutoCompleteFileParse.ParseFile(Properties.Resources.CPP.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None));

            Application.Run(Selector);
            if (Selector.SystemSelected)
                Application.Run(new Editor());
        }
    }
}
