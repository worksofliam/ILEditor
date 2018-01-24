using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class Project
    {
        public enum InitLang
        {
            None, RPG, C
        }
        public enum Type
        {
            PGM, MOD, SRVPGM
        }

        public static List<Project> Projects = new List<Project>();
        public static Project GetProject(string Name)
        {
            if (Projects.Count() > 0) {
                foreach (Project proj in Projects)
                    if (proj.GetName() == Name) return proj;

                return null;
            }
            else
                return null;
        }

        private Config Settings;
        private string Dir; //Local project directory
        private Type OutputType; //Compile type

        private string SourceDir, HeadersDir;

        //When creating a new project
        public Project(string ProjectDir, string ObjectName)
        {
            this.Dir = ProjectDir;
            Directory.CreateDirectory(this.Dir);

            Settings = new Config(Path.Combine(this.Dir, "project.ileprj"));

            Settings.SetValue("objectname", ObjectName); //Used for CRTPGM and CRTSRVPGM

            //Build settings
            Settings.SetValue("preprojectbuild", ""); //OTHER PROJECTS TO BUILD BEFORE THIS ONE
            Settings.SetValue("staticmods", ""); //EXTRA *MODs to pass into CRTPGM

            Settings.SetValue("sqlcommit", "*NONE");
            Settings.SetValue("debugview", "*SOURCE");

            this.HeadersDir = Path.Combine(this.Dir, "Headers");
            this.SourceDir = Path.Combine(this.Dir, "Source");
        }

        public void Init(Type ProjectType, InitLang InitLanguage)
        {
            Settings.SetValue("projecttype", ProjectType.ToString());
            this.OutputType = ProjectType;

            Directory.CreateDirectory(this.HeadersDir);
            Directory.CreateDirectory(this.SourceDir);

            string SourceFile;
            switch (InitLanguage)
            {
                case InitLang.RPG:
                    SourceFile = Path.Combine(this.SourceDir, this.GetBuildObject() + ".sqlrpgle");
                    switch (ProjectType)
                    {
                        case Type.PGM:
                            File.WriteAllLines(SourceFile, new[]
                            {
                                "**FREE", "",
                                "Dcl-Pi " + this.GetBuildObject() + ";", "End-Pi;",
                                "", "Return;"
                            });
                            break;

                        case Type.MOD:
                        case Type.SRVPGM:
                            File.WriteAllLines(SourceFile, new[]
                            {
                                "**FREE", "", "Ctl-Opt NoMain;", "",
                                "Dcl-Proc ProcName Export;",
                                "End-Pi;"
                            });
                            File.WriteAllLines(Path.Combine(this.SourceDir, this.GetBuildObject() + ".sqlrpgle"), new[]
                            {
                                "**FREE", "",
                                "Dcl-Pr ProcName ExtProc('PROCNAME');",
                                "End-Pi;"
                            });
                            break;
                    }
                    break;
                case InitLang.C:
                    break;
            }

            if (ProjectType == Type.SRVPGM)
            {
                File.WriteAllLines(Path.Combine(this.HeadersDir, this.GetBuildObject() + ".bnd"), new[]
                {
                    "STRPGMEXP PGMLVL(*CURRENT) SIGNATURE('" + this.GetBuildObject() + "')",
                    "  EXPORT SYMBOL('PROCNAME')",
                    "ENDPGMEXP"
                });
            }
        }
        
        //Pass in directory when loading existing project
        public Project(string Directory)
        {
            this.Dir = Directory;
            Settings = new Config(Path.Combine(this.Dir, "project.ileprj"));

            this.OutputType = (Type)Enum.Parse(typeof(Type), Settings.GetValue("projecttype"));
            this.HeadersDir = Path.Combine(this.Dir, "Headers");
            this.SourceDir = Path.Combine(this.Dir, "Source");
        }

        public string GetName() => Path.GetFileName(this.Dir);
        public string GetDirectory() => this.Dir;
        public string[] GetHeaderFiles() => Directory.GetFiles(this.HeadersDir);
        public string[] GetSourceFiles() => Directory.GetFiles(this.SourceDir);

        public Type GetProjectType() => this.OutputType;

        public string GetCommitmentControl() => Settings.GetValue("sqlcommit");
        public void SetCommitmentControl(string Value) => Settings.SetValue("sqlcommit", Value);

        public string GetDebugView() => Settings.GetValue("debugview");
        public void SetDebugView(string Value) => Settings.SetValue("debugview", Value);

        public string GetBuildObject() => Settings.GetValue("objectname");
        public void SetBuildObject(string Object) => Settings.SetValue("objectname", Object);

        public static string GetBuildLibrary() => IBMi.CurrentSystem.GetValue("buildlibrary");
        public static string GetUploadDir() => IBMi.CurrentSystem.GetValue("projuploaddir");

        public string[] GetLocalProjectDeps() => Settings.GetValue("preprojectbuild").Split('|');
        public void SetLocalProjectDeps(string[] ProjectNames) => Settings.SetValue("preprojectbuild", String.Join("|", ProjectNames));

        public string[] GetStaticModules() => Settings.GetValue("staticmods").Split('|');
        public void SetStaticModules(string[] ObjectList) => Settings.SetValue("staticmods", String.Join("|", ObjectList));

        private static List<string> BuildMessages = new List<string>();
        public static string[] GetBuildMessages() => BuildMessages.ToArray();
        public static void PreProjectBuild()
        {
            BuildMessages.Clear();
            IBMi.RemoteCommand("CRTSRCPF FILE(" + GetBuildLibrary() + "/HEADERS) RCDLEN(112)", false);
            IBMi.RemoteCommand("CRTSRCPF FILE(" + GetBuildLibrary() + "/SOURCE) RCDLEN(112)", false);
        }
        
        public bool Build()
        {
            string[] ProjectDeps = GetLocalProjectDeps();

            foreach (string ProjDeps in ProjectDeps)
            {
                if (ProjDeps == "") continue;
                if (GetProject(ProjDeps) == null)
                {
                    BuildMessages.Add("Local project " + ProjDeps + " is missing but is required for " + this.GetName() + " to build.");
                    return false;
                }
            }

            bool DepBuildResult = true;
            foreach (string ProjDeps in ProjectDeps)
            {
                if (ProjDeps == "") continue;
                if (DepBuildResult == true)
                {
                    BuildMessages.Add("Building " + ProjDeps + " as required for " + this.GetName());
                    if (GetProject(ProjDeps).Build() == false)
                    {
                        DepBuildResult = false;
                    }
                }
            }

            if (DepBuildResult == false)
            {
                BuildMessages.Add(this.GetName() + " didn't initiate build due to failing local project dependancy builds.");
                return false;
            }

            BuildMessages.Add("Building " + this.GetName());
            Editor.TheEditor.SetStatus("Building " + this.GetName());

            //TODO CUSTOMISE REMOTE DIR
            string ProjectDirectory = Project.GetUploadDir() + this.GetName() + "/";
            IBMi.CreateDirecory(ProjectDirectory);

            IBMi.UploadFiles(ProjectDirectory + "/Headers/", this.GetHeaderFiles());
            IBMi.UploadFiles(ProjectDirectory + "/Source/", this.GetSourceFiles());

            IBMi.SetWorkingDir(ProjectDirectory);

            List<string> Commands = new List<string>(), CurModList = new List<string>();
            string Name, Ext;
            foreach (String FilePath in this.GetSourceFiles())
            {
                Name = Path.GetFileNameWithoutExtension(FilePath);
                Ext = Path.GetExtension(FilePath).Substring(1);
                
                switch (Ext.ToUpper())
                {
                    case "RPGLE":
                        CurModList.Add(GetBuildLibrary() + "/" + Name);
                        Commands.Add("CRTRPGMOD MODULE(" + GetBuildLibrary() + "/" + Name + ") SRCSTMF('Source/" + Name + "." + Ext + "') OPTION(*EVENTF) DBGVIEW(" + GetDebugView() + ")");
                        break;
                    case "SQLRPGLE":
                        CurModList.Add(GetBuildLibrary() + "/" + Name);
                        Commands.Add("CRTSQLRPGI OBJ(" + GetBuildLibrary() + "/" + Name + ") SRCSTMF('Source/" + Name + "." + Ext + "') COMMIT(" + GetCommitmentControl() + ") OBJTYPE(*MODULE) OPTION(*EVENTF) DBGVIEW(" + GetDebugView() + ") RPGPPOPT(*LVL2)");
                        break;
                    case "C":
                        CurModList.Add(GetBuildLibrary() + "/" + Name);
                        Commands.Add("CRTCMOD MODULE(" + GetBuildLibrary() + "/" + Name + ") SRCSTMF('Source/" + Name + "." + Ext + "') DBGVIEW(" + GetDebugView() + ") OPTION(*EVENTF)");
                        break;
                    case "SQLC":
                        CurModList.Add(GetBuildLibrary() + "/" + Name);
                        Commands.Add("CRTSQLCI OBJ(" + GetBuildLibrary() + "/" + Name + ") SRCSTMF('Source/" + Name + "." + Ext + "') COMMIT(c) OBJTYPE(*MODULE) OPTION(*EVENTF) DBGVIEW(" + GetDebugView() + ")");
                        break;
                    case "CLLE":
                        CurModList.Add(GetBuildLibrary() + "/" + Name);
                        Commands.Add("CPYFRMSTMF FROMSTMF('Source/" + Name + "." + Ext + "') TOMBR('/QSYS.lib/" + GetBuildLibrary() + ".lib/SOURCE.file/" + Name + ".mbr') MBROPT(*REPLACE)");
                        Commands.Add("CRTCLMOD MOD(" + GetBuildLibrary() + "/" + Name + ") SRCFILE(" + GetBuildLibrary() + "/SOURCE) OPTION(*EVENTF) DBGVIEW(" + GetDebugView() + ")");
                        break;

                    case "CMD":
                        Commands.Add("CPYFRMSTMF FROMSTMF('Source/" + Name + "." + Ext + "') TOMBR('/QSYS.lib/" + GetBuildLibrary() + ".lib/SOURCE.file/" + Name + ".mbr') MBROPT(*REPLACE)");
                        Commands.Add("CRTCMD CMD(" + GetBuildLibrary() + "/" + Name + ") PGM(" + GetBuildLibrary() + "/" + Name + ") SRCFILE(" + GetBuildLibrary() + "/SOURCE)");
                        break;

                    case "DSPF":
                        Commands.Add("CPYFRMSTMF FROMSTMF('Source/" + Name + "." + Ext + "') TOMBR('/QSYS.lib/" + GetBuildLibrary() + ".lib/SOURCE.file/" + Name + ".mbr') MBROPT(*REPLACE)");
                        Commands.Add("CRTCMD CMD(" + GetBuildLibrary() + "/" + Name + ") SRCFILE(" + GetBuildLibrary() + "/SOURCE)");
                        break;

                    case "SQL":
                        Commands.Add("RUNSQLSTM SRCSTMF('Source/" + Name + "." + Ext + "') COMMIT('Source/" + Name + "." + Ext + "') NAMING(*SQL)");
                        break;
                }
            }

            List<string> ModList = new List<string>();
            ModList.AddRange(CurModList);
            ModList.AddRange(this.GetStaticModules());

            switch (this.GetProjectType())
            {
                case Type.PGM:
                    Commands.Add("CRTPGM PGM(" + GetBuildLibrary() + "/" + this.GetBuildObject() + ") MODULE(" + String.Join(" ", ModList) + ") ENTMOD(*PGM) ACTGRP(*NEW)");
                    break;

                case Type.MOD:
                    //Create modules only, no need to make any pgms
                    break;

                case Type.SRVPGM:
                    string BinderSource = "BND" + GetBuildObject();
                    if (BinderSource.Length > 10) BinderSource = BinderSource.Substring(0, 10);

                    Commands.Add("CPYFRMSTMF FROMSTMF('Headers/" + this.GetBuildObject() + ".bnd') TOMBR('/QSYS.lib/" + GetBuildLibrary() + ".lib/HEADERS.file/" + BinderSource + ".mbr') MBROPT(*REPLACE)");
                    Commands.Add("CRTSRVPGM SRVPGM(" + GetBuildLibrary() + "/" + this.GetBuildObject() + ") MODULE(" + String.Join(" ", ModList) + ") SRCFILE(" + GetBuildLibrary() + "/HEADERS) SRCMBR(" + BinderSource + ")");
                    break;
            }

            string Response;
            foreach (string Command in Commands)
            {
                Response = IBMi.RemoteCommandResponse(Command);

                if (Response != "")
                {
                    BuildMessages.Add(Response);
                    BuildMessages.Add("Ending " + this.GetName() + " build.");
                    return false;
                }
            }


            BuildMessages.Add("Finished build.");
            Editor.TheEditor.SetStatus("Build of " + this.GetName() + " finished.");
            return true;
        }
    }
}
