using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILEditor.Forms;

namespace ILEditor
{
    static class Program
    {
        public static readonly string SYSTEMSDIR = "systems";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HostSelect Selector = new HostSelect();
            Application.Run(Selector);
            if (Selector.SystemSelected)
                Application.Run(new Editor());
        }
    }
}
