using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    public static class AvalonEditUtility
    {
        public static void DeleteLine( TextEditor editor, int lineNumber)
        {
            var editorLine = editor.Document.GetLineByNumber(lineNumber);
            // remove the line from AvalonEdit

            if( editorLine.PreviousLine != null && editorLine.NextLine != null)
            {
                // has both previous and next lines
                editor.Document.Remove(editorLine.PreviousLine.EndOffset, editorLine.NextLine.Offset - editorLine.PreviousLine.EndOffset);
            }else if( editorLine.NextLine != null)
            {
                // has no previous but has next
                editor.Document.Remove(editorLine.Offset, editorLine.NextLine.Offset - editorLine.Offset);
            }
            else if( editorLine.PreviousLine != null)
            {
                // has no next but has previous
                editor.Document.Remove(editorLine.PreviousLine.EndOffset, editorLine.EndOffset - editorLine.PreviousLine.EndOffset);
            }
            else
            {
                // has no next or previous
                editor.Document.Remove(editorLine); // this just clears contents of the line
            }
        }
    }
}
