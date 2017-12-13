using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            // update the adorner layer
            AdornerLayer.GetAdornerLayer(marginElement).Update();

            // setup events that we will need to use to modify our list of line numbers
            marginElement.LineNumbersChangedDelayedEvent += MarginElement_LineNumbersChangedDelayedEvent;
            marginElement.MaxLineNumberLengthChanged += MarginElement_MaxLineNumberLengthChanged;

            // need to initially populate line numbers that are already there
            populateLineNumbers(marginElement.uiLineInfoList, this.listView.LineNumbers);
        }

        private void MarginElement_MaxLineNumberLengthChanged(object sender, MaxLineNumberLengthChangedEventArgs args)
        {
            // width of list needs to change
            this.listView.Width = args.NewSize.Width;
        }


        private void populateLineNumbers(List<LineInfo> textLineInfoList,
                                        ObservableCollection<LineNumberDisplayModel> visualLines)
        {
            // determine what needs to be created and what needs hidden
            var nonExistantTextLines = from t in textLineInfoList
                                       where !visualLines.Any(v => v.LineNumber == t.Number)
                                       select t;

            var visualsToHide = from v in visualLines
                                where !textLineInfoList.Any(t => t.Number == v.LineNumber)
                                select v;

            var visualsToShow = from v in visualLines
                                where textLineInfoList.Any(t => t.Number == v.LineNumber)
                                select v;

            // create 
            foreach( var t in nonExistantTextLines)
            {
                visualLines.Add(new LineNumberDisplayModel
                {
                    IsInView = true,
                    LineNumber = t.Number,
                    ControlHeight = t.RenderSize.Height
                });
            }

            // hide
            foreach( var v in visualsToHide)
            {
                v.IsInView = false;
            }

            // show
            foreach(var v in visualsToShow)
            {
                v.IsInView = true;
            }
        }

        private void MarginElement_LineNumbersChangedDelayedEvent(object sendor, EventArgs args)
        {
            var margin = sendor as LineNumberMarginWithCommands;
            populateLineNumbers(margin.uiLineInfoList, this.listView.LineNumbers);
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
