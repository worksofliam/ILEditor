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
	public class TabStyleChromeProvider : TabStyleProvider
	{
		public TabStyleChromeProvider(TabControlExtra tabControl) : base(tabControl){
			this.Overlap = 16;
			//this.Radius = 16;
			this.ShowTabCloser = true;

            this.CloserColorFocused = Color.Black;
            this.CloserColorFocusedActive = Color.White;
            this.CloserColorSelected = Color.Black;
            this.CloserColorSelectedActive = Color.White;
            this.CloserColorHighlighted = Color.Black;
            this.CloserColorHighlightedActive = Color.White;
            this.CloserColorUnselected = Color.Empty;

            this.CloserButtonFillColorFocused = Color.Empty;
            this.CloserButtonFillColorFocusedActive = Color.FromArgb(244, 159, 148);
            this.CloserButtonFillColorSelected = Color.Empty;
            this.CloserButtonFillColorSelectedActive = Color.FromArgb(244, 159, 148);
            this.CloserButtonFillColorHighlighted = Color.Empty;
            this.CloserButtonFillColorHighlightedActive = Color.FromArgb(244, 159, 148);
            this.CloserButtonFillColorUnselected = Color.Empty;

            this.CloserButtonOutlineColorFocused = Color.Empty;
            this.CloserButtonOutlineColorFocusedActive = Color.FromArgb(209, 106, 94);
            this.CloserButtonOutlineColorSelected = Color.Empty;
            this.CloserButtonOutlineColorSelectedActive = Color.FromArgb(209, 106, 94);
            this.CloserButtonOutlineColorHighlighted = Color.Empty;
            this.CloserButtonOutlineColorHighlightedActive = Color.FromArgb(209, 106, 94);
            this.CloserButtonOutlineColorUnselected = Color.Empty;

			
			//	Must set after the _Radius as this is used in the calculations of the actual padding
			this.Padding = new Point(16, 5);
		}
		
		public override void AddTabBorder(System.Drawing.Drawing2D.GraphicsPath path, System.Drawing.Rectangle tabBounds){

			int spread;
			int eigth;
			int sixth;
			int quarter;

			if (this.TabControl.Alignment <= TabAlignment.Bottom){
				spread = (int)Math.Floor((decimal)tabBounds.Height * 2/3);
				eigth = (int)Math.Floor((decimal)tabBounds.Height * 1/8);
				sixth = (int)Math.Floor((decimal)tabBounds.Height * 1/6);
				quarter = (int)Math.Floor((decimal)tabBounds.Height * 1/4);
			} else {
				spread = (int)Math.Floor((decimal)tabBounds.Width * 2/3);
				eigth = (int)Math.Floor((decimal)tabBounds.Width * 1/8);
				sixth = (int)Math.Floor((decimal)tabBounds.Width * 1/6);
				quarter = (int)Math.Floor((decimal)tabBounds.Width * 1/4);
			}
			
			switch (this.TabControl.Alignment) {
				case TabAlignment.Top:
					
					path.AddCurve(new Point[] {  new Point(tabBounds.X, tabBounds.Bottom)
					              		,new Point(tabBounds.X + sixth, tabBounds.Bottom - eigth)
					              		,new Point(tabBounds.X + spread - quarter, tabBounds.Y + eigth)
					              		,new Point(tabBounds.X + spread, tabBounds.Y)});
					path.AddLine(tabBounds.X + spread, tabBounds.Y, tabBounds.Right - spread, tabBounds.Y);
					path.AddCurve(new Point[] {  new Point(tabBounds.Right - spread, tabBounds.Y)
					              		,new Point(tabBounds.Right - spread + quarter, tabBounds.Y + eigth)
					              		,new Point(tabBounds.Right - sixth, tabBounds.Bottom - eigth)
					              		,new Point(tabBounds.Right, tabBounds.Bottom)});
					break;
				case TabAlignment.Bottom:
					path.AddCurve(new Point[] {  new Point(tabBounds.Right, tabBounds.Y)
					              		,new Point(tabBounds.Right - sixth, tabBounds.Y + eigth)
					              		,new Point(tabBounds.Right - spread + quarter, tabBounds.Bottom - eigth)
					              		,new Point(tabBounds.Right - spread, tabBounds.Bottom)});
					path.AddLine(tabBounds.Right - spread, tabBounds.Bottom, tabBounds.X + spread, tabBounds.Bottom);
					path.AddCurve(new Point[] {  new Point(tabBounds.X + spread, tabBounds.Bottom)
					              		,new Point(tabBounds.X + spread - quarter, tabBounds.Bottom - eigth)
					              		,new Point(tabBounds.X + sixth, tabBounds.Y + eigth)
					              		,new Point(tabBounds.X, tabBounds.Y)});
					break;
				case TabAlignment.Left:
					path.AddCurve(new Point[] {  new Point(tabBounds.Right, tabBounds.Bottom)
					              		,new Point(tabBounds.Right - eigth, tabBounds.Bottom - sixth)
					              		,new Point(tabBounds.X + eigth, tabBounds.Bottom - spread + quarter)
					              		,new Point(tabBounds.X, tabBounds.Bottom - spread)});
					path.AddLine(tabBounds.X, tabBounds.Bottom - spread, tabBounds.X ,tabBounds.Y + spread);
					path.AddCurve(new Point[] {  new Point(tabBounds.X, tabBounds.Y + spread)
					              		,new Point(tabBounds.X + eigth, tabBounds.Y + spread - quarter)
					              		,new Point(tabBounds.Right - eigth, tabBounds.Y + sixth)
					              		,new Point(tabBounds.Right, tabBounds.Y)});

					break;
				case TabAlignment.Right:
					path.AddCurve(new Point[] {  new Point(tabBounds.X, tabBounds.Y)
					              		,new Point(tabBounds.X + eigth, tabBounds.Y + sixth)
					              		,new Point(tabBounds.Right - eigth, tabBounds.Y + spread - quarter)
					              		,new Point(tabBounds.Right, tabBounds.Y + spread)});
					path.AddLine(tabBounds.Right, tabBounds.Y + spread, tabBounds.Right, tabBounds.Bottom - spread);
					path.AddCurve(new Point[] {  new Point(tabBounds.Right, tabBounds.Bottom - spread)
					              		,new Point(tabBounds.Right - eigth, tabBounds.Bottom - spread + quarter)
					              		,new Point(tabBounds.X + eigth, tabBounds.Bottom - sixth)
					              		,new Point(tabBounds.X, tabBounds.Bottom)});
					break;
			}
		}

        protected internal override GraphicsPath GetTabCloserPath(Rectangle closerRect) {
            GraphicsPath closerPath = new GraphicsPath();
            closerPath.AddLine(closerRect.X+4, closerRect.Y+4, closerRect.Right-4, closerRect.Bottom-4);
            closerPath.CloseFigure();
            closerPath.AddLine(closerRect.Right-4, closerRect.Y+4, closerRect.X+4, closerRect.Bottom-4);
            closerPath.CloseFigure();

            return closerPath;
        }

        protected internal override GraphicsPath GetTabCloserButtonPath(Rectangle closerRect) {
			GraphicsPath closerPath = new GraphicsPath();
			closerPath.AddEllipse(new Rectangle(closerRect.X, closerRect.Y, closerRect.Width, closerRect.Height));
			closerPath.CloseFigure();
			return closerPath;
		}
	}
}
