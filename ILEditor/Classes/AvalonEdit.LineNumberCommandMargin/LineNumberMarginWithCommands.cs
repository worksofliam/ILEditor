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
    public static class Extensions
    {
        public static Typeface CreateTypeface(this FrameworkElement fe)
        {
            return new Typeface((FontFamily)fe.GetValue(TextBlock.FontFamilyProperty),
                                (FontStyle)fe.GetValue(TextBlock.FontStyleProperty),
                                (FontWeight)fe.GetValue(TextBlock.FontWeightProperty),
                                (FontStretch)fe.GetValue(TextBlock.FontStretchProperty));
        }
    }

    // idea from: http://community.icsharpcode.net/forums/t/11706.aspx
    public class LineNumberMarginWithCommands : LineNumberMargin
    {

        public static void Install(TextEditor _editor)
        {
            var me = new LineNumberMarginWithCommands(_editor);

            me.Loaded += (_sender, args) =>
            {
                var adorner1 = new LineNumberMarginAdorner(me);
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


        Typeface typeface;
        double emSize;


        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            typeface = this.CreateTypeface();
            emSize = (double)GetValue(TextBlock.FontSizeProperty);

            FormattedText text = new FormattedText(
                new string('9', maxLineNumberLength),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                emSize,
                (Brush)GetValue(Control.ForegroundProperty)
            );
            return new Size(text.Width, 0);
        }


        public class LineInfo
        {
            public int Number { get; set; }
            public double uiXPos { get; set; }
            public double uiYPos { get; set; }

            public double uiTotalAvailableWidth { get; set; }
        }

        public List<LineInfo> uiLineInfoList { get; set; } = new List<LineInfo>();


        // do a delayed event when the line info list is updated
        public event Action<object, EventArgs> LineNumbersChangedDelayedEvent;



        protected override void OnRender(DrawingContext drawingContext)
        {
            //lineNumbersChangedDelayTimer.Stop(); // if we have ticks going then stop it because we've rendered line numbers again
            this.uiLineInfoList.Clear();
            TextView textView = this.TextView;
            Size renderSize = this.RenderSize;
            if (textView != null && textView.VisualLinesValid)
            {
                var foreground = (Brush)GetValue(Control.ForegroundProperty);
                foreach (VisualLine line in textView.VisualLines)
                {
                    var info = new LineInfo();
                    info.Number = line.FirstDocumentLine.LineNumber;
                    info.uiYPos = line.VisualTop - textView.VerticalOffset;


                    FormattedText text = new FormattedText(
                        info.Number.ToString(CultureInfo.CurrentCulture),
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        typeface, emSize, foreground
                    );

                    info.uiTotalAvailableWidth = renderSize.Width;
                    info.uiXPos = renderSize.Width - text.Width;

                    //drawingContext.DrawText(text, new Point(info.uiXPos,info.uiYPos));

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
