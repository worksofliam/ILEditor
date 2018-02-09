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
        private static string ExportDir = Program.SOURCEDIR + @"\" + IBMi.CurrentSystem.GetValue("system");
        private static string ExportLoc = ExportDir + @"\membercache";
        private static string OfflineLoc = ExportDir + @"\offlinecache";

        private static List<string> OfflineEdits = new List<string>();
        private static Dictionary<string, string> MemberList = new Dictionary<string, string>();

        public static void AddMemberCache(string MemberString, string Type)
        {
            if (!MemberList.ContainsKey(MemberString))
                MemberList.Add(MemberString, Type);
        }

        public static void EditsClear() {
            OfflineEdits.Clear();
        }

        public static void EditsAdd(string Lib, string Obj, string Mbr)
        {
            if (!EditsContains(Lib, Obj, Mbr))
                OfflineEdits.Add(Lib.ToUpper() + "/" + Obj.ToUpper() + "." + Mbr.ToUpper());
        }

        public static bool EditsContains(string Lib, string Obj, string Mbr)
        {
            return OfflineEdits.Contains(Lib.ToUpper() + "/" + Obj.ToUpper() + "." + Mbr.ToUpper());
        }

        public static void Export()
        {
            List<string> Output = new List<string>();

            Directory.CreateDirectory(ExportDir);

            foreach (var Member in MemberList)
                Output.Add(Member.Key + "," + Member.Value);
            File.WriteAllLines(ExportLoc, Output);

            Output.Clear();

            foreach (string Member in OfflineEdits)
                Output.Add(Member);
            File.WriteAllLines(OfflineLoc, Output);
        }

        public static void Import()
        {
            if (File.Exists(ExportLoc))
            {
                string[] data;
                foreach (string Line in File.ReadAllLines(ExportLoc))
                {
                    if (Line == "") continue;
                    data = Line.Split(',');
                    MemberList.Add(data[0], data[1]);
                }
            }

            if (File.Exists(OfflineLoc))
            {
                foreach (string Line in File.ReadAllLines(OfflineLoc))
                {
                    OfflineEdits.Add(Line);
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
            if (MemberList.ContainsKey(Member))
                return MemberList[Member];
            else
                return "";
        }
    }
}
