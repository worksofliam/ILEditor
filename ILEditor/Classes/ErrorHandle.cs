using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ILEditor.Classes
{
	internal class ErrorHandle
	{
		private static string   _name = "";
		private static string[] _lines;

		private static int                              _fileId;
		private static Dictionary<int, string>          _fileIDs;
		private static Dictionary<int, List<LineError>> _errors;
		private static Dictionary<int, List<ExpRange>>  _expansions;

		private static Dictionary<int, bool> _trackCopies; //Used for embedded sql with copies/includes using *lvl2

		private static bool _success;

		public static void GetErrors(string lib, string obj)
		{
			lib = lib.Trim().ToUpper();
			obj = obj.Trim().ToUpper();

			_success = false;
			var filetemp = IBMiUtils.DownloadMember(lib, "EVFEVENT", obj);

			if (filetemp != "")
			{
				DoName(lib.ToUpper() + '/' + obj.ToUpper());
				SetLines(File.ReadAllLines(filetemp, Program.Encoding));
				_success = true;
			}
		}

		public static string DoName(string newName = "")
		{
			if (newName != "")
				_name = newName;

			return _name;
		}

		public static bool WasSuccessful()
		{
			return _success;
		}

		public static void SetLines(string[] data)
		{
			_lines = data;
			WrkErrors();
		}

		public static void WrkErrors()
		{
			_fileIDs     = new Dictionary<int, string>();
			_errors      = new Dictionary<int, List<LineError>>();
			_expansions  = new Dictionary<int, List<ExpRange>>();
			_trackCopies = new Dictionary<int, bool>();
			ExpRange copyRange = null;

			foreach (var line in _lines)
			{
				if (line == null)
					continue;

				var err         = line.PadRight(150);
				var pieces      = err.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				var currentType = err.Substring(0, 10).TrimEnd();
				_fileId = int.Parse(line.Substring(13, 3));
				switch (currentType)
				{
					case "FILEID":
						if (_fileIDs.ContainsKey(_fileId))
						{
							//_FileIDs[_FileID] = pieces[5];
						}
						else
						{
							_fileIDs.Add(_fileId, pieces[5]);
							_errors.Add(_fileId, new List<LineError>());
							_expansions.Add(_fileId, new List<ExpRange>());

							//000000 check means that the current FILEID is not an include
							_trackCopies.Add(_fileId, line.Substring(17, 6) != "000000");
							copyRange = new ExpRange(1, 0);
						}

						break;

					case "FILEEND":
						if (_trackCopies[_fileId])
						{
							copyRange.High = int.Parse(pieces[3]);
							if (_expansions.ContainsKey(999)) //Indicates SQL precompiler temp file
								_expansions[1].Add(copyRange);
						}

						break;

					case "EXPANSION":
						_expansions[_fileId].Add(new ExpRange(int.Parse(pieces[6]), int.Parse(pieces[7])));

						break;

					case "ERROR":
						var sev     = int.Parse(err.Substring(58, 2));
						var lineNum = int.Parse(err.Substring(37, 6));
						var column  = int.Parse(err.Substring(33, 3));
						var sqldiff = 0;

						if (sev >= 20)
							foreach (var range in _expansions[_fileId])
								if (range.InRange(lineNum))
									sqldiff += range.GetVal();

						if (sqldiff > 0)
							lineNum -= sqldiff;

						_errors[_fileId]
							.Add(new LineError(sev, lineNum, column, err.Substring(65), err.Substring(48, 7)));

						break;
				}
			}

			if (IBMi.CurrentSystem.GetValue("fetchJobLog") == "true")
			{
				var jobLog = IBMiUtils.GetLocalFile("QTEMP", "JOBLOG", "JOBLOG");
				_fileId = -1;
				_fileIDs.Add(_fileId, "Job Log");
				_errors.Add(_fileId, new List<LineError>());
				foreach (var line in File.ReadAllLines(jobLog))
					_errors[_fileId].Add(new LineError(50, 0, 0, line, ""));
			}
		}

		public static int[] GetFileIDs()
		{
			return _fileIDs.Keys.ToArray();
		}

		public static string GetFilePath(int fileId)
		{
			return _fileIDs[fileId];
		}

		public static LineError[] GetErrors(int fileId)
		{
			return _errors[fileId].ToArray();
		}
	}

	internal class ExpRange
	{
		public readonly int Low;
		public          int High;

		public ExpRange(int low, int high)
		{
			Low  = low;
			High = high;
		}

		public bool InRange(int num)
		{
			return num >= High;
		}

		public int GetVal()
		{
			return High - Low + 1;
		}
	}

	internal class LineError
	{
		public LineError(int sev, int line, int col, string data, string code)
		{
			Sev    = sev;
			Line   = line;
			Column = col;
			Data   = data;
			Code   = code;
		}

		public int Sev { get; }

		public int Line { get; }

		public int Column { get; }

		public string Data { get; }

		public string Code { get; }
	}
}