using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin.SEUCommands
{
    public class dCommand : ICommand
    {
        public void Execute(ObservableCollection<LineNumberDisplayModel> commands, TextEditor editor)
        {
            var cmds = from c in commands
                       where string.Equals(c.CommandText, "d", StringComparison.OrdinalIgnoreCase)
                       select c;



            foreach( var c in cmds)
            {
                var editorLine = editor.Document.GetLineByNumber(c.LineNumber);
                editor.Document.Remove(editorLine);
                editor.Document.BeginUpdate();

                // we've deleted a line so we have to renumber all lines after it
                int lineNumber = c.LineNumber;
                c.LineNumber = -1;
                c.IsInView = false;
                // will also need to remove c at some point
                int commandsIndex = commands.IndexOf(c);
                for( int i = commandsIndex + 1; i < commands.Count; ++i )
                {
                    var cmdToRenumber = commands[i];
                    cmdToRenumber.LineNumber = lineNumber;
                    ++lineNumber; 
                }
            }
        }
    }
}
