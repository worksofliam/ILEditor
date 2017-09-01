using ILEditor.UserTools;
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

        public static ILEObject[] GetObjectList(string Lib, string Types = "*PGM *SRVPGM *MODULE")
        {
            string Line = ""; ILEObject Object = new ILEObject();
            List<ILEObject> Objects = new List<ILEObject>();
            List<string> commands = new List<string>();
            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("QUOTE RCMD DSPOBJD OBJ(" + Lib + "/*ALL) OBJTYPE(" + Types + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/objlist)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/DATA AS (SELECT ODOBNM, ODOBTP, ODOBAT, char(ODOBSZ) as ODOBSZ, ODOBTX, ODOBOW, ODSRCF, ODSRCL, ODSRCM FROM qtemp/objlist) WITH DATA') COMMIT(*NONE)");
            string file = DownloadMember("QTEMP", "DATA", "DATA", commands.ToArray());

            if (file != "")
            {
                foreach (string RealLine in File.ReadAllLines(file))
                {
                    if (RealLine.Trim() != "")
                    {
                        Object = new ILEObject();
                        Line = RealLine.PadRight(135);
                        Object.Library = Lib;
                        Object.Name = Line.Substring(0, 10).Trim();
                        Object.Type = Line.Substring(10, 8).Trim();
                        Object.Extension = Line.Substring(18, 10).Trim();
                        UInt32.TryParse(Line.Substring(28, 12).Trim(), out Object.SizeKB);
                        Object.Text = Line.Substring(40, 50).Trim();
                        Object.Owner = Line.Substring(90, 10).Trim();
                        Object.SrcSpf = Line.Substring(100, 10).Trim();
                        Object.SrcLib = Line.Substring(110, 10).Trim();
                        Object.SrcMbr = Line.Substring(120, 10).Trim();

                        Objects.Add(Object);
                    }
                }
            }
            else
            {
                return null;
            }

            return Objects.ToArray();
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

        public static Boolean CompileMember(Member MemberInfo, string TrueCmd = "")
        {
            string type, command;

            type = MemberInfo.GetExtension();
            command = IBMi.CurrentSystem.GetValue("DFT_" + type);
            if (command.Trim() != "")
            {
                if (TrueCmd != "") command = TrueCmd;
                Editor.TheEditor.SetStatus("Compiling with " + command + "...");
                command = IBMi.CurrentSystem.GetValue(command);
                if (command.Trim() != "")
                {
                    command = command.Replace("&OPENLIB", MemberInfo.GetLibrary());
                    command = command.Replace("&OPENSPF", MemberInfo.GetObject());
                    command = command.Replace("&OPENMBR", MemberInfo.GetMember());
                    command = command.Replace("&CURLIB", IBMi.CurrentSystem.GetValue("curlib"));
                    
                    IBMi.RunCommands(new string[] { "QUOTE RCMD " + command });
                    if (command.ToUpper().Contains("*EVENTF"))
                    {
                        Editor.TheEditor.SetStatus("Fetching errors..");
                        Editor.TheEditor.AddTool("Error Listing", new ErrorListing(MemberInfo.GetLibrary(), MemberInfo.GetMember()));
                    }
                    Editor.TheEditor.SetStatus("Compile finished.");
                }
            }

            return true;
        }

        public static string DownloadMember(string Lib, string Obj, string Mbr, string[] PreCommands = null)
        {
            string filetemp = Path.GetTempPath() + Mbr + "." + Obj;
            List<string> commands = new List<string>();

            //if (!File.Exists(filetemp)) File.Create(filetemp).Close();

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
