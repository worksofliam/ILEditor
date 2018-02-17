using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.LanguageTools
{
    public enum StorageType
    {
        Normal,
        Struct,
        File,
        Const,
        Subroutine
    }

    public class Function
    {
        private string Name;
        private int Line;
        private List<Variable> Variables;

        public Function(string Name, int LineNumber)
        {
            this.Name = Name;
            this.Variables = new List<Variable>();
            this.Line = LineNumber;
        }

        public void AddVariable(Variable var)
        {
            this.Variables.Add(var);
        }

        public string GetName() => this.Name;
        public Variable[] GetVariables() => this.Variables.ToArray();
        public int GetLineNumber() => this.Line;
    }

    public class Variable
    {
        private string Name;
        private string Type;
        private StorageType varType;
        private int Line;

        private List<Variable> Members; //Subfields for a struct

        public Variable(string Name, string Type, StorageType varType, int LineNumber)
        {
            this.Name = Name;
            this.Type = Type;
            this.varType = varType;
            this.Line = LineNumber;

            if (varType == StorageType.Struct)
                Members = new List<Variable>();
        }

        public string GetName() => this.Name;
        public new string GetType() => this.Type;
        public int GetLine() => this.Line;
        public StorageType GetStorageType() => this.varType;
        public Variable[] GetMembers() => this.Members.ToArray();

        public void AddMember(Variable Member) => this.Members.Add(Member);
    }
}
