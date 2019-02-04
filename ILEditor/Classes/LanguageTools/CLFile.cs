using System.Collections.Generic;

namespace ILEditor.Classes.LanguageTools
{
	internal class CLFile
	{
		public static IList<string> CorrectLines(string[] Lines, int RecordLength)
		{
			var outputFile = new List<string>();
			foreach (var Line in Lines)
				if (Line.Length <= RecordLength)
					outputFile.Add(Line);
				else
					foreach (var newLine in SplitUpLine(Line, FindStartSpace(Line), RecordLength))
						outputFile.Add(newLine);

			return outputFile.ToArray();
		}

		public static int FindStartSpace(string Line)
		{
			var output = 0;

			foreach (var c in Line)
				if (c == ' ')
					output++;
				else
					break;

			return output;
		}

		public static string[] SplitUpLine(string Line, int StartSpace, int RecordLength)
		{
			Line = Line.Trim();
			var lines   = new List<string>();
			var pieces  = new List<string>();
			var current = "";

			foreach (var c in Line)
				switch (c)
				{
					case ' ':
						if (current != "")
						{
							pieces.Add(current);
							current = "";
						}
						else
						{
							current += c;
						}

						break;
					default:
						current += c;

						break;
				}

			if (current != "")
				pieces.Add(current);

			var parmStart = StartSpace + pieces[0].Length + 1;

			current = "".PadLeft(StartSpace);
			foreach (var piece in pieces)
				if (current.Length + piece.Length + 2 < RecordLength)
				{
					current += piece + ' ';
				}
				else
				{
					lines.Add(current.TrimEnd() + " +");
					current = "".PadLeft(parmStart) + piece + ' ';
				}

			if (current.Trim() != "")
				lines.Add(current);

			return lines.ToArray();
		}
	}
}