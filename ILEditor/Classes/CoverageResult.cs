using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class CoverageResult
    {
        public static bool Extract(string Location, string Folder = "")
        {
            //TODO: Extract zip
            //TODO: ccdata

            if (Folder == "")
                Folder = "results";
            
            string extractPath = Path.Combine(IBMiUtils.GetLocalDir("CODECOV", "TESTS"), Folder);

            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);

            ZipFile.ExtractToDirectory(Location, extractPath);

            return true;
        }
    }
}
