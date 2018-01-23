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
        public Project(string ProjectDir, string ObjectName, Type ProjectType)
        {
            this.Dir = ProjectDir;
            Directory.CreateDirectory(this.Dir);

            Settings = new Config(Path.Combine(this.Dir, "project.ileprj"));

            Settings.SetValue("objectname", ObjectName); //Used for CRTPGM and CRTSRVPGM

            //Build settings
            Settings.SetValue("projecttype", ProjectType.ToString());
            Settings.SetValue("buildlibrary", "QTEMP");
            Settings.SetValue("preprojectbuild", ""); //OTHER PROJECTS TO BUILD BEFORE THIS ONE
            Settings.SetValue("staticmods", ""); //EXTRA *MODs to pass into CRTPGM
            
            this.OutputType = ProjectType;

            this.HeadersDir = Path.Combine(this.Dir, "Headers");
            this.SourceDir = Path.Combine(this.Dir, "Source");

            this.Init(InitLang.None);
        }

        public void Init(InitLang InitLanguage)
        {
            Directory.CreateDirectory(this.HeadersDir);
            Directory.CreateDirectory(this.SourceDir);

            switch (InitLanguage)
            {
                case InitLang.RPG:
                    File.WriteAllLines(Path.Combine(this.SourceDir, Settings.GetValue("objectname") + ".sqlrpgle"), new[]
                    {
                        "**FREE", "",
                        "Dcl-Pi " + Settings.GetValue("objectname") + ";", "End-Pi;",
                        "", "Return;"
                    });
                    break;
                case InitLang.C:
                    break;
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

        public string GetBuildObject() => Settings.GetValue("objectname");
        public void SetBuildObject(string Object) => Settings.SetValue("objectname", Object);

        public string GetBuildLibrary() => Settings.GetValue("buildlibrary");
        public void SetBuildLibrary(string Library) => Settings.SetValue("buildlibrary", Library);

        public string[] GetLocalProjectDeps() => Settings.GetValue("preprojectbuild").Split('|');
        public void SetLocalProjectDeps(string[] ProjectNames) => Settings.SetValue("preprojectbuild", String.Join("|", ProjectNames));

        public string[] GetStaticModules() => Settings.GetValue("staticmods").Split('|');
        public void SetStaticModules(string[] ObjectList) => Settings.SetValue("staticmods", String.Join("|", ObjectList));

        private static List<string> BuildMessages = new List<string>();
        public static string[] GetBuildMessages() => BuildMessages.ToArray();
        public static void PreProjectBuild() => BuildMessages.Clear();
        
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
            string ProjectDirectory = "/home/" + IBMi.CurrentSystem.GetValue("username") + "/" + this.GetName() + "/";
            IBMi.CreateDirecory(ProjectDirectory);

            IBMi.UploadFiles(ProjectDirectory + "/Headers/", this.GetHeaderFiles());
            IBMi.UploadFiles(ProjectDirectory + "/Source/", this.GetSourceFiles());

            IBMi.SetWorkingDir(ProjectDirectory);

            List<string> Commands = new List<string>();
            string Name, Ext;
            bool hasSPF = false;
            foreach (String FilePath in this.GetSourceFiles())
            {
                Name = Path.GetFileNameWithoutExtension(FilePath);
                Ext = Path.GetExtension(FilePath).Substring(1);
                
                switch (Ext.ToUpper())
                {
                    case "RPGLE":
                        Commands.Add("CRTRPGMOD MODULE(" + this.GetBuildLibrary() + "/" + Name + ") SRCSTMF('Source/" + Name + "." + Ext + "') OPTION(*EVENTF)");
                        break;
                    case "SQLRPGLE":
                        Commands.Add("CRTSQLRPGI OBJ(" + this.GetBuildLibrary() + "/" + Name + ") SRCSTMF('Source/" + Name + "." + Ext + "') COMMIT(*NONE) OBJTYPE(*MODULE) OPTION(*EVENTF *XREF)");
                        break;
                    case "CLLE":
                        if (hasSPF != true)
                        {
                            Commands.Add("CRTSRCPF FILE(" + this.GetBuildLibrary() + "/SOURCE) RCDLEN(112)");
                            hasSPF = true;
                        }
                        Commands.Add("CPYFRMSTMF FROMSTMF('Source/" + Name + "." + Ext + "') TOMBR('/QSYS.lib/" + this.GetBuildLibrary() + ".lib/SOURCE.file/" + Name + ".mbr') MBROPT(*ADD)");
                        Commands.Add("CRTCLMOD MOD(" + this.GetBuildLibrary() + "/" + Name + ") SRCFILE(" + this.GetBuildLibrary() + "/SOURCE) OPTION(*EVENTF)");
                        break;
                }
            }

            switch (this.GetProjectType())
            {
                case Type.PGM:
                    List<string> ModList = new List<string>();
                    foreach (string Mod in this.GetSourceFiles())
                        ModList.Add(this.GetBuildLibrary() + "/" + Path.GetFileNameWithoutExtension(Mod));

                    ModList.AddRange(this.GetStaticModules());

                    Commands.Add("CRTPGM PGM(" + this.GetBuildLibrary() + "/" + this.GetBuildObject() + ") MODULE(" + String.Join(" ", ModList) + ") ENTMOD(*PGM) ACTGRP(*NEW)");
                    break;

                case Type.MOD:
                    //Create modules only, no need to make any pgms
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
