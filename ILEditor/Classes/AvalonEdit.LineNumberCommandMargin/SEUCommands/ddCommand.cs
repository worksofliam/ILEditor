using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin.SEUCommands
{
    public class ddCommand : ICommand
    {
        public void Execute(ObservableCollection<LineNumberDisplayModel> commands, TextEditor editor)
        {
            var firstDD = commands.OrderBy(c => c.LineNumber).FirstOrDefault(c => string.Equals(c.CommandText, "dd", StringComparison.OrdinalIgnoreCase));
            var lastDD = commands.OrderBy(c => c.LineNumber).LastOrDefault(c => string.Equals(c.CommandText, "dd", StringComparison.OrdinalIgnoreCase));

            if( firstDD != null && lastDD != null)
            {
                // delete the lines
                int posOfFirstShiftedLineCommand = commands.IndexOf(firstDD); // this is the pos of the first shifted because a bunch are now gone
                var commandsToDelete = commands.Where(c => c.LineNumber >= firstDD.LineNumber && c.LineNumber <= lastDD.LineNumber);

                // delete out the line number commands
                commandsToDelete.ToList().ForEach(c => commands.Remove(c));
                // delete out the line from AvalonEdit
                commandsToDelete.ToList().ForEach(c => AvalonEditUtility.DeleteLine(editor, c.LineNumber));

                // starting from line after lastDD shift the line numbers
                int lineNumber = firstDD.LineNumber; // this is the new line number to start with for everything shifted

                for( int i = posOfFirstShiftedLineCommand; i < commands.Count; ++i)
                {
                    var commandToRenumber = commands[i];
                    commandToRenumber.LineNumber = lineNumber++; // use the line number then incriment it
                }
            }

        }
    }
}
