using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin.SEUCommands
{
    public static class SEUCommandHandler
    {

        public static void ExecuteCommands(IEnumerable<SEUCommandInfo> commands, TextEditor editor)
        {
            var d = new dCommand();
            d.Execute(commands, editor);
            var dd = new ddCommand();
            dd.Execute(commands, editor);
        }


        


    }
}
