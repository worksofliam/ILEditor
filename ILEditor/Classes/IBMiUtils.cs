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

        public static BindingEntry[] GetBindingDirectory(string Lib, string Obj)
        {
            string Line = ""; BindingEntry Entry;
            List<BindingEntry> Entries = new List<BindingEntry>();
            List<string> commands = new List<string>();
            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("QUOTE RCMD DSPBNDDIR BNDDIR(" + Lib + "/" + Obj + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/BNDDIR)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/BNDDATA AS (SELECT BNOBNM, BNOBTP, BNOLNM, BNOACT, BNODAT, BNOTIM FROM qtemp/bnddir) WITH DATA ') COMMIT(*NONE)");
            string file = DownloadMember("QTEMP", "BNDDATA", "BNDDATA", commands.ToArray());

            if (file != "")
            {
                foreach (string RealLine in File.ReadAllLines(file))
                {
                    if (RealLine.Trim() != "")
                    {
                        Entry = new BindingEntry();
                        Line = RealLine.PadRight(50);
                        Entry.BindingLib = Lib;
                        Entry.BindingObj = Obj;
                        Entry.Name = Line.Substring(0, 10).Trim();
                        Entry.Type = Line.Substring(10, 7).Trim();
                        Entry.Library = Line.Substring(17, 10).Trim();
                        Entry.Activation = Line.Substring(27, 10).Trim();
                        Entry.CreationDate = Line.Substring(37, 6).Trim();
                        Entry.CreationTime = Line.Substring(43, 6).Trim();
                        Entries.Add(Entry);
                    }
                }
            }
            else
            {
                return null;
            }

            return Entries.ToArray();
        }

        public static ILEObject[] GetObjectList(string Lib, string Types = "*PGM *SRVPGM *MODULE")
        {
            string Line = ""; ILEObject Object;
            List<ILEObject> Objects = new List<ILEObject>();
            List<string> commands = new List<string>();
            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("QUOTE RCMD DSPOBJD OBJ(" + Lib + "/*ALL) OBJTYPE(" + Types + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/objlist)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/DATA AS (SELECT ODOBNM, ODOBTP, ODOBAT, char(ODOBSZ) as ODOBSZ, ODOBTX, ODOBOW, ODSRCF, ODSRCL, ODSRCM FROM qtemp/objlist order by ODOBNM) WITH DATA') COMMIT(*NONE)");
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

        public static ILEObject[] GetSPFList(string Lib)
        {
            List<ILEObject> SPFList = new List<ILEObject>();
            List<string> commands = new List<string>();

            Lib = Lib.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("QUOTE RCMD DSPFD FILE(" + Lib + "/*ALL) TYPE(*ATR) OUTPUT(*OUTFILE) FILEATR(*PF) OUTFILE(QTEMP/SPFLIST)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/DATA AS (SELECT PHFILE, PHLIB FROM QTEMP/SPFLIST WHERE PHDTAT = ''S'' order by PHFILE) WITH DATA') COMMIT(*NONE)");

            Editor.TheEditor.SetStatus("Fetching source-physical files for " + Lib + "...");
            string file = DownloadMember("QTEMP", "DATA", "DATA", commands.ToArray());

            if (file != "")
            {
                string Line, Library, Object;
                ILEObject Obj;
                foreach (string RealLine in File.ReadAllLines(file))
                {
                    if (RealLine.Trim() != "")
                    {
                        Line = RealLine.PadRight(31);
                        Object = Line.Substring(0, 10).Trim();
                        Library = Line.Substring(10, 10).Trim();

                        Obj = new ILEObject();
                        Obj.Library = Library;
                        Obj.Name = Object;

                        SPFList.Add(Obj);
                    }
                }
            }
            else
            {
                return null;
            }

            Editor.TheEditor.SetStatus("Fetched source-physical files for " + Lib + ".");

            return SPFList.ToArray();
        }

        public static Member[] GetMemberList(string Lib, string Obj)
        {
            List<Member> Members = new List<Member>();
            List<string> commands = new List<string>();

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            commands.Add("QUOTE RCMD DSPFD FILE(" + Lib + "/" + Obj + ") TYPE(*MBR) OUTPUT(*OUTFILE) OUTFILE(QTEMP/MEMBERS)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/DATA AS (SELECT MBFILE, MBNAME, MBMTXT, MBSEU2, char(MBMXRL) as MBMXRL FROM QTEMP/MEMBERS order by MBNAME) WITH DATA') COMMIT(*NONE)");

            Editor.TheEditor.SetStatus("Fetching members for " + Lib + "/" + Obj + "...");
            string file = DownloadMember("QTEMP", "DATA", "DATA", commands.ToArray());

            string Line, Object, Name, Desc, Type, RcdLen;

            if (file != "")
            {

                Member NewMember;
                foreach(string RealLine in File.ReadAllLines(file))
                {
                    if (RealLine.Trim() != "")
                    {
                        Line = RealLine.PadRight(90);
                        Object = Line.Substring(0, 10).Trim();
                        Name = Line.Substring(10, 10).Trim();
                        Desc = Line.Substring(20, 50).Trim();
                        Type = Line.Substring(70, 10).Trim();
                        RcdLen = Line.Substring(80, 7).Trim();

                        if (Name != "")
                        {
                            NewMember = new Member("", Lib, Object, Name, Type, true, int.Parse(RcdLen) - 12);
                            NewMember._Text = Desc;

                            Members.Add(NewMember);
                            MemberCache.AddMember(Lib + "/" + Object + "." + Name, Type);
                        }
                    }
                }
            }
            else
            {
                return null;
            }

            Editor.TheEditor.SetStatus("Fetched members for " + Lib + " / " + Obj + ".");

            return Members.ToArray();
        }

        public static SpoolFile[] GetSpoolListing()
        {
            List<SpoolFile> Listing = new List<SpoolFile>();
            List<string> commands = new List<string>();

            //string SQLFile = IBMiUtils.GetLocalFile("QTEMP", "SPOOL", "SPOOL");
            //string SQLFileContent = "CREATE TABLE QTEMP/SPOOL AS (SELECT Char(SPOOLED_FILE_NAME) as a, Char(USER_DATA) as b, Char(JOB_NAME) as c FROM QSYS2.OUTPUT_QUEUE_ENTRIES ORDER BY CREATE_TIMESTAMP DESC FETCH FIRST 100 ROWS ONLY) WITH DATA";
            //File.WriteAllText(SQLFile, SQLFileContent);

            //commands.Add("put \"" + SQLFile + "\" \"/tmp/LALLANSpool.sql\"");
            //commands.Add("QUOTE RCMD RUNSQLSTM SRCSTMF('/tmp/LALLANSpool.sql') COMMIT(*NONE)");
            commands.Add("QUOTE RCMD RUNSQL SQL('CREATE TABLE QTEMP/SPOOL AS (SELECT Char(SPOOLED_FILE_NAME) as a, Char(COALESCE(USER_DATA, '''')) as b, Char(JOB_NAME) as c, Char(STATUS) as d FROM QSYS2.OUTPUT_QUEUE_ENTRIES WHERE USER_NAME = ''" + IBMi.CurrentSystem.GetValue("username").ToUpper() + "'' ORDER BY CREATE_TIMESTAMP DESC FETCH FIRST 100 ROWS ONLY) WITH DATA') COMMIT(*NONE)");

            Editor.TheEditor.SetStatus("Fetching spool file listing.. (can take a moment)");
            string file = DownloadMember("QTEMP", "SPOOL", "SPOOL", commands.ToArray());
            Editor.TheEditor.SetStatus("Finished fetching spool file listing.");

            if (file != "")
            {
                string Line, SpoolName, UserData, Job, Status;
                foreach (string RealLine in File.ReadAllLines(file))
                {
                    if (RealLine.Trim() != "")
                    {
                        Line = RealLine.PadRight(65);
                        SpoolName = Line.Substring(0, 10).Trim();
                        UserData = Line.Substring(10, 10).Trim();
                        Job = Line.Substring(20, 28).Trim();
                        Status = Line.Substring(48, 15).Trim();

                        if (SpoolName != "")
                        {
                            Listing.Add(new SpoolFile(SpoolName, UserData, Job, Status));
                        }
                    }
                }

                return (Listing.Count > 0 ? Listing.ToArray() : null);
            }
            else
            {
                return null;
            }

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
                        Editor.TheEditor.AddTool("Error Listing", new ErrorListing(MemberInfo.GetLibrary(), MemberInfo.GetMember()), true);
                    }
                    Editor.TheEditor.SetStatus("Compile finished.");
                }
            }

            return true;
        }

        public static string GetLocalDir(string Lib, string Obj)
        {
            string SPFDir = Program.SOURCEDIR + "\\" + IBMi.CurrentSystem.GetValue("system") + "\\" + Lib + "\\" + Obj;

            if (!Directory.Exists(SPFDir))
                Directory.CreateDirectory(SPFDir);

            return SPFDir;
        }

        public static string GetLocalFile(string Lib, string Obj, string Mbr, string Ext = "")
        {
            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();
            Mbr = Mbr.ToUpper();
            if (Ext == "")
                Ext = "mbr";

            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

            string SPFDir = Program.SOURCEDIR + "\\" + IBMi.CurrentSystem.GetValue("system") + "\\" + Lib + "\\" + Obj;

            if (!Directory.Exists(SPFDir))
                Directory.CreateDirectory(SPFDir);

            return SPFDir + "\\" + Mbr.ToUpper() + "." + Ext.ToLower();
        }

        public static string DownloadSpoolFile(String Name, string Job)
        {
            //CPYSPLF FILE(NAME) JOB(B/A/JOB) TOSTMF('STMF')

            string filetemp = GetLocalFile("QTEMP", "SPOOLS", "NAME", "SPOOL");
            string remoteTemp = "/tmp/" + Name + ".spool";
            List<string> commands = new List<string>();

            Editor.TheEditor.SetStatus("Downloading spool file " + Name + "..");
            commands.Add("QUOTE RCMD CPYSPLF FILE(" + Name + ") JOB(" + Job + ") TOFILE(*TOSTMF) TOSTMF('" + remoteTemp + "') STMFOPT(*REPLACE)");
            commands.Add("recv \"" + remoteTemp + "\" \"" + filetemp + "\"");

            if (IBMi.RunCommands(commands.ToArray()) == false)
            {
                Editor.TheEditor.SetStatus("Downloaded spool file " + Name + ".");
                return filetemp;
            }
            else
            {
                Editor.TheEditor.SetStatus("Failed downloading spool file " + Name + ".");
                return "";
            }
        }

        public static string DownloadMember(string Lib, string Obj, string Mbr, string[] PreCommands = null, string Ext = "")
        {
            string filetemp = GetLocalFile(Lib, Obj, Mbr, Ext);
            List<string> commands = new List<string>();

            //if (!File.Exists(filetemp)) File.Create(filetemp).Close();
            
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
