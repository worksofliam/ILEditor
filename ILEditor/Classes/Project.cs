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

        public string GetBuildLibrary() => Settings.GetValue("buildlibrary");
        public void SetBuildLibrary(string Library) => Settings.SetValue("buildlibrary", Library);

        public string[] GetLocalProjectDeps() => Settings.GetValue("preprojectbuild").Split('|');
        public void SetLocalProjectDeps(string[] ProjectNames) => Settings.SetValue("preprojectbuild", String.Join("|", ProjectNames));

        public string[] GetStaticModules() => Settings.GetValue("staticmods").Split('|');
        public void SetStaticModules(string[] ObjectList) => Settings.SetValue("staticmods", String.Join("|", ObjectList));
    }
}
