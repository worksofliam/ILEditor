using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    public class CoverageTest
    {
        public static Dictionary<string, CoverageTest> Tests = new Dictionary<string, CoverageTest>();

        public static CoverageTest GetTest(string Name)
        {
            if (Tests.ContainsKey(Name))
                return Tests[Name];
            else
                return null;
        }

        public static void LoadTests()
        {

        }

        public static void SaveTests()
        {

        }

        #region Class
        public string Command;
        public List<ILEObject> Modules;

        public CoverageTest(string Command = "")
        {
            this.Command = Command;
            this.Modules = new List<ILEObject>();
        }
        #endregion
    }
}
