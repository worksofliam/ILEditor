using ICSharpCode.AvalonEdit.CodeCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System.Windows.Media;

namespace ILEditor.Classes
{
    class AutoCompleteData : ICompletionData
    {
        public static ImageSource icon = Editor.ConvertBitmap(Properties.Resources.cube);
        public AutoCompleteData(string text, string desc)
        {
            this.Text = text;
            this.Description = desc;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return icon; }
        }

        public string Text { get; private set; }
        public object Description { get; private set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return this.Text; }
        }

        public double Priority => 1;

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }
    }
}
