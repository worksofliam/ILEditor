using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin.SEUCommands
{
    public static class SEUCommandHandler
    {

        public static void ExecuteCommands(ObservableCollection<LineNumberDisplayModel> possibleCommands, TextEditor editor)
        {
            var d = new dCommand();
            d.Execute(possibleCommands, editor);
            var dd = new ddCommand();
            dd.Execute(possibleCommands, editor);
        }


        


    }
}
