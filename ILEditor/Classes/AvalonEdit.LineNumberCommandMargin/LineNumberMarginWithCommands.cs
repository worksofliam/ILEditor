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
                var adorner1 = new LineNumberMarginAdorner(me, me.previousLineNumberDisplaySize);
                // it's got to be displayed before adorning I think
                // adorn it
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


        private int previousMaxLineNumberLength = -1;
        private System.Windows.Size previousLineNumberDisplaySize; // only change this when line number length changes


        private static System.Windows.Size recalculateLineNumberDisplayControlSize(int maxLineNumberLength,
                                                                            double lineHeight,
                                                                            System.Windows.Size availableSize)
        {
            var lineNumberDisplayExampleCtrl = new LineNumberDisplay();
            lineNumberDisplayExampleCtrl.Model = new LineNumberDisplayModel();
            lineNumberDisplayExampleCtrl.Model.ControlHeight = lineHeight;
            lineNumberDisplayExampleCtrl.Model.IsInView = true;
            lineNumberDisplayExampleCtrl.Model.LineNumber = Convert.ToInt32(new string('9', maxLineNumberLength));
            
            lineNumberDisplayExampleCtrl.Measure(availableSize);
            availableSize.Width = 1;
            lineNumberDisplayExampleCtrl.Arrange(new Rect(availableSize));

            return new System.Windows.Size(lineNumberDisplayExampleCtrl.ActualWidth, lineHeight);
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            if( this.maxLineNumberLength != this.previousMaxLineNumberLength)
            {
                double lineHeight = 1;
                if(this.TextView != null && this.TextView.VisualLinesValid)
                {
                    lineHeight = this.TextView.VisualLines.First().Height;
                }

                this.previousLineNumberDisplaySize = recalculateLineNumberDisplayControlSize(this.maxLineNumberLength, lineHeight, availableSize);

                if( this.MaxLineNumberLengthChanged != null)
                {
                    this.MaxLineNumberLengthChanged(this, new MaxLineNumberLengthChangedEventArgs
                    {
                        NewSize = this.previousLineNumberDisplaySize
                    });
                }
            }

            return new Size(this.previousLineNumberDisplaySize.Width, 0);
        }




        public List<LineInfo> uiLineInfoList { get; set; } = new List<LineInfo>();


        // do a delayed event when the line info list is updated
        public event Action<object, EventArgs> LineNumbersChangedDelayedEvent;



        public event Action<object, MaxLineNumberLengthChangedEventArgs> MaxLineNumberLengthChanged;


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
                    info.RenderSize = previousLineNumberDisplaySize;

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
