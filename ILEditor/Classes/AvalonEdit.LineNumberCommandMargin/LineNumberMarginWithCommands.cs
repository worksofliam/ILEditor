using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Threading;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{

    // idea from: http://community.icsharpcode.net/forums/t/11706.aspx
    public class LineNumberMarginWithCommands : LineNumberMargin
    {


        public static void Install(TextEditor _editor)
        {
            var me = new LineNumberMarginWithCommands(_editor);

            me.Loaded += (_sender, args) =>
            {
                var adorner1 = new LineNumberMarginAdorner(me, _editor);
                // it's got to be displayed before adorning I think
                // adorn it
                adorner1.LineNumberListViewWidthChanged += (_sender2, _args2) =>
                {
                    me.UpdateLineNumberListWidthFromAdorner(_args2.Width);
                };

                AdornerLayer.GetAdornerLayer(me).Add(adorner1);
            };

            _editor.ShowLineNumbers = false; // turn off the built in

            addLineNumberMarching(_editor, me);

        }


        private static void addLineNumberMarching(TextEditor _editor, LineNumberMargin lineNumbers)
        {
            var leftMargins = _editor.TextArea.LeftMargins;

            Line line = (Line)DottedLineMargin.Create();
            leftMargins.Insert(0, lineNumbers);
            leftMargins.Insert(1, line);
            var lineNumbersForeground = new Binding("LineNumbersForeground") { Source = _editor };
            line.SetBinding(Line.StrokeProperty, lineNumbersForeground);
            lineNumbers.SetBinding(Control.ForegroundProperty, lineNumbersForeground);

        }

        public LineNumberMarginWithCommands(TextEditor _editor)
        {

        }

        private double lineHeight = 1;

        private double lineNumberListViewWidth = 0;
        public void UpdateLineNumberListWidthFromAdorner(double width)
        {
            if (width != lineNumberListViewWidth)
            {
                this.lineNumberListViewWidth = width;
                this.InvalidateMeasure();
            }
        }



        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            if (this.TextView != null && this.TextView.VisualLinesValid)
            {
                lineHeight = this.TextView.VisualLines.First().Height;
            }

            return new Size(this.lineNumberListViewWidth, 0);
        }




        public List<LineInfo> uiLineInfoList { get; set; } = new List<LineInfo>();


        // do a delayed event when the line info list is updated
        public event Action<object, EventArgs> LineNumbersChangedDelayedEvent;

        private bool doWeNeedToRedoLineNumberDisplay(IReadOnlyCollection<VisualLine> visualLines,
                                                       List<LineInfo> linesDisplayed)
        {
            var firstVisual = visualLines.FirstOrDefault();
            var lastVisual = visualLines.LastOrDefault();

            var firstLine = linesDisplayed.FirstOrDefault();
            var lastLine = linesDisplayed.LastOrDefault();

            if(firstVisual?.FirstDocumentLine?.LineNumber == firstLine?.Number
                && lastVisual?.FirstDocumentLine?.LineNumber == lastLine?.Number
                )
            {
                return false;
            }

            return true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            
            TextView textView = this.TextView;
            if (textView != null 
                && textView.VisualLinesValid
                && doWeNeedToRedoLineNumberDisplay(textView.VisualLines, this.uiLineInfoList)
                )
            {

                // repopulate the ui list
                this.uiLineInfoList.Clear();
                foreach (VisualLine line in textView.VisualLines)
                {
                    var info = new LineInfo();
                    info.Number = line.FirstDocumentLine.LineNumber;
                    info.LineHeight = this.lineHeight;

                    this.uiLineInfoList.Add(info);
                }

                // finished processing line numbers

                if (this.LineNumbersChangedDelayedEvent != null)
                {
                    this.LineNumbersChangedDelayedEvent(this, new EventArgs());
                }


            }
        }

    }
}
