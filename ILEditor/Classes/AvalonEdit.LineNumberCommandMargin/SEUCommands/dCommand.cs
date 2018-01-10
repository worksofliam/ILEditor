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
            var cmds = (from c in commands
                       where string.Equals(c.CommandText, "d", StringComparison.OrdinalIgnoreCase)
                       select c
                       ).ToList();// make a copy of it



            foreach( var c in cmds)
            {
                int commandsIndex = commands.IndexOf(c); // it's being removed from commands so grab it's index prior to that
                commands.Remove(c); // remove it from the margin since it's line is about to be deleted

                var editorLine = editor.Document.GetLineByNumber(c.LineNumber);
                // remove the line from AvalonEdit
                editor.Document.Remove(editorLine.Offset, editorLine.NextLine.Offset - editorLine.Offset);
                

                
                // we've deleted a line so we have to renumber all lines after it
                int lineNumber = c.LineNumber;
                
                // things will shift down so we want to renumber starting with the old index of our removed line
                for( int i = commandsIndex; i < commands.Count; ++i )
                {
                    var cmdToRenumber = commands[i];
                    cmdToRenumber.LineNumber = lineNumber++; // assign current line number then incriment it
                }
            }
        }
    }
}
