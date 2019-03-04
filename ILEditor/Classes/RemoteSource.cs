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
		private bool _isLocked;

		public RemoteSource(string localFile,
		                    string library,
		                    string obj,
		                    string Mbr,
		                    string extension,
		                    bool   isEditable   = true,
		                    int    recordLength = 0)
		{
			FileSystem = FileSystem.QSYS;

			LocalFile    = localFile;
			Library      = library;
			Object       = obj;
			Name         = Mbr;
			Extension    = extension;
			RecordLength = recordLength;
			IsEditable   = isEditable;

			_isLocked  = false;
			RemoteFile = "";
		}

		public RemoteSource(string localFile, string Remote, bool isEditable = true)
		{
			FileSystem = FileSystem.IFS;

			LocalFile  = localFile;
			RemoteFile = Remote;
			IsEditable = isEditable;

			Name = Remote.Split('/').Last();
			if (Name.Contains("."))
			{
				var data = Name.Split('.');
				Name      = data.First();
				Extension = data.Last();
			}
			else
			{
				Extension = string.Empty;
			}

			RecordLength = 0;
			_isLocked    = false;

			Library = string.Empty;
			Object  = string.Empty;
		}

		public FileSystem FileSystem { get; }

		public int RecordLength { get; }

		public string Text { get; set; }

		public string LocalFile { get; set; }

		public string RemoteFile { get; }

		public string Library { get; }

		public string Object { get; }

		public string Name { get; }

		public string Extension { get; }

		public bool IsEditable { get; private set; }

		public void Lock()
		{
			if (!IBMi.IsConnected)
				return;

			if (!IsEditable)
				return;

			switch (FileSystem)
			{
				case FileSystem.QSYS:
					var result = IBMi.RemoteCommand(
						"ALCOBJ OBJ((" + Library + "/" + Object + " *FILE *EXCLRD " + Name + ")) WAIT(1)",
						false);

					_isLocked = result;
					if (result == false)
					{
						Editor.TheEditor.SetStatus("Failed to allocate a lock for " +
						                           Name +
						                           "! Member has been placed in read-only mode.");

						IsEditable = false;
					}

					break;
			}
		}

		public void Unlock()
		{
			if (!IBMi.IsConnected)
				return;

			if (!_isLocked)
				return;

			switch (FileSystem)
			{
				case FileSystem.QSYS:
					IBMi.RemoteCommand("DLCOBJ OBJ((" + Library + "/" + Object + " *FILE *EXCLRD " + Name + "))",
						false);

					break;
			}
		}
	}
}