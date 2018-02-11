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
        private static List<string> QTEMPListing = new List<string>();
        //This method is used to determine whether a file in QTEMP needs to be deleted
        //If it's already exists in QTEMP, we delete it - otherwise we delete it next time
        private static void UsingQTEMPFiles(string[] Objects)
        {
            foreach (string Object in Objects) 
                if (QTEMPListing.Contains(Object))
                    IBMi.RemoteCommand("DLTOBJ OBJ(QTEMP/" + Object + ") OBJTYPE(*FILE)", false);
                else
                    QTEMPListing.Add(Object);
        }

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
            if (IBMi.IsConnected())
            {
                string Line = ""; BindingEntry Entry;
                List<BindingEntry> Entries = new List<BindingEntry>();
                if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
                
                UsingQTEMPFiles(new[] { "BNDDIR", "BNDDATA" });

                IBMi.RemoteCommand("DSPBNDDIR BNDDIR(" + Lib + "/" + Obj + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/BNDDIR)");
                IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/BNDDATA AS (SELECT BNOBNM, BNOBTP, BNOLNM, BNOACT, BNODAT, BNOTIM FROM qtemp/bnddir) WITH DATA ') COMMIT(*NONE)");
                string file = DownloadMember("QTEMP", "BNDDATA", "BNDDATA");

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
            else
            {
                return null;
            }
        }

        public static ILEObject[] GetObjectList(string Lib, string Types = "*PGM *SRVPGM *MODULE")
        {
            if (IBMi.IsConnected())
            {
                string Line = ""; ILEObject Object;
                List<ILEObject> Objects = new List<ILEObject>();
                if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");

                string FileA = 'O' + Lib, FileB = "T" + Lib;

                if (FileA.Length > 10)
                    FileA = FileA.Substring(0, 10);
                if (FileB.Length > 10)
                    FileB = FileB.Substring(0, 10);
                
                UsingQTEMPFiles(new[] { FileA, FileB });

                IBMi.RemoteCommand("DSPOBJD OBJ(" + Lib + "/*ALL) OBJTYPE(" + Types + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/" + FileA + ")");
                IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/" + FileB + " AS (SELECT ODOBNM, ODOBTP, ODOBAT, char(ODOBSZ) as ODOBSZ, ODOBTX, ODOBOW, ODSRCF, ODSRCL, ODSRCM FROM qtemp/" + FileA + " order by ODOBNM) WITH DATA') COMMIT(*NONE)");

                string file = DownloadMember("QTEMP", FileB, FileB);

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
            else
            {
                return null;
            }
        }

        private static readonly string[] IgnorePFs = new[] {
            "EVFTEMP",
            "QSQLTEMP"
        };
        public static ILEObject[] GetSPFList(string Lib)
        {
            List<ILEObject> SPFList = new List<ILEObject>();
            Lib = Lib.ToUpper();
            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
            if (IBMi.IsConnected())
            {
                string FileA = 'S' + Lib, FileB = "D" + Lib;

                if (FileA.Length > 10)
                    FileA = FileA.Substring(0, 10);
                if (FileB.Length > 10)
                    FileB = FileB.Substring(0, 10);

                UsingQTEMPFiles(new[] { FileA, FileB });

                IBMi.RemoteCommand("DSPFD FILE(" + Lib + "/*ALL) TYPE(*ATR) OUTPUT(*OUTFILE) FILEATR(*PF) OUTFILE(QTEMP/" + FileA + ")");
                IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/" + FileB + " AS (SELECT PHFILE, PHLIB FROM QTEMP/" + FileA + " WHERE PHDTAT = ''S'' order by PHFILE) WITH DATA') COMMIT(*NONE)");

                Editor.TheEditor.SetStatus("Fetching source-physical files for " + Lib + "...");
                string file = DownloadMember("QTEMP", FileB, FileB);

                if (file != "")
                {
                    Boolean validName = true;
                    string Line, Library, Object;
                    ILEObject Obj;
                    foreach (string RealLine in File.ReadAllLines(file))
                    {
                        if (RealLine.Trim() != "")
                        {
                            validName = true;
                            Line = RealLine.PadRight(31);
                            Object = Line.Substring(0, 10).Trim();
                            Library = Line.Substring(10, 10).Trim();

                            Obj = new ILEObject();
                            Obj.Library = Library;
                            Obj.Name = Object;

                            foreach (string Name in IgnorePFs)
                            {
                                if (Obj.Name.StartsWith(Name))
                                    validName = false;
                            }

                            if (validName)
                                SPFList.Add(Obj);
                        }
                    }
                }
                else
                {
                    return null;
                }
                Editor.TheEditor.SetStatus("Fetched source-physical files for " + Lib + ".");
            }
            else
            {
                string DirPath = GetLocalDir(Lib);
                if (Directory.Exists(DirPath)) {
                    foreach (string dir in Directory.GetDirectories(DirPath)) {
                        SPFList.Add(new ILEObject { Library = Lib, Name = Path.GetDirectoryName(dir) });
                    }
                }
                else
                {
                    return null;
                }
            }

            return SPFList.ToArray();
        }

        public static RemoteSource[] GetMemberList(string Lib, string Obj)
        {
            string Line, Object, Name, Desc, Type, RcdLen;
            List<RemoteSource> Members = new List<RemoteSource>();
            RemoteSource NewMember;

            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();

            if (IBMi.IsConnected())
            {

                if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
                Editor.TheEditor.SetStatus("Fetching members for " + Lib + "/" + Obj + "...");

                string TempName = 'M' + Obj;
                if (TempName.Length > 10)
                    TempName = TempName.Substring(0, 10);
                
                UsingQTEMPFiles(new[] { TempName, Obj });

                IBMi.RemoteCommand("DSPFD FILE(" + Lib + "/" + Obj + ") TYPE(*MBR) OUTPUT(*OUTFILE) OUTFILE(QTEMP/" + TempName + ")");
                IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/" + Obj + " AS (SELECT MBFILE, MBNAME, MBMTXT, MBSEU2, char(MBMXRL) as MBMXRL FROM QTEMP/" + TempName + " order by MBNAME) WITH DATA') COMMIT(*NONE)");

                string file = DownloadMember("QTEMP", Obj, Obj);

                if (file != "")
                {
                    foreach (string RealLine in File.ReadAllLines(file))
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
                                NewMember = new RemoteSource("", Lib, Object, Name, Type, true, int.Parse(RcdLen) - 12);
                                NewMember._Text = Desc;

                                Members.Add(NewMember);
                                MemberCache.AddMemberCache(Lib + "/" + Object + "." + Name, Type);
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }

                Editor.TheEditor.SetStatus("Fetched members for " + Lib + " / " + Obj + ".");
            }
            else
            {
                string DirPath = GetLocalDir(Lib, Obj);
                if (Directory.Exists(DirPath))
                {
                    foreach (string file in Directory.GetFiles(DirPath))
                    {
                        Type = Path.GetExtension(file).ToUpper();
                        if (Type.StartsWith(".")) Type = Type.Substring(1);
                        
                        NewMember = new RemoteSource(file, Lib, Obj, Path.GetFileNameWithoutExtension(file), Type);
                        NewMember._Text = "";
                        Members.Add(NewMember);
                    }
                }
                else
                {
                    return null;
                }
            }

            return Members.ToArray();
        }

        public static SpoolFile[] GetSpoolListing(string Lib, string Obj)
        {
            if (IBMi.IsConnected())
            {
                List<SpoolFile> Listing = new List<SpoolFile>();
                List<string> commands = new List<string>();

                string file = "";

                if (Lib != "" && Obj != "")
                {
                    Editor.TheEditor.SetStatus("Fetching spool file listing.. (can take a moment)");

                    UsingQTEMPFiles(new[] { "SPOOL" });
                    IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/SPOOL AS (SELECT Char(SPOOLED_FILE_NAME) as a, Char(COALESCE(USER_DATA, '''')) as b, Char(JOB_NAME) as c, Char(STATUS) as d, Char(FILE_NUMBER) as e FROM TABLE(QSYS2.OUTPUT_QUEUE_ENTRIES(''" + Lib + "'', ''" + Obj + "'', ''*NO'')) A WHERE USER_NAME = ''" + IBMi.CurrentSystem.GetValue("username").ToUpper() + "'' ORDER BY CREATE_TIMESTAMP DESC FETCH FIRST 25 ROWS ONLY) WITH DATA') COMMIT(*NONE)");
                    file = DownloadMember("QTEMP", "SPOOL", "SPOOL");
                    Editor.TheEditor.SetStatus("Finished fetching spool file listing.");
                }

                if (file != "")
                {
                    string Line, SpoolName, UserData, Job, Status, Number;
                    foreach (string RealLine in File.ReadAllLines(file))
                    {
                        if (RealLine.Trim() != "")
                        {
                            Line = RealLine.PadRight(75);
                            SpoolName = Line.Substring(0, 10).Trim();
                            UserData = Line.Substring(10, 10).Trim();
                            Job = Line.Substring(20, 28).Trim();
                            Status = Line.Substring(48, 15).Trim();
                            Number = Line.Substring(63, 11);

                            if (SpoolName != "")
                            {
                                Listing.Add(new SpoolFile(SpoolName, UserData, Job, Status, int.Parse(Number)));
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
            else
            {
                return null;
            }
        }

        public static Boolean CompileSource(RemoteSource SourceInfo, string TrueCmd = "")
        {
            if (IBMi.IsConnected())
            {
                List<string> commands = new List<string>();
                string type, command, filetemp = GetLocalFile("QTEMP", "JOBLOG", "JOBLOG"), name, library = "";

                if (SourceInfo != null)
                {
                    type = SourceInfo.GetExtension().ToUpper();
                    command = IBMi.CurrentSystem.GetValue("DFT_" + type);
                    if (command.Trim() != "")
                    {
                        if (TrueCmd != "") command = TrueCmd;
                        Editor.TheEditor.SetStatus("Compiling " + SourceInfo.GetName() + " with " + command + "...");

                        if (SourceInfo.GetFS() == FileSystem.IFS)
                            command += "_IFS";

                        command = IBMi.CurrentSystem.GetValue(command);
                        if (command.Trim() != "")
                        {
                            name = SourceInfo.GetName();
                            if (name.Length > 10) name.Substring(0, 10);

                            switch (SourceInfo.GetFS())
                            {
                                case FileSystem.QSYS:
                                    command = command.Replace("&OPENLIB", SourceInfo.GetLibrary());
                                    command = command.Replace("&OPENSPF", SourceInfo.GetObject());
                                    command = command.Replace("&OPENMBR", SourceInfo.GetName());
                                    library = SourceInfo.GetLibrary();
                                    break;
                                case FileSystem.IFS:
                                    command = command.Replace("&FILENAME", name);
                                    command = command.Replace("&FILEPATH", SourceInfo.GetRemoteFile());
                                    command = command.Replace("&BUILDLIB", IBMi.CurrentSystem.GetValue("buildLib"));

                                    library = IBMi.CurrentSystem.GetValue("buildLib");
                                    IBMi.SetWorkingDir(IBMi.CurrentSystem.GetValue("homeDir"));
                                    break;
                            }

                            command = command.Replace("&CURLIB", IBMi.CurrentSystem.GetValue("curlib"));
                            
                            IBMi.RemoteCommand($"CHGLIBL LIBL({ IBMi.CurrentSystem.GetValue("datalibl").Replace(',', ' ')}) CURLIB({ IBMi.CurrentSystem.GetValue("curlib") })");

                            if (!IBMi.RemoteCommand(command))
                            {
                                Editor.TheEditor.SetStatus("Compile finished unsuccessfully.");
                                if (command.ToUpper().Contains("*EVENTF"))
                                {
                                    Editor.TheEditor.SetStatus("Fetching errors..");
                                    Editor.TheEditor.AddTool("Error Listing", new ErrorListing(library, name), true);
                                }
                                if (IBMi.CurrentSystem.GetValue("fetchJobLog") == "true")
                                {
                                    UsingQTEMPFiles(new[] { "JOBLOG" });
                                    IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/JOBLOG AS (SELECT char(MESSAGE_TEXT) as a FROM TABLE(QSYS2.JOBLOG_INFO(''*'')) A WHERE MESSAGE_TYPE = ''DIAGNOSTIC'') WITH DATA') COMMIT(*NONE)");
                                    IBMi.DownloadFile(filetemp, "/QSYS.lib/QTEMP.lib/JOBLOG.file/JOBLOG.mbr");
                                }
                            }
                            else
                            {
                                Editor.TheEditor.SetStatus("Compile finished successfully.");
                            }
                        }
                    }
                }
                else
                {
                    Editor.TheEditor.SetStatus("Only remote members can be compiled.");
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetLocalDir(string Lib)
        {
            string LIBDir = Program.SOURCEDIR + "\\" + IBMi.CurrentSystem.GetValue("system") + "\\" + Lib;

            if (!Directory.Exists(LIBDir))
                Directory.CreateDirectory(LIBDir);

            return LIBDir;
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

        public static string DownloadSpoolFile(string Name, int Number, string Job)
        {
            //CPYSPLF FILE(NAME) JOB(B/A/JOB) TOSTMF('STMF')

            if (IBMi.IsConnected())
            {
                string filetemp = GetLocalFile("SPOOLS", Job.Replace('/', '.'), Name + '-' + Number.ToString(), "SPOOL");
                string remoteTemp = "/tmp/" + Name + ".spool";

                Editor.TheEditor.SetStatus("Downloading spool file " + Name + "..");
                IBMi.RemoteCommand("CPYSPLF FILE(" + Name + ") JOB(" + Job + ") SPLNBR(" + Number.ToString() + ") TOFILE(*TOSTMF) TOSTMF('" + remoteTemp + "') STMFOPT(*REPLACE)");

                if (!IBMi.DownloadFile(filetemp, remoteTemp))
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
            else
            {
                return "";
            }
        }

        public static string DownloadMember(string Lib, string Obj, string Mbr, string Ext = "")
        {
            if (Lib == "*CURLIB") Lib = IBMi.CurrentSystem.GetValue("curlib");
            string filetemp = GetLocalFile(Lib, Obj, Mbr, Ext);

            if (IBMi.IsConnected())
            {
                if (IBMi.DownloadFile(filetemp, "/QSYS.lib/" + Lib + ".lib/" + Obj + ".file/" + Mbr + ".mbr") == false)
                    return filetemp;
                else
                    return "";
            }
            else
            {
                Editor.TheEditor.SetStatus("Fetching existing local member.");
                if (File.Exists(filetemp))
                    return filetemp;
                else
                    return "";
            }
        }

        public static string DownloadFile(string RemoteFile)
        {
            string filetemp = Path.Combine(GetLocalDir("IFS"), Path.GetFileName(RemoteFile));

            if (IBMi.IsConnected())
            {
                if (IBMi.DownloadFile(filetemp, RemoteFile) == false)
                    return filetemp;
                else
                    return "";
            }
            else
            {
                Editor.TheEditor.SetStatus("Fetching existing local file.");
                if (File.Exists(filetemp))
                    return filetemp;
                else
                    return "";
            }
        }

        public static bool UploadMember(string Local, string Lib, string Obj, string Mbr)
        {
            Lib = Lib.ToUpper();
            Obj = Obj.ToUpper();
            Mbr = Mbr.ToUpper();
            
            if (IBMi.IsConnected()) 
                return IBMi.UploadFile(Local, "/QSYS.lib/" + Lib + ".lib/" + Obj + ".file/" + Mbr + ".mbr");
            else
            {
                return true;
            }
        }

        public static bool UploadFile(string Local, string Remote)
        {
            if (IBMi.IsConnected())
                return IBMi.UploadFile(Local, Remote);
            else
            {
                Editor.TheEditor.SetStatus("Saving locally only.");
                return true;
            }
        }
    }
}
