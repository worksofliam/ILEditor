using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class MemberCache
    {
        private static Dictionary<string, string> MemberList = new Dictionary<string, string>();

        public static void AddMember(string MemberString, string Type)
        {
            if (!MemberList.ContainsKey(MemberString))
                MemberList.Add(MemberString, Type);
        }

        public static void Export()
        {
            string ExportLoc = Program.SOURCEDIR + @"\" + IBMi.CurrentSystem.GetValue("system") + @"\membercache";

            List<string> Output = new List<string>();

            foreach (var Member in MemberList)
            {
                Output.Add(Member.Key + "," + Member.Value);
            }

            File.WriteAllLines(ExportLoc, Output);
        }

        public static void Import()
        {
            string ImportLoc = Program.SOURCEDIR + @"\" + IBMi.CurrentSystem.GetValue("system") + @"\membercache";

            if (File.Exists(ImportLoc))
            {
                string[] data;
                foreach (string Line in File.ReadAllLines(ImportLoc))
                {
                    if (Line == "") continue;
                    data = Line.Split(',');
                    MemberList.Add(data[0], data[1]);
                }
            }
        }

        public static string[] Find(string Value)
        {
            List<string> Output = new List<string>();

            foreach(string Member in MemberList.Keys)
            {
                if (Member.Contains(Value))
                    Output.Add(Member);
            }

            return Output.ToArray();
        }

        public static string GetType(string Member)
        {
            return MemberList[Member];
        }
    }
}
