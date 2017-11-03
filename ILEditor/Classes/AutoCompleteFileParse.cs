using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    class AutoCompleteFileParse
    {
        public static AutocompleteItem[] ParseFile(string[] Lines)
        {
            List<AutocompleteItem> Items = new List<AutocompleteItem>();

            string[] data;
            foreach(string Line in Lines)
            {
                data = Line.Split('|');
                Items.Add(new AutocompleteItem(data[1], int.Parse(data[2]), data[0], data[0], data[1]));
            }

            return Items.ToArray();
        }
    }
}
