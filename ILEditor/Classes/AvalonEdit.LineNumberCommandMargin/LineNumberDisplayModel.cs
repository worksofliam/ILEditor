using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    public class LineNumberDisplayModel : WPFHelpers.ViewModelBase
    {

        public int LineNumber
        {
            get { return this.GetValue(() => this.LineNumber); }
            set { this.SetValue(() => this.LineNumber, value); }
        }

        public string CommandText
        {
            get { return this.GetValue(() => this.CommandText); }
            set { this.SetValue(() => this.CommandText, value); }
        }



    }
}
