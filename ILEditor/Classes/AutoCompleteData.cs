using ICSharpCode.AvalonEdit.CodeCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System.Windows.Media;
using System.Windows.Documents;
using ILEditor.UserTools;

namespace ILEditor.Classes
{
    class AutoCompleteData : ICompletionData
    {
        public AutoCompleteData(string text, string desc, SourceEditor sourceEditor)
        {
            this.Text = text;
            this.Description = desc;
			this.Editor = sourceEditor;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }
        public object Description { get; private set; }
		public SourceEditor Editor { get; private set; }

		// Use this property if you want to show a fancy UIElement in the list.
		public object Content
        {
            get { return this.Text; }
        }

        public double Priority => 1;

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
  			int begin =  TextUtilities.GetNextCaretPosition(textArea.Document, textArea.Caret.Offset, LogicalDirection.Backward, CaretPositioningMode.WordStart);
			int end = textArea.Caret.Offset;
			textArea.Document.Replace(begin, (end-begin),  this.Text);
			this.Editor.resetWordBuilder();

		}
    }
}
