using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Forms;
using ILEditor.Classes;
using NetFwTypeLib;

namespace ILEditor
{
    static class Program
    {
        public static readonly string SYSTEMSDIR = Environment.GetEnvironmentVariable("ProgramData") + @"\ileditor";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Firewall.AuthorizeApplication("Notepad", @"C:\Windows\Notepad.exe",
                NET_FW_SCOPE_.NET_FW_SCOPE_ALL,
                NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY);

            LicenceKey LicenceInput = new LicenceKey();
            HostSelect Selector = new HostSelect();

            Application.Run(new Splash());

            if (!Licence.CheckExistsIsValid())
                Application.Run(LicenceInput);

            if (Licence.CheckExistsIsValid())
            {
                Application.Run(Selector);
                if (Selector.SystemSelected)
                    Application.Run(new Editor());
            }
        }
    }
}
