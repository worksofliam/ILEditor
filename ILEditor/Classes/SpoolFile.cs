namespace ILEditor.Classes
{
	internal class SpoolFile
	{
		private readonly int    FILE_NUMBER;
		private readonly string JOB_NAME;
		private readonly string SPOOLED_FILE_NAME;
		private readonly string STATUS;
		private readonly string USER_DATA;

		public SpoolFile(string name, string userdata, string job, string status, int fileNumber)
		{
			SPOOLED_FILE_NAME = name;
			USER_DATA         = userdata;
			JOB_NAME          = job;
			STATUS            = status;
			FILE_NUMBER       = fileNumber;
		}

		public string GetName()
		{
			return SPOOLED_FILE_NAME;
		}

		public string GetData()
		{
			return USER_DATA;
		}

		public string GetJob()
		{
			return JOB_NAME;
		}

		public string GetStatus()
		{
			return STATUS;
		}

		public int GetFileNumber() //todo: not used
		{
			return FILE_NUMBER;
		}

		public string Download()
		{
			return IBMiUtils.DownloadSpoolFile(SPOOLED_FILE_NAME, FILE_NUMBER, JOB_NAME);
		}
	}
}