using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin.SEUCommands
{
    public interface ICommand
    {
        void Execute(IEnumerable<SEUCommandInfo> commands, TextEditor editor);
    }
}
