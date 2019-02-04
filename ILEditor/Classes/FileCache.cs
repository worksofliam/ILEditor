using System.Collections.Generic;
using System.IO;

namespace ILEditor.Classes
{
	internal class FileCache
	{
		private static readonly string ExportDir  = Program.SOURCEDIR + @"\" + IBMi.CurrentSystem.GetValue("system");
		private static readonly string ExportLoc  = ExportDir + @"\membercache";
		private static readonly string OfflineLoc = ExportDir + @"\offlinecache";

		private static readonly List<string>               OfflineEdits = new List<string>();
		private static readonly Dictionary<string, string> SourceList   = new Dictionary<string, string>();

		public static void AddMemberCache(string MemberString, string Type)
		{
			if (!SourceList.ContainsKey(MemberString))
				SourceList.Add(MemberString, Type);
		}

		public static void AddStreamFile(string Path)
		{
			if (!SourceList.ContainsKey(Path))
				SourceList.Add(Path, "");
		}

		public static void EditsClear()
		{
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
			var Output = new List<string>();

			Directory.CreateDirectory(ExportDir);

			foreach (var Member in SourceList)
				Output.Add(Member.Key + "," + Member.Value);

			File.WriteAllLines(ExportLoc, Output);

			Output.Clear();

			foreach (var Member in OfflineEdits)
				Output.Add(Member);

			File.WriteAllLines(OfflineLoc, Output);
		}

		public static void Import()
		{
			if (File.Exists(ExportLoc))
			{
				string[] data;
				foreach (var Line in File.ReadAllLines(ExportLoc))
				{
					if (Line == "")
						continue;

					data = Line.Split(',');
					SourceList.Add(data[0], data[1]);
				}
			}

			if (File.Exists(OfflineLoc))
				foreach (var Line in File.ReadAllLines(OfflineLoc))
					OfflineEdits.Add(Line);
		}

		public static string[] Find(string Value)
		{
			var Output = new List<string>();

			foreach (var Member in SourceList.Keys)
				if (Member.ToLower().Contains(Value.ToLower()))
					Output.Add(Member);

			return Output.ToArray();
		}

		public static string GetType(string Member)
		{
			if (SourceList.ContainsKey(Member))
				return SourceList[Member];

			return "";
		}
	}
}