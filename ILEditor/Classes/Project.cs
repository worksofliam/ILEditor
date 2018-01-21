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
            RPG, C
        }
        public enum Type
        {
            PGM, MOD, SRVPGM
        }

        public static List<Project> Projects = new List<Project>();

        private string Name; //Project name
        private string Dir; //Local project directory
        private string OutputName; //Object name
        private string BuildLibrary; //Library in which source and objects are created
        private Type OutputType; //Compile type

        private string SourceDir, HeadersDir;

        public Project(string ProjectName, string ProjectDir, string ObjectName, string BuildLibrary, Type ProjectType)
        {
            this.Name = ProjectName;
            this.Dir = ProjectDir;
            this.OutputName = ObjectName;
            this.BuildLibrary = BuildLibrary;
            this.OutputType = ProjectType;

            this.HeadersDir = Path.Combine(this.Dir, "Headers");
            this.SourceDir = Path.Combine(this.Dir, "Source");
        }

        public void Init(InitLang InitLanguage)
        {
            Directory.CreateDirectory(this.Dir);
            File.Create(Path.Combine(this.Dir, "project.ileprj")).Close();

            //Create init config here

            Directory.CreateDirectory(this.HeadersDir);
            Directory.CreateDirectory(this.SourceDir);

            switch (InitLanguage)
            {
                case InitLang.RPG:
                    File.WriteAllLines(Path.Combine(this.SourceDir, this.OutputName + ".sqlrpgle"), new[]
                    {
                        "**FREE", "",
                        "Dcl-Pi " + this.OutputName + ";", "End-Pi;",
                        "", "Return"
                    });
                    break;
                case InitLang.C:
                    break;
            }
        }

    }
}
