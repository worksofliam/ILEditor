/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Windows.Forms
{

    [System.ComponentModel.ToolboxItem(false)]
    public class TabStyleIE8Provider : TabStyleRoundedProvider {
        public TabStyleIE8Provider(TabControlExtra tabControl)
            : base(tabControl) {
            this.Radius = 3;
            this.ShowTabCloser = true;
            this.SelectedTabIsLarger = true;

            this.CloserColorFocusedActive = Color.Red;
            this.CloserColorFocused = Color.Black;
            this.CloserColorSelected = Color.Black;
            this.CloserColorHighlighted = Color.Black;
            this.CloserColorUnselected = Color.Empty;

            this.CloserButtonFillColorFocusedActive = Color.White;
            this.CloserButtonFillColorFocused = Color.Empty;
            this.CloserButtonFillColorSelected = Color.Empty;
            this.CloserButtonFillColorHighlighted = Color.Empty;
            this.CloserButtonFillColorUnselected = Color.Empty;

            this.CloserButtonOutlineColorFocusedActive = SystemColors.ControlDark;
            this.CloserButtonOutlineColorFocused = Color.Empty;
            this.CloserButtonOutlineColorSelected = Color.Empty;
            this.CloserButtonOutlineColorHighlighted = Color.Empty;
            this.CloserButtonOutlineColorUnselected = Color.Empty;

            this.PageBackgroundColorDisabled = Color.FromArgb(247, 247, 255);
            this.PageBackgroundColorFocused = Color.FromArgb(247, 247, 255);
            this.PageBackgroundColorHighlighted = Color.FromArgb(247, 247, 255);
            this.PageBackgroundColorSelected = Color.FromArgb(247, 247, 255);
            this.PageBackgroundColorUnselected = Color.FromArgb(198, 223, 255);

            this.TabColorFocused2 = Color.FromArgb(198, 223, 255);
            this.TabColorHighLighted2 = Color.FromArgb(198, 223, 255);
            this.TabColorSelected2 = Color.FromArgb(198, 223, 255);

            this.Padding = new Point(6, 5);

            this.TabPageMargin = new Padding(0, 4, 0, 4);

        }

        protected internal override void PaintTabBackground(GraphicsPath tabBorder, TabState state, Graphics graphics) {
            // first draw a white-ish line inside the tab boundary
            var tabBounds = tabBorder.GetBounds();
            switch (this.TabControl.Alignment) {
                case TabAlignment.Bottom:
                    tabBounds.X += 1;
                    tabBounds.Width -= 2;
                    tabBounds.Height -= 1;
                    break;
                case TabAlignment.Top:
                    tabBounds.X += 1;
                    tabBounds.Width -= 2;
                    tabBounds.Y += 1;
                    break;
                case TabAlignment.Left:
                    tabBounds.X += 1;
                    tabBounds.Width -= 1;
                    tabBounds.Y += 1;
                    tabBounds.Height -= 2;
                    break;
                case TabAlignment.Right:
                    tabBounds.Width -= 1;
                    tabBounds.Y += 1;
                    tabBounds.Height -= 2;
                    break;
            }

            using (var pen = new Pen(Color.FromArgb(247, 247, 255))) {
                graphics.DrawPath(pen, tabBorder);
            }

            // now paint the tab so that the full gradient lies inside the white line
            tabBounds.X += 1;
            tabBounds.Width -= 2;
            tabBounds.Y += 1;
            tabBounds.Height -= 1;

            base.PaintTabBackground(GetTabBorder(Rectangle.Round(tabBounds)), state, graphics);
        }

    }

}
