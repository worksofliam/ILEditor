using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static string[][] GetMemberList(string Lib, string Obj)
        {
            List<string[]> Members = new List<string[]>();
            List<string> commands = new List<string>();

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("QUOTE RCMD DSPFD FILE(" + Lib + "/" + Obj + ") TYPE(*MBRLIST) OUTPUT(*OUTFILE) OUTFILE(QTEMP/MEMBERS)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/DATA AS (SELECT MLNAME, MLMTXT, MLSEU2 FROM QTEMP/MEMBERS) WITH DATA') COMMIT(*NONE)");
            string file = DownloadMember("QTEMP", "DATA", "DATA", commands.ToArray());

            string Line, Name, Desc, Type;

            if (file != "")
            {
                foreach(string RealLine in File.ReadAllLines(file))
                {
                    if (RealLine.Trim() != "")
                    {
                        Line = RealLine.PadRight(70);
                        Name = Line.Substring(0, 10).Trim();
                        Desc = Line.Substring(10, 50).Trim();
                        Type = Line.Substring(60, 10).Trim();

                        Members.Add(new string[3] { Name, Type, Desc });
                    }
                }
            }
            else
            {
                return null;
            }

            return Members.ToArray();
        }

        public static string DownloadMember(string Lib, string Obj, string Mbr, string[] PreCommands = null)
        {
            string filetemp = Path.GetTempPath() + Mbr + "." + Obj;
            List<string> commands = new List<string>();

            if (!File.Exists(filetemp)) File.Create(filetemp).Close();

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();
            Mbr = Mbr.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
            
            if (PreCommands != null)
                commands.AddRange(PreCommands);

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
