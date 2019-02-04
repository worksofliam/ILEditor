using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ILEditor.UserTools;
using WeifenLuo.WinFormsUI.Docking;

namespace ILEditor.Classes
{
	internal class IBMiUtils
	{
		private static readonly List<string> QTEMPListing = new List<string>();

		private static readonly string[] IgnorePFs = {"EVFTEMP", "QSQLTEMP"};

		//This method is used to determine whether a file in QTEMP needs to be deleted
		//If it's already exists in QTEMP, we delete it - otherwise we delete it next time
		public static void UsingQTEMPFiles(string[] Objects)
		{
			foreach (var Object in Objects)
				if (QTEMPListing.Contains(Object))
					IBMi.RemoteCommand("DLTOBJ OBJ(QTEMP/" + Object + ") OBJTYPE(*FILE)", false);
				else
					QTEMPListing.Add(Object);
		}

		public static bool IsValueObjectName(string Name)
		{
			if (Name.Trim() == "")
				return false;

			if (Name.Length > 10)
				return false;

			return true;
		}

		private static string GetCurrentSystem()
		{
			return IBMi.CurrentSystem.GetValue("system").Split(':')[0];
		}

		public static BindingEntry[] GetBindingDirectory(string Lib, string Obj)
		{
			var line = "";
			if (IBMi.IsConnected())
			{
				var entries = new List<BindingEntry>();
				if (Lib == "*CURLIB")
					Lib = IBMi.CurrentSystem.GetValue("curlib");

				UsingQTEMPFiles(new[] {"BNDDIR", "BNDDATA"});

				IBMi.RemoteCommand("DSPBNDDIR BNDDIR(" + Lib + "/" + Obj + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/BNDDIR)");
				IBMi.RemoteCommand(
					"RUNSQL SQL('CREATE TABLE QTEMP/BNDDATA AS (SELECT BNOBNM, BNOBTP, BNOLNM, BNOACT, BNODAT, BNOTIM FROM qtemp/bnddir) WITH DATA ') COMMIT(*NONE)");

				var file = DownloadMember("QTEMP", "BNDDATA", "BNDDATA");

				if (file != "")
					foreach (var realLine in File.ReadAllLines(file, Program.Encoding))
						if (realLine.Trim() != "")
						{
							var entry = new BindingEntry();
							line               = realLine.PadRight(50);
							entry.BindingLib   = Lib;
							entry.BindingObj   = Obj;
							entry.Name         = line.Substring(0, 10).Trim();
							entry.Type         = line.Substring(10, 7).Trim();
							entry.Library      = line.Substring(17, 10).Trim();
							entry.Activation   = line.Substring(27, 10).Trim();
							entry.CreationDate = line.Substring(37, 6).Trim();
							entry.CreationTime = line.Substring(43, 6).Trim();
							entries.Add(entry);
						}
						else
						{
							return null;
						}

				return entries.ToArray();
			}

			return null;
		}

		public static ILEObject[] GetObjectList(string Lib, string Types = "*PGM *SRVPGM *MODULE")
		{
			if (!IBMi.IsConnected())
				return null;

			var objects = new List<ILEObject>();
			if (Lib == "*CURLIB")
				Lib = IBMi.CurrentSystem.GetValue("curlib");

			string fileA = 'O' + Lib, fileB = "T" + Lib;

			if (fileA.Length > 10)
				fileA = fileA.Substring(0, 10);

			if (fileB.Length > 10)
				fileB = fileB.Substring(0, 10);

			UsingQTEMPFiles(new[] {fileA, fileB});

			IBMi.RemoteCommand("DSPOBJD OBJ(" +
			                   Lib +
			                   "/*ALL) OBJTYPE(" +
			                   Types +
			                   ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/" +
			                   fileA +
			                   ")");

			IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/" +
			                   fileB +
			                   " AS (SELECT ODOBNM, ODOBTP, ODOBAT, char(ODOBSZ) as ODOBSZ, ODOBTX, ODOBOW, ODSRCF, ODSRCL, ODSRCM FROM qtemp/" +
			                   fileA +
			                   " order by ODOBNM) WITH DATA') COMMIT(*NONE)");

