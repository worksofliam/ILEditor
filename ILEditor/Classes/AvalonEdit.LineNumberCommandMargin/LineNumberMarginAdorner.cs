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

        public LineNumberMarginAdorner(LineNumberMarginWithCommands marginElement)
            : base(marginElement)
        {
            marginElement.LineNumbersChangedDelayedEvent += MarginElement_LineNumbersChangedDelayedEvent;

        }

        public class LineNumberDisplayControlInfo
        {
            public LineNumberMarginWithCommands.LineInfo LineInfo { get; set; }
            public LineNumberDisplay Control { get; set; }
        }

        private List<LineNumberDisplayControlInfo> Controls = new List<LineNumberDisplayControlInfo>();
        private List<Visual> visualChildTracker = new List<Visual>();

        private void ClearVisualChilds()
        {
            foreach (var _child in this.visualChildTracker.ToList())
            {
                this.visualChildTracker.Remove(_child);
                this.RemoveVisualChild(_child);
            }
        }

        private void AddVisualChildWithTracking(Visual _child)
        {
            this.AddVisualChild(_child);
            this.visualChildTracker.Add(_child);
        }

        private void MarginElement_LineNumbersChangedDelayedEvent(object sendor, EventArgs args)
        {
            this.Controls.Clear();
            this.ClearVisualChilds();
            var margin = sendor as LineNumberMarginWithCommands;
            if (margin != null && margin.uiLineInfoList != null)
            {
                foreach (var info in margin.uiLineInfoList)
                {
                    var ctrl = new LineNumberDisplayControlInfo
                    {
                        LineInfo = info
                    };

                    ctrl.Control = new LineNumberDisplay();
                    ctrl.Control.Model.LineNumber = ctrl.LineInfo.Number;

                    this.Controls.Add(ctrl);
                    this.AddVisualChildWithTracking(ctrl.Control);
                }

                // update the adorner layer
                AdornerLayer.GetAdornerLayer(margin).Update();
            }
        }




        protected override int VisualChildrenCount
        {
            get
            {
                if (this.Controls.Any())
                {
                    return this.Controls.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.Controls[index].Control;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            if (this.Controls.Any())
            {
                var last = this.Controls.Last();
                foreach (var ctrl in this.Controls)
                {
                    ctrl.Control.Measure(constraint);
                }
                return last.Control.DesiredSize;
            }
            else
            {
                return new Size(0, 0);
            }
        }


        protected override Size ArrangeOverride(Size finalSize)
        {

            if (this.Controls.Any())
            {
                var last = this.Controls.Last();

                foreach (var ctrl in this.Controls)
                {
                    ctrl.Control.Arrange(new Rect(new Point(0, ctrl.LineInfo.uiYPos), finalSize));
                }

                return new Size(last.Control.ActualWidth, last.Control.ActualHeight);
            }
            else
            {
                return new Size(0, 0);
            }

        }

    }
}
