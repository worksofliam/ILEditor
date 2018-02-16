using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.LanguageTools
{
    public class Function
    {
        private string Name;
        private List<Variable> Variables;

        public Function(string Name)
        {
            this.Name = Name;
            this.Variables = new List<Variable>();
        }

        public void AddVariable(Variable var)
        {
            this.Variables.Add(var);
        }

        public string GetName() => this.Name;
        public Variable[] GetVariables() => Variables.ToArray();
    }

    public class Variable
    {
        private string Name;
        //TODO: change Type to an enum
        private string Type;
        private int Line;
        public Variable(string Name, string Type, int LineNumber)
        {
            this.Name = Name;
            this.Type = Type;
            this.Line = LineNumber;
        }

        public string GetName() => this.Name;
        public new string GetType() => this.Type;
        public int GetLine() => this.Line;
    }
}
