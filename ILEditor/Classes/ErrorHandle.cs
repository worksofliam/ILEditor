using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class ErrorHandle
    {
        private static string _name = "";
        private static string[] _Lines;

        private static int _FileID;
        private static Dictionary<int, string> _FileIDs;
        private static Dictionary<int, List<LineError>> _Errors;
        private static Dictionary<int, List<expRange>> _Expansions;
        private static Dictionary<int, bool> _TrackCopies; //Used for embedded sql with copies/includes using *lvl2
        private static Boolean _Success = false;

        public static void getErrors(string lib, string obj)
        {
            lib = lib.Trim().ToUpper();
            obj = obj.Trim().ToUpper();

            _Success = false;
            string filetemp = IBMiUtils.DownloadMember(lib, "EVFEVENT", obj);

            if (filetemp != "")
            {
                ErrorHandle.doName(lib.ToUpper() + '/' + obj.ToUpper());
                ErrorHandle.setLines(File.ReadAllLines(filetemp, Program.Encoding));
                _Success = true;
            }
        }

        public static string doName(string newName = "")
        {
            if (newName != "") _name = newName;

            return _name;
        }

        public static Boolean WasSuccessful()
        {
            return _Success;
        }
        public static void setLines(string[] data)
        {
            _Lines = data;
            wrkErrors();
        }

        public static void wrkErrors()
        {
            _FileIDs = new Dictionary<int, string>();
            _Errors = new Dictionary<int, List<LineError>>();
            _Expansions = new Dictionary<int, List<expRange>>();
            _TrackCopies = new Dictionary<int, bool>();
            expRange copyRange = null;

            string err;
            int sev;
            int linenum, column, sqldiff;

            string[] pieces;
            string curtype;

            foreach (string line in _Lines)
            {
                if (line == null) continue;
                err = line.PadRight(150);
                pieces = err.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                curtype = err.Substring(0, 10).TrimEnd();
                _FileID = int.Parse(line.Substring(13, 3));
                switch (curtype)
                {
                    case "FILEID":
                        if (_FileIDs.ContainsKey(_FileID))
                        {
                            //_FileIDs[_FileID] = pieces[5];
                        }
                        else
                        {
                            _FileIDs.Add(_FileID, pieces[5]);
                            _Errors.Add(_FileID, new List<LineError>());
                            _Expansions.Add(_FileID, new List<expRange>());

                            //000000 check means that the current FILEID is not an include
                            _TrackCopies.Add(_FileID, line.Substring(17, 6) != "000000");
                            copyRange = new expRange(1, 0);
                        }
                        break;

                    case "FILEEND":
                        if (_TrackCopies[_FileID])
                        {
                            copyRange._high = int.Parse(pieces[3]);
                            if (_Expansions.ContainsKey(999)) //Indicates SQL precompiler temp file
                                _Expansions[1].Add(copyRange);
                        }
                        break;

                    case "EXPANSION":
                        _Expansions[_FileID].Add(new expRange(int.Parse(pieces[6]), int.Parse(pieces[7])));
                        break;

                    case "ERROR":
                        sev = int.Parse(err.Substring(58, 2));
                        linenum = int.Parse(err.Substring(37, 6));
                        column = int.Parse(err.Substring(33, 3));
                        sqldiff = 0;

                        if (sev >= 20)
                        {
                            foreach (expRange range in _Expansions[_FileID])
                            {
                                if (range.inRange(linenum))
                                {
                                    sqldiff += range.getVal();
                                }
                            }
                        }

                        if (sqldiff > 0)
                        {
                            linenum -= sqldiff;
                        }

                        _Errors[_FileID].Add(new LineError(sev, linenum, column, err.Substring(65), err.Substring(48, 7)));
                        break;
                }
            }

            if (IBMi.CurrentSystem.GetValue("fetchJobLog") == "true")
            {
                string JobLog = IBMiUtils.GetLocalFile("QTEMP", "JOBLOG", "JOBLOG");
                _FileID = -1;
                _FileIDs.Add(_FileID, "Job Log");
                _Errors.Add(_FileID, new List<LineError>());
                foreach (string Line in File.ReadAllLines(JobLog))
                {
                    _Errors[_FileID].Add(new LineError(50, 0, 0, Line, ""));
                }
            }
        }

        public static int[] getFileIDs()
        {
            return _FileIDs.Keys.ToArray();
        }

        public static string getFilePath(int fileid)
        {
            return _FileIDs[fileid];
        }

        public static LineError[] getErrors(int fileid)
        {
            return _Errors[fileid].ToArray();
        }
    }

    class expRange
    {
        public int _low;
        public int _high;

        public expRange(int low, int high)
        {
            _low = low;
            _high = high;
        }

        public bool inRange(int num)
        {
            return (num >= _high);
        }

        public int getVal()
        {
            return (_high - _low) + 1;
        }
    }

    class LineError
    {
        private int _sev;
        private int _line;
        private int _col;
        private string _data = "";
        private string _errcode;

        public LineError(int sev, int line, int col, string data, string errcode)
        {
            _sev = sev;
            _line = line;
            _col = col;
            _data = data;
            _errcode = errcode;
        }

        public int getSev()
        {
            return _sev;
        }

        public int getLine()
        {
            return _line;
        }

        public int getColumn()
        {
            return _col;
        }

        public string getData()
        {
            return _data;
        }

        public string getCode()
        {
            return _errcode;
        }
    }
}
