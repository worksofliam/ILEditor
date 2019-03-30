using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    public class MaxLineNumberLengthChangedEventArgs : EventArgs
    {
        public System.Windows.Size NewSize { get; set; }
    }
}
