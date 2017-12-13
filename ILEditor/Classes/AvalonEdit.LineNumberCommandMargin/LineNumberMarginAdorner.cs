using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    public class LineNumberMarginAdorner : Adorner
    {

        public LineNumberMarginAdorner(LineNumberMarginWithCommands marginElement,
                                    System.Windows.Size lineNumberDisplaySize)
            : base(marginElement)
        {
            this.listView = new LineNumbersListView();
            this.listView.Width = lineNumberDisplaySize.Width; // constrain width
            this.AddVisualChild(this.listView); // this has to be there for events and interaction to work
            marginElement.LineNumbersChangedDelayedEvent += MarginElement_LineNumbersChangedDelayedEvent;
            marginElement.MaxLineNumberLengthChanged += MarginElement_MaxLineNumberLengthChanged;

        }

        private void MarginElement_MaxLineNumberLengthChanged(object sender, MaxLineNumberLengthChangedEventArgs args)
        {
            // width of list needs to change
            this.listView.Width = args.NewSize.Width;
        }


        private void MarginElement_LineNumbersChangedDelayedEvent(object sendor, EventArgs args)
        {
            this.listView.LineNumbers.Clear();
            var margin = sendor as LineNumberMarginWithCommands;
            if (margin != null && margin.uiLineInfoList != null)
            {
                foreach (var info in margin.uiLineInfoList)
                {
                    this.listView.LineNumbers.Add(new LineNumberDisplayModel
                    {
                        LineNumber = info.Number,
                        ControlHeight = info.RenderSize.Height
                    });
                }

                // update the adorner layer
                AdornerLayer.GetAdornerLayer(margin).Update();
            }
        }


        private LineNumbersListView listView;



        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return listView;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            listView.Measure(constraint);
            return listView.DesiredSize;
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            listView.Arrange(new Rect(new Point(0, 0), finalSize));
            return new Size(listView.ActualWidth, listView.ActualHeight);
        }

    }
}
