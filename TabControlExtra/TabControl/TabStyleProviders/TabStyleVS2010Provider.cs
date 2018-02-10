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
	public class TabStyleVS2010Provider : TabStyleRoundedProvider
	{
		public TabStyleVS2010Provider(TabControlExtra tabControl) : base(tabControl){
			this.Radius = 3;
			this.ShowTabCloser = true;
			
            this.CloserColorFocused = Color.FromArgb(117, 99, 61);
            this.CloserColorFocusedActive = Color.Black;
            this.CloserColorSelected = Color.FromArgb(95,102,115);
            this.CloserColorSelectedActive = Color.Black;
            this.CloserColorHighlighted = Color.FromArgb(206, 212, 221);
            this.CloserColorHighlightedActive = Color.Black;
            this.CloserColorUnselected = Color.Empty;

            this.CloserButtonFillColorFocused = Color.Empty;
            this.CloserButtonFillColorFocusedActive = Color.White;
            this.CloserButtonFillColorSelected = Color.Empty;
            this.CloserButtonFillColorSelectedActive = Color.White;
            this.CloserButtonFillColorHighlighted = Color.Empty;
            this.CloserButtonFillColorHighlightedActive = Color.White;
            this.CloserButtonFillColorUnselected = Color.Empty;

            this.CloserButtonOutlineColorFocused = Color.Empty;
            this.CloserButtonOutlineColorFocusedActive = Color.FromArgb(229, 195, 101);
            this.CloserButtonOutlineColorSelected = Color.Empty;
            this.CloserButtonOutlineColorSelectedActive = Color.FromArgb(229, 195, 101);
            this.CloserButtonOutlineColorHighlighted = Color.Empty;
            this.CloserButtonOutlineColorHighlightedActive = Color.FromArgb(229, 195, 101);
            this.CloserButtonOutlineColorUnselected = Color.Empty;

            this.TextColorUnselected = Color.White;
			this.TextColorDisabled = Color.WhiteSmoke;
            this.BorderColorDisabled = Color.FromArgb(41, 57, 85);
            this.BorderColorFocused = Color.FromArgb(255, 243, 205);
            this.BorderColorHighlighted = Color.FromArgb(155, 167, 183);
            this.BorderColorSelected = Color.FromArgb(206, 212, 223);
            this.BorderColorUnselected = Color.FromArgb(41, 57, 85);

            this.PageBackgroundColorDisabled = Color.FromArgb(41, 57, 85);
            this.PageBackgroundColorFocused = Color.FromArgb(255, 243, 205);
            this.PageBackgroundColorHighlighted = Color.FromArgb(75,92,116);
            this.PageBackgroundColorSelected = Color.FromArgb(206, 212, 223);
            this.PageBackgroundColorUnselected = Color.FromArgb(41, 57, 85);

            this.TabColorDisabled1 = this.PageBackgroundColorDisabled;
            this.TabColorDisabled2 = this.TabColorDisabled1;
            this.TabColorFocused1 = this.PageBackgroundColorFocused;
            this.TabColorFocused2 = this.TabColorFocused1;
            this.TabColorHighLighted1 = this.PageBackgroundColorHighlighted;
            this.TabColorHighLighted2 = this.TabColorHighLighted1;
            this.TabColorSelected1 = this.PageBackgroundColorSelected;
            this.TabColorSelected2 = this.TabColorSelected1;
            this.TabColorUnSelected1 = this.PageBackgroundColorUnselected;
            this.TabColorUnSelected1 = this.TabColorUnSelected1;

            //	Must set after the _Radius as this is used in the calculations of the actual padding
			this.Padding = new Point(6, 5);

            this.TabPageMargin = new Padding(0, 4, 0, 4);
            this.TabPageRadius = 2;     
            
		}
	
	}
}
