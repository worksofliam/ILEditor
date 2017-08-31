using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.LanguageTools
{
    class CLFile
    {
        public static IList<string> CorrectLines(string[] Lines, int RecordLength)
        {
            List<string> outputFile = new List<string>();
            foreach (string Line in Lines)
            {
                if (Line.Length <= RecordLength)
                {
                    outputFile.Add(Line);
                }
                else
                {
                    foreach (string newLine in SplitUpLine(Line, FindStartSpace(Line), RecordLength))
                    {
                        outputFile.Add(newLine);
                    }
                }
            }

            return outputFile.ToArray();
        }

        public static int FindStartSpace(string Line)
        {
            int output = 0;

            foreach (char c in Line.ToCharArray())
            {
                if (c == ' ')
                {
                    output++;
                }
                else
                {
                    break;
                }
            }

            return output;
        }

        public static string[] SplitUpLine(string Line, int StartSpace, int RecordLength)
        {
            Line = Line.Trim();
            List<string> lines = new List<string>();
            List<string> pieces = new List<string>();
            string current = "";

            foreach (char c in Line.ToCharArray())
            {
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
            }
            if (current != "") pieces.Add(current);

            int parmStart = StartSpace + pieces[0].Length + 1;

            current = "".PadLeft(StartSpace);
            foreach (string piece in pieces)
            {
                if ((current.Length + piece.Length + 2) < RecordLength)
                {
                    current += piece + ' ';
                }
                else
                {
                    lines.Add(current.TrimEnd() + " +");
                    current = "".PadLeft(parmStart) + piece + ' ';
                }
            }

            if (current.Trim() != "") lines.Add(current);

            return lines.ToArray();
        }
    }
}