			var file = DownloadMember("QTEMP", fileB, fileB);

			if (file == "")
				return objects.ToArray();

			foreach (var realLine in File.ReadAllLines(file, Program.Encoding))
				if (realLine.Trim() != "")
				{
					var Object = new ILEObject();
					var line   = realLine.PadRight(135);
					Object.Library   = Lib;
					Object.Name      = line.Substring(0, 10).Trim();
					Object.Type      = line.Substring(10, 8).Trim();
					Object.Extension = line.Substring(18, 10).Trim();
					uint.TryParse(line.Substring(28, 12).Trim(), out Object.SizeKB);
					Object.Text   = line.Substring(40, 50).Trim();
					Object.Owner  = line.Substring(90, 10).Trim();
					Object.SrcSpf = line.Substring(100, 10).Trim();
					Object.SrcLib = line.Substring(110, 10).Trim();
					Object.SrcMbr = line.Substring(120, 10).Trim();

					objects.Add(Object);
				}
				else
				{
					return null;
				}

			return objects.ToArray();
		}

		public static ILEObject[] GetSpfList(string Lib)
		{
			var spfList = new List<ILEObject>();
			Lib = Lib.ToUpper();
			if (Lib == "*CURLIB")
				Lib = IBMi.CurrentSystem.GetValue("curlib");

			if (IBMi.IsConnected())
			{
				string fileA = 'S' + Lib, fileB = "D" + Lib;

				if (fileA.Length > 10)
					fileA = fileA.Substring(0, 10);

				if (fileB.Length > 10)
					fileB = fileB.Substring(0, 10);

				UsingQTEMPFiles(new[] {fileA, fileB});

				Editor.TheEditor.SetStatus("Fetching source-physical files for " + Lib + "...");

				IBMi.RemoteCommand("DSPFD FILE(" +
				                   Lib +
				                   "/*ALL) TYPE(*ATR) OUTPUT(*OUTFILE) FILEATR(*PF) OUTFILE(QTEMP/" +
				                   fileA +
				                   ")");

				IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/" +
				                   fileB +
				                   " AS (SELECT PHFILE, PHLIB FROM QTEMP/" +
				                   fileA +
				                   " WHERE PHDTAT = ''S'' order by PHFILE) WITH DATA') COMMIT(*NONE)");

				var file = DownloadMember("QTEMP", fileB, fileB);

				if (file != "")
				{
					foreach (var realLine in File.ReadAllLines(file, Program.Encoding))
						if (realLine.Trim() != "")
						{
							var validName = true;
							var line      = realLine.PadRight(31);
							var Object    = line.Substring(0, 10).Trim();
							var library   = line.Substring(10, 10).Trim();

							var obj = new ILEObject();
							obj.Library = library;
							obj.Name    = Object;

							foreach (var name in IgnorePFs)
								if (obj.Name.StartsWith(name))
									validName = false;

							if (validName)
								spfList.Add(obj);
						}
				}
				else
				{
					return null;
				}

				Editor.TheEditor.SetStatus("Fetched source-physical files for " + Lib + ".");
			}
			else
			{
				var dirPath = GetLocalDir(Lib);

				if (Directory.Exists(dirPath))
					foreach (var dir in Directory.GetDirectories(dirPath))
						spfList.Add(new ILEObject {Library = Lib, Name = Path.GetDirectoryName(dir)});
				else
					return null;
			}

