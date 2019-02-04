using System.Linq;

namespace ILEditor.Classes
{
	public enum FileSystem
	{
		QSYS,
		IFS
	}

	public class RemoteSource
	{
		private readonly string _Ext;

		private readonly string _IFSPath;
		private          bool   _isEditable;

		private bool _isLocked;

		private readonly string _Lib;
		public           string _Local;
		private readonly string _Mbr;
		private readonly string _Obj;
		private readonly int    _RecordLength;

		public           string     _Text;
		private readonly FileSystem RemoteFS;

		public RemoteSource(string Local,
		                    string Lib,
		                    string Obj,
		                    string Mbr,
		                    string Ext,
		                    bool   isEditable   = true,
		                    int    RecordLength = 0)
		{
			RemoteFS = FileSystem.QSYS;

			_Local        = Local;
			_Lib          = Lib;
			_Obj          = Obj;
			_Mbr          = Mbr;
			_Ext          = Ext;
			_RecordLength = RecordLength;
			_isEditable   = isEditable;

			_isLocked = false;
			_IFSPath  = "";
		}

		public RemoteSource(string Local, string Remote, bool isEditable = true)
		{
			RemoteFS = FileSystem.IFS;

			_Local      = Local;
			_IFSPath    = Remote;
			_isEditable = isEditable;

			_Mbr = Remote.Split('/').Last();
			if (_Mbr.Contains("."))
			{
				var data = _Mbr.Split('.');
				_Mbr = data.First();
				_Ext = data.Last();
			}
			else
			{
				_Ext = "";
			}

			_RecordLength = 0;
			_isLocked     = false;

			_Lib = "";
			_Obj = "";
		}

		public FileSystem GetFS()
		{
			return RemoteFS;
		}

		public int GetRecordLength()
		{
			return _RecordLength;
		}

		public string GetText()
		{
			return _Text;
		}

		public string GetLocalFile()
		{
			return _Local;
		}

		public string GetRemoteFile()
		{
			return _IFSPath;
		}

		public string GetLibrary()
		{
			return _Lib;
		}

		public string GetObject()
		{
			return _Obj;
		}

		public string GetName()
		{
			return _Mbr;
		}

		public string GetExtension()
		{
			return _Ext;
		}

		public bool IsEditable()
		{
			return _isEditable;
		}

		public void Lock()
		{
			if (!IBMi.IsConnected())
				return;

			if (!_isEditable)
				return;

			switch (RemoteFS)
			{
				case FileSystem.QSYS:
					var result = IBMi.RemoteCommand(
						"ALCOBJ OBJ((" + _Lib + "/" + _Obj + " *FILE *EXCLRD " + _Mbr + ")) WAIT(1)",
						false);

					_isLocked = result;
					if (result == false)
					{
						Editor.TheEditor.SetStatus("Failed to allocate a lock for " +
						                           _Mbr +
						                           "! Member has been placed in read-only mode.");

						_isEditable = false;
					}

					break;
			}
		}

		public void Unlock()
		{
			if (!IBMi.IsConnected())
				return;

			if (!_isLocked)
				return;

			switch (RemoteFS)
			{
				case FileSystem.QSYS:
					IBMi.RemoteCommand("DLCOBJ OBJ((" + _Lib + "/" + _Obj + " *FILE *EXCLRD " + _Mbr + "))", false);

					break;
			}
		}
	}
}