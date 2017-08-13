using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class IBMiUtils
    {
        public static Boolean IsValueObjectName(string Name)
        {
            if (Name.Trim() == "")
                return false;

            if (Name.Length > 10)
                return false;

            return true;
        }

        public static string[] GetMemberList(string Lib, string Obj)
        {
            List<string> commands = new List<string>();

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("cd /QSYS.lib/" + Lib + ".lib/" + Obj + ".file");
            commands.Add("ls");

            if (IBMi.RunCommands(commands.ToArray()) == false)
            {
                return IBMi.GetListing();
            }
            else
            {
                return null;
            }
        }

        public static string DownloadMember(string Lib, string Obj, string Mbr)
        {
            string filetemp = Path.GetTempPath() + Mbr + "." + Obj;
            List<string> commands = new List<string>();

            if (!File.Exists(filetemp)) File.Create(filetemp).Close();

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();
            Mbr = Mbr.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
            
            commands.Add("cd /QSYS.lib");
            commands.Add("recv \"" + Lib + ".lib/" + Obj + ".file/" + Mbr + ".mbr\" \"" + filetemp + "\"");

            if (IBMi.RunCommands(commands.ToArray()) == false)
            {
                return filetemp;
            }
            else
            {
                return "";
            }
        }

        public static bool UploadMember(string Local, string Lib, string Obj, string Mbr)
        {
            List<string> commands = new List<string>();

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();
            Mbr = Mbr.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
            
            commands.Add("cd /QSYS.lib");
            commands.Add("put \"" + Local + "\" \"" + Lib + ".lib/" + Obj + ".file/" + Mbr + ".mbr\"");

            //Returns true if successful
            return !IBMi.RunCommands(commands.ToArray());
        }
    }
}
