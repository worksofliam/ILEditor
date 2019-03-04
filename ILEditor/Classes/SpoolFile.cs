namespace ILEditor.Classes
{
	internal class SpoolFile
	{
		public SpoolFile(string name, string userdata, string job, string status, int fileNumber)
		{
			Name       = name;
			UserData   = userdata;
			Job        = job;
			Status     = status;
			FileNumber = fileNumber;
		}

		public string Name { get; }

		public string UserData { get; }

		public string Job { get; }

		public string Status { get; }

		public int FileNumber { get; } //todo not used

		public string Download()
		{
			return IBMiUtils.DownloadSpoolFile(Name, FileNumber, Job);
		}
	}
}