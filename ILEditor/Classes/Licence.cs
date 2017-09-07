using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class Licence
    {
        public static readonly Dictionary<char, string> Keys = new Dictionary<char, string>()
        {
            { '1', "Y656" },
            { '2', "Wltq" },
            { '3', "Ww4C" },
            { '4', "DEPl" },
            { '5', "xTpb" },
            { '6', "6qYi" },
            { '7', "UTzm" },
            { '8', "7KH5" },
            { '9', "1Ofq" },
            { '0', "ar0v" },
            { 'A', "3T7c" },
            { 'B', "iz5O" },
            { 'C', "Rq1G" },
            { 'D', "PZ2q" },
            { 'E', "kCVH" },
            { 'F', "ktZj" },
            { 'G', "9VNc" },
            { 'H', "c3WK" },
            { 'I', "Mnb7" },
            { 'J', "sicj" },
            { 'K', "pSlC" },
            { 'L', "Sa8R" },
            { 'M', "1QuZ" },
            { 'N', "mhF0" },
            { 'O', "k89s" },
            { 'P', "c6hq" },
            { 'Q', "I8KG" },
            { 'R', "FsQP" },
            { 'S', "20lH" },
            { 'T', "at9Z" },
            { 'U', "TPF3" },
            { 'V', "Dktf" },
            { 'W', "KZZ9" },
            { 'X', "qnFU" },
            { 'Y', "BX8r" },
            { 'Z', "rfBm" },
            { 'a', "eNI4" },
            { 'b', "N2bD" },
            { 'c', "bEI6" },
            { 'd', "PFPC" },
            { 'e', "5gy0" },
            { 'f', "MRlh" },
            { 'g', "UgEv" },
            { 'h', "nHdU" },
            { 'i', "7pSr" },
            { 'j', "bfZi" },
            { 'k', "jZQu" },
            { 'l', "OFA6" },
            { 'm', "xbiN" },
            { 'n', "hZwB" },
            { 'o', "jpln" },
            { 'p', "VtRl" },
            { 'q', "OSKg" },
            { 'r', "CalW" },
            { 's', "4cyc" },
            { 't', "FFnY" },
            { 'u', "BLay" },
            { 'v', "DGV8" },
            { 'w', "OGrm" },
            { 'x', "41XV" },
            { 'y', "L6Jo" },
            { 'z', "bItZ" }
        };

        //XXXXX-XXXX-XXXX-XXXX-XXXX-XXXX
        public static Boolean IsValid(string Key)
        {
            String[] Parts;
            List<String> EncKeys = new List<string>();
            if (Key.Length == 30)
            {
                Parts = Key.Split('-');
                if (Parts.Length == 6)
                {
                    foreach(char c in Parts[0].ToCharArray())
                    {
                        EncKeys.Add(Keys[c]);
                    }
                    for(int i = 0; i < 5; i++)
                    {
                        if (Parts[i+1] != EncKeys[i])
                            return false;
                    }
                    
                    RegistryKey licencekey = Registry.CurrentUser.CreateSubKey("ileditor");
                    licencekey.SetValue("key", Key);
                    licencekey.Close();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static Boolean CheckExistsIsValid()
        {
            Boolean result = false;
            string key = "";
            RegistryKey licencekey = Registry.CurrentUser.CreateSubKey("ileditor");
            if (licencekey.GetValue("key") != null)
            {
                key = licencekey.GetValue("key").ToString();
                if (IsValid(key))
                    result = true;
            }

            licencekey.Close();

            return result;
        }
    }
}
