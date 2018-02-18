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
        Prototype,
        File,
        Const,
        Struct,
        Subroutine
    }

    public class Function
    {
        private string Name;
        private int Line;
        private string ReturnType;
        private List<Variable> Variables;

        public Function(string Name, int LineNumber)
        {
            this.Name = Name;
            this.Variables = new List<Variable>();
            this.Line = LineNumber;
            this.ReturnType = "Void";
        }

        public void AddVariable(Variable var) => this.Variables.Add(var);
        public void SetReturnType(string Type) => this.ReturnType = Type;

        public string GetName() => this.Name;
        public Variable[] GetVariables() => this.Variables.ToArray();
        public int GetLineNumber() => this.Line;
        public string GetReturnType() => this.ReturnType;
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

            if (varType == StorageType.Struct || varType == StorageType.Prototype)
                Members = new List<Variable>();
        }

        public string GetName() => this.Name;
        public new string GetType() => this.Type;
        public int GetLine() => this.Line;
        public StorageType GetStorageType() => this.varType;
        public Variable[] GetMembers() => this.Members.ToArray();

        public void SetType(string Value) => this.Type = Value;
        public void AddMember(Variable Member) => this.Members.Add(Member);
    }
}
