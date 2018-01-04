using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin.SEUCommands
{
    public class dCommand : ICommand
    {
        public void Execute(IEnumerable<SEUCommandInfo> commands, TextEditor editor)
        {
            var cmds = from c in commands
                       where string.Equals(c.CommandText, "d", StringComparison.OrdinalIgnoreCase)
                       select c;

            foreach( var c in cmds)
            {
                var editorLine = editor.Document.GetLineByNumber(c.LineNumber);
                editor.Document.Remove(editorLine);
            }
        }
    }
}
