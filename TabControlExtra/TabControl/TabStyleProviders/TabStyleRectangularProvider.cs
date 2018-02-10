/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Windows.Forms {

    [System.ComponentModel.ToolboxItem(false)]
    public class TabStyleRectangularProvider : TabStyleProvider {
        public TabStyleRectangularProvider(TabControlExtra tabControl) : base(tabControl) {
            this.Radius = 1;
            this.ShowTabCloser = true;

            this.CloserColorFocused = Color.FromArgb(208, 230, 245);
            this.CloserColorFocusedActive = Color.White;
            this.CloserColorSelected = Color.FromArgb(109, 109, 112);
            this.CloserColorSelectedActive = Color.FromArgb(113, 113, 113);
            this.CloserColorHighlighted = Color.FromArgb(129, 195, 241);
            this.CloserColorHighlightedActive = Color.White;
            this.CloserColorUnselected = Color.Empty;

            this.CloserButtonFillColorFocused = Color.Empty;
            this.CloserButtonFillColorFocusedActive = Color.FromArgb(28, 151, 234);
            this.CloserButtonFillColorSelected = Color.Empty;
            this.CloserButtonFillColorSelectedActive = Color.FromArgb(230, 231, 237);
            this.CloserButtonFillColorHighlighted = Color.Empty;
            this.CloserButtonFillColorHighlightedActive = Color.FromArgb(82, 176, 239);
            this.CloserButtonFillColorUnselected = Color.Empty;

            this.CloserButtonOutlineColorFocused = Color.Empty; //Color.FromArgb(0, 122, 204);
            this.CloserButtonOutlineColorFocusedActive = Color.Empty;
            this.CloserButtonOutlineColorSelected = Color.Empty;
            this.CloserButtonOutlineColorSelectedActive = Color.Empty;
            this.CloserButtonOutlineColorHighlighted = Color.Empty; //Color.FromArgb(28, 151, 234);
            this.CloserButtonOutlineColorHighlightedActive = Color.Empty;
            this.CloserButtonOutlineColorUnselected = Color.Empty;

            this.TextColorFocused = Color.White;
            this.TextColorHighlighted = Color.White;
            this.TextColorSelected = Color.FromArgb(113, 113, 113);
            this.TextColorUnselected = Color.Black;

            this.PageBackgroundColorDisabled = SystemColors.Control;
            this.PageBackgroundColorFocused = Color.FromArgb(0, 122, 204);
            this.PageBackgroundColorHighlighted = Color.FromArgb(28, 151, 234);
            this.PageBackgroundColorSelected = Color.FromArgb(204, 206, 219);
            this.PageBackgroundColorUnselected = Color.FromArgb(239, 239, 242);

            this.BorderColorDisabled = this.PageBackgroundColorDisabled;
            this.BorderColorFocused = this.PageBackgroundColorFocused;
            this.BorderColorHighlighted = this.PageBackgroundColorHighlighted;
            this.BorderColorSelected = this.PageBackgroundColorSelected;
            this.BorderColorUnselected = this.PageBackgroundColorUnselected;

            this.TabPageRadius = 0;

            //	Must set after the _Radius as this is used in the calculations of the actual padding
            this.Padding = new Point(6, 5);

            this.TabPageMargin = new Padding(0, 2, 0, 2);

        }

    }
}