			return spfList.ToArray();
		}

		public static RemoteSource[] GetMemberList(string Lib, string Obj)
		{
			string       type;
			var          members = new List<RemoteSource>();
			RemoteSource newMember;

			Lib = Lib.ToUpper();
			Obj = Obj.ToUpper();

			if (IBMi.IsConnected())
			{
				if (Lib == "*CURLIB")
					Lib = IBMi.CurrentSystem.GetValue("curlib");

				Editor.TheEditor.SetStatus("Fetching members for " + Lib + "/" + Obj + "...");

				var tempName = 'M' + Obj;
				if (tempName.Length > 10)
					tempName = tempName.Substring(0, 10);

				UsingQTEMPFiles(new[] {tempName, Obj});

				IBMi.RemoteCommand("DSPFD FILE(" +
				                   Lib +
				                   "/" +
				                   Obj +
				                   ") TYPE(*MBR) OUTPUT(*OUTFILE) OUTFILE(QTEMP/" +
				                   tempName +
				                   ")");

				IBMi.RemoteCommand("RUNSQL SQL('CREATE TABLE QTEMP/" +
				                   Obj +
				                   " AS (SELECT MBFILE, MBNAME, MBMTXT, MBSEU2, char(MBMXRL) as MBMXRL FROM QTEMP/" +
				                   tempName +
				                   " order by MBNAME) WITH DATA') COMMIT(*NONE)");

				var file = DownloadMember("QTEMP", Obj, Obj);

				if (file != "")
					foreach (var realLine in File.ReadAllLines(file, Program.Encoding))
						if (realLine.Trim() != "")
						{
							var line   = realLine.PadRight(90);
							var Object = line.Substring(0, 10).Trim();
							var name   = line.Substring(10, 10).Trim();
							var desc   = line.Substring(20, 50).Trim();
							type = line.Substring(70, 10).Trim();
							var rcdLen = line.Substring(80, 7).Trim();

							if (name != "")
							{
								newMember = new RemoteSource("", Lib, Object, name, type, true, int.Parse(rcdLen) - 12);

								newMember._Text = desc;

								members.Add(newMember);
								FileCache.AddMemberCache(Lib + "/" + Object + "." + name, type);
							}
						}
						else
						{
							return null;
						}

				Editor.TheEditor.SetStatus("Fetched members for " + Lib + " / " + Obj + ".");
			}
			else
			{
				var dirPath = GetLocalDir(Lib, Obj);

				if (Directory.Exists(dirPath))
					foreach (var file in Directory.GetFiles(dirPath))
					{
						type = Path.GetExtension(file).ToUpper();
						if (type.StartsWith("."))
							type = type.Substring(1);

						newMember = new RemoteSource(file, Lib, Obj, Path.GetFileNameWithoutExtension(file), type);

						newMember._Text = "";
						members.Add(newMember);
					}
				else
					return null;
			}

			return members.ToArray();
		}

		public static List<ILEObject[]> GetProgramReferences(string Lib, string Obj = "*ALL")
		{
			var items = new List<ILEObject[]>();

			Lib = Lib.ToUpper();
			Obj = Obj.ToUpper();

			if (IBMi.IsConnected())
			{
				if (Lib == "*CURLIB")
					Lib = IBMi.CurrentSystem.GetValue("curlib");

				Editor.TheEditor.SetStatus("Fetching references for " + Lib + "/" + Obj + "...");

				UsingQTEMPFiles(new[] {"REFS", "REFSB"});

				IBMi.RemoteCommand("DSPPGMREF PGM(" + Lib + "/" + Obj + ") OUTPUT(*OUTFILE) OUTFILE(QTEMP/REFS)");
				IBMi.RemoteCommand(
					"RUNSQL SQL('CREATE TABLE QTEMP/REFSB AS (SELECT WHLIB, WHPNAM, WHFNAM, WHLNAM, WHOTYP FROM qtemp/refs) WITH DATA') COMMIT(*NONE)");

				var file = DownloadMember("QTEMP", "REFSB", "REFSB");

				if (file != "")
					foreach (var realLine in File.ReadAllLines(file, Program.Encoding))
						if (realLine.Trim() != "")
						{
							var line    = realLine.PadRight(52);
							var library = line.Substring(0, 10).Trim();
							var Object  = line.Substring(10, 10).Trim();
							var refObj  = line.Substring(20, 11).Trim();
							var refLib  = line.Substring(31, 11).Trim();
							var type    = line.Substring(42, 10).Trim();

							if (library != "")
								items.Add(new[] {new ILEObject(library, Object), new ILEObject(refLib, refObj, type)});
						}
						else
						{
							return null;
						}

				Editor.TheEditor.SetStatus("Fetched references for " + Lib + " / " + Obj + ".");
			}
			else
			{
				Editor.TheEditor.SetStatus("Cannot fetch references when offline.");

				return null;
			}

			return items;
		}

		public static SpoolFile[] GetSpoolListing(string Lib, string Obj)
		{
			if (!IBMi.IsConnected())
				return null;

			var listing  = new List<SpoolFile>();
			var commands = new List<string>(); // todo: not used

			var file = "";

			if (Lib != "" && Obj != "")
			{
				Editor.TheEditor.SetStatus("Fetching spool file listing.. (can take a moment)");

				UsingQTEMPFiles(new[] {"SPOOL"});
				IBMi.RemoteCommand(
					"RUNSQL SQL('CREATE TABLE QTEMP/SPOOL AS (SELECT Char(SPOOLED_FILE_NAME) as a, Char(COALESCE(USER_DATA, '''')) as b, Char(JOB_NAME) as c, Char(STATUS) as d, Char(FILE_NUMBER) as e FROM TABLE(QSYS2.OUTPUT_QUEUE_ENTRIES(''" +
					Lib +
					"'', ''" +
					Obj +
					"'', ''*NO'')) A WHERE USER_NAME = ''" +
					IBMi.CurrentSystem.GetValue("username").ToUpper() +
					"'' ORDER BY CREATE_TIMESTAMP DESC FETCH FIRST 25 ROWS ONLY) WITH DATA') COMMIT(*NONE)");

				file = DownloadMember("QTEMP", "SPOOL", "SPOOL");
				Editor.TheEditor.SetStatus("Finished fetching spool file listing.");
			}

			if (file != "")
			{
				foreach (var realLine in File.ReadAllLines(file, Program.Encoding))
					if (realLine.Trim() != "")
					{
						var line      = realLine.PadRight(75);
						var spoolName = line.Substring(0, 10).Trim();
						var userData  = line.Substring(10, 10).Trim();
						var job       = line.Substring(20, 28).Trim();
						var status    = line.Substring(48, 15).Trim();
						var number    = line.Substring(63, 11);

						if (spoolName != "")
							listing.Add(new SpoolFile(spoolName, userData, job, status, int.Parse(number)));
					}

				return listing.Count > 0 ? listing.ToArray() : null;
			}

			return null;
		}

		public static bool CompileSource(RemoteSource SourceInfo, string TrueCmd = "")
		{
			if (IBMi.IsConnected())
			{
				var    commands = new List<string>(); // todo: not used
				string filetemp = GetLocalFile("QTEMP", "JOBLOG", "JOBLOG"), name, library = "";

				if (SourceInfo != null)
				{
					var type    = SourceInfo.GetExtension().ToUpper();
					var command = IBMi.CurrentSystem.GetValue("DFT_" + type);

					if (command.Trim() == "")
						return true;

					if (TrueCmd != "")
						command = TrueCmd;

					Editor.TheEditor.SetStatus("Compiling " + SourceInfo.GetName() + " with " + command + "...");

					if (SourceInfo.GetFS() == FileSystem.IFS)
						command += "_IFS";

					command = IBMi.CurrentSystem.GetValue(command);

					if (command.Trim() == "")
						return true;

					name = SourceInfo.GetName();
					if (name.Length > 10)
						name.Substring(0, 10);

					switch (SourceInfo.GetFS())
					{
						case FileSystem.QSYS:
							command = command.Replace("&OPENLIB", SourceInfo.GetLibrary());
							command = command.Replace("&OPENSPF", SourceInfo.GetObject());
							command = command.Replace("&OPENMBR", SourceInfo.GetName());
							library = SourceInfo.GetLibrary();

							break;
						case FileSystem.IFS:
							command = command.Replace("&FILENAME", name);
							command = command.Replace("&FILEPATH", SourceInfo.GetRemoteFile());
							command = command.Replace("&BUILDLIB", IBMi.CurrentSystem.GetValue("buildLib"));

							library = IBMi.CurrentSystem.GetValue("buildLib");
							IBMi.SetWorkingDir(IBMi.CurrentSystem.GetValue("homeDir"));

							break;
					}

					if (library != "")
					{
						command = command.Replace("&CURLIB", IBMi.CurrentSystem.GetValue("curlib"));
						IBMi.RemoteCommand(
							$"CHGLIBL LIBL({IBMi.CurrentSystem.GetValue("datalibl").Replace(',', ' ')}) CURLIB({IBMi.CurrentSystem.GetValue("curlib")})");

						if (!IBMi.RemoteCommand(command))
						{
							Editor.TheEditor.SetStatus("Compile finished unsuccessfully.");
							if (command.ToUpper().Contains("*EVENTF"))
							{
								Editor.TheEditor.SetStatus("Fetching errors..");
								Editor.TheEditor.Invoke((MethodInvoker) delegate
								{
									Editor.TheEditor.AddTool(new ErrorListing(library, name),
										DockState.DockLeft,
										true);
								});

								Editor.TheEditor.SetStatus("Fetched errors.");
							}

							if (IBMi.CurrentSystem.GetValue("fetchJobLog") == "true")
							{
								UsingQTEMPFiles(new[] {"JOBLOG"});
								IBMi.RemoteCommand(
									"RUNSQL SQL('CREATE TABLE QTEMP/JOBLOG AS (SELECT char(MESSAGE_TEXT) as a FROM TABLE(QSYS2.JOBLOG_INFO(''*'')) A WHERE MESSAGE_TYPE = ''DIAGNOSTIC'') WITH DATA') COMMIT(*NONE)");

								IBMi.DownloadFile(filetemp, "/QSYS.lib/QTEMP.lib/JOBLOG.file/JOBLOG.mbr");
							}
						}
						else
						{
							Editor.TheEditor.SetStatus("Compile finished successfully.");
						}
					}
					else
					{
						Editor.TheEditor.SetStatus("Build library not set. See Connection Settings.");
					}
				}
				else
				{
					Editor.TheEditor.SetStatus("Only remote members can be compiled.");
				}

				return true;
			}

			return false;
		}

		public static string GetLocalDir(string Lib)
		{
			var libDir = Program.SOURCEDIR + "\\" + GetCurrentSystem() + "\\" + Lib;

			if (!Directory.Exists(libDir))
				Directory.CreateDirectory(libDir);

			return libDir;
		}

		public static string GetLocalDir(string Lib, string Obj)
		{
			var spfDir = Program.SOURCEDIR + "\\" + GetCurrentSystem() + "\\" + Lib + "\\" + Obj;

			if (!Directory.Exists(spfDir))
				Directory.CreateDirectory(spfDir);

			return spfDir;
		}

		public static string GetLocalSource(string Lib, string Spf, string Mbr) //todo: not used
		{
			var      result = "";
			string[] libl;

			if (Lib == "*LIBL")
				libl = IBMi.CurrentSystem.GetValue("datalibl").Split(',');
			else
				libl = new[] {Lib};

			foreach (var lib in libl)
			{
				var dir = Path.Combine(Program.SOURCEDIR, GetCurrentSystem(), lib);

				if (!Directory.Exists(dir))
					continue;

				dir = Path.Combine(dir, Spf);

				if (!Directory.Exists(dir))
					continue;

				foreach (var filePath in Directory.GetFiles(dir))
					if (Path.GetFileNameWithoutExtension(filePath) == Mbr)
					{
						result = File.ReadAllText(filePath).ToUpper();

						return result;
					}
			}

			return result;
		}

		public static string GetLocalFile(string Lib, string Obj, string Mbr, string Ext = "")
		{
			Lib = Lib.ToUpper();
			Obj = Obj.ToUpper();
			Mbr = Mbr.ToUpper();
			if (Ext == "")
				Ext = "mbr";

			if (Lib == "*CURLIB")
				Lib = IBMi.CurrentSystem.GetValue("curlib");

			var spfDir = Program.SOURCEDIR + "\\" + GetCurrentSystem() + "\\" + Lib + "\\" + Obj;

			if (!Directory.Exists(spfDir))
				Directory.CreateDirectory(spfDir);

			return spfDir + "\\" + Mbr.ToUpper() + "." + Ext.ToLower();
		}

		public static string DownloadSpoolFile(string Name, int Number, string Job)
		{
			//CPYSPLF FILE(NAME) JOB(B/A/JOB) TOSTMF('STMF')

			if (!IBMi.IsConnected())
				return "";

			var filetemp   = GetLocalFile("SPOOLS", Job.Replace('/', '.'), Name + '-' + Number, "SPOOL");
			var remoteTemp = "/tmp/" + Name + ".spool";

			Editor.TheEditor.SetStatus("Downloading spool file " + Name + "..");
			IBMi.RemoteCommand("CPYSPLF FILE(" +
			                   Name +
			                   ") JOB(" +
			                   Job +
			                   ") SPLNBR(" +
			                   Number +
			                   ") TOFILE(*TOSTMF) TOSTMF('" +
			                   remoteTemp +
			                   "') STMFOPT(*REPLACE)");

			if (!IBMi.DownloadFile(filetemp, remoteTemp))
			{
				Editor.TheEditor.SetStatus("Downloaded spool file " + Name + ".");

				return filetemp;
			}

			Editor.TheEditor.SetStatus("Failed downloading spool file " + Name + ".");

			return "";
		}

		public static string DownloadMember(string Lib, string Obj, string Mbr, string Ext = "")
		{
			if (Lib == "*CURLIB")
				Lib = IBMi.CurrentSystem.GetValue("curlib");

			var filetemp = GetLocalFile(Lib, Obj, Mbr, Ext);

			if (IBMi.IsConnected())
			{
				if (IBMi.DownloadFile(filetemp, "/QSYS.lib/" + Lib + ".lib/" + Obj + ".file/" + Mbr + ".mbr") == false)
					return filetemp;

				return "";
			}

			Editor.TheEditor.SetStatus("Fetching existing local member.");

			if (File.Exists(filetemp))
				return filetemp;

			return "";
		}

		public static string DownloadFile(string RemoteFile)
		{
			var filetemp = Path.Combine(GetLocalDir("IFS"), Path.GetFileName(RemoteFile));

			if (IBMi.IsConnected())
			{
				if (IBMi.DownloadFile(filetemp, RemoteFile) == false)
					return filetemp;

				return "";
			}

			Editor.TheEditor.SetStatus("Fetching existing local file.");

			if (File.Exists(filetemp))
				return filetemp;

			return "";
		}

		public static bool UploadMember(string Local, string Lib, string Obj, string Mbr)
		{
			Lib = Lib.ToUpper();
			Obj = Obj.ToUpper();
			Mbr = Mbr.ToUpper();

			if (IBMi.IsConnected())
				return IBMi.UploadFile(Local, "/QSYS.lib/" + Lib + ".lib/" + Obj + ".file/" + Mbr + ".mbr");

			return true;
		}

		public static bool UploadFile(string Local, string Remote)
		{
			if (IBMi.IsConnected())
				return IBMi.UploadFile(Local, Remote);

			Editor.TheEditor.SetStatus("Saving locally only.");

			return true;
		}
	}
}