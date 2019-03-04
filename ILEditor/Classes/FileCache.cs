using System.Collections.Generic;
using System.IO;

namespace ILEditor.Classes
{
	internal static class FileCache
	{
		private static readonly string ExportDir  = Program.SOURCEDIR + @"\" + IBMi.CurrentSystem.GetValue("system");
		private static readonly string ExportLoc  = ExportDir + @"\membercache";
		private static readonly string OfflineLoc = ExportDir + @"\offlinecache";

		private static readonly List<string>               OfflineEdits     = new List<string>();
		private static readonly Dictionary<string, string> SourceDictionary = new Dictionary<string, string>();

		public static void AddMemberCache(string MemberString, string Type)
		{
			if (!SourceDictionary.ContainsKey(MemberString))
				SourceDictionary.Add(MemberString, Type);
		}

		public static void AddStreamFile(string Path)
		{
			if (!SourceDictionary.ContainsKey(Path))
				SourceDictionary.Add(Path, "");
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
			var output = new List<string>();

			Directory.CreateDirectory(ExportDir);

			foreach (var member in SourceDictionary)
				output.Add(member.Key + "," + member.Value);

			File.WriteAllLines(ExportLoc, output);

			output.Clear();

			foreach (var member in OfflineEdits)
				output.Add(member);

			File.WriteAllLines(OfflineLoc, output);
		}

		public static void Import()
		{
			if (File.Exists(ExportLoc))
				foreach (var line in File.ReadAllLines(ExportLoc))
				{
					if (string.IsNullOrWhiteSpace(line))
						continue;

					var data = line.Split(',');
					SourceDictionary.Add(data[0], data[1]);
				}

			if (File.Exists(OfflineLoc))
				foreach (var line in File.ReadAllLines(OfflineLoc))
					OfflineEdits.Add(line);
		}

		public static string[] Find(string Value)
		{
			var output = new List<string>();

			foreach (var member in SourceDictionary.Keys)
				if (member.ToLower().Contains(Value.ToLower()))
					output.Add(member);

			return output.ToArray();
		}

		public static string GetType(string Member)
		{
			if (SourceDictionary.ContainsKey(Member))
				return SourceDictionary[Member];

			return string.Empty;
		}
	}
}