using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class SpoolFile
    {
        private string SPOOLED_FILE_NAME;
        private string USER_DATA;
        private string JOB_NAME;
        private string STATUS;

        public SpoolFile(string name, string userdata, string job, string status)
        {
            this.SPOOLED_FILE_NAME = name;
            this.USER_DATA = userdata;
            this.JOB_NAME = job;
            this.STATUS = status;
        }

        public string getName()
        {
            return this.SPOOLED_FILE_NAME;
        }

        public string getData()
        {
            return this.USER_DATA;
        }

        public string getJob()
        {
            return this.JOB_NAME;
        }

        public string getStatus()
        {
            return this.STATUS;
        }

        public string Download()
        {
            return IBMiUtils.DownloadSpoolFile(this.SPOOLED_FILE_NAME, this.JOB_NAME);
        }
    }
}
