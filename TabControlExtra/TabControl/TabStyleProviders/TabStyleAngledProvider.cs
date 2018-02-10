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
	public class TabStyleAngledProvider : TabStyleDefaultProvider
	{
		public TabStyleAngledProvider(TabControlExtra tabControl) : base(tabControl){
			this.ImageAlign = ContentAlignment.MiddleRight;
			this.Overlap = 7;
			this.Radius = 10;
			
			//	Must set after the _Radius as this is used in the calculations of the actual padding
			this.Padding = new Point(10, 3);

		}
		
		public override void AddTabBorder(System.Drawing.Drawing2D.GraphicsPath path, System.Drawing.Rectangle tabBounds){
			switch (this.TabControl.Alignment) {
				case TabAlignment.Top:
					path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + this.Radius - 2, tabBounds.Y + 2);
					path.AddLine(tabBounds.X + this.Radius, tabBounds.Y, tabBounds.Right - this.Radius, tabBounds.Y);
					path.AddLine(tabBounds.Right - this.Radius + 2, tabBounds.Y + 2, tabBounds.Right, tabBounds.Bottom);
					break;
				case TabAlignment.Bottom:
					path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right - this.Radius + 2, tabBounds.Bottom - 2);
					path.AddLine(tabBounds.Right - this.Radius, tabBounds.Bottom, tabBounds.X + this.Radius, tabBounds.Bottom);
					path.AddLine(tabBounds.X + this.Radius - 2, tabBounds.Bottom - 2, tabBounds.X, tabBounds.Y);
					break;
				case TabAlignment.Left:
					path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X + 2, tabBounds.Bottom - this.Radius + 2);
					path.AddLine(tabBounds.X, tabBounds.Bottom - this.Radius, tabBounds.X, tabBounds.Y + this.Radius);
					path.AddLine(tabBounds.X + 2, tabBounds.Y + this.Radius - 2, tabBounds.Right, tabBounds.Y);
					break;
				case TabAlignment.Right:
					path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right - 2, tabBounds.Y + this.Radius - 2);
					path.AddLine(tabBounds.Right, tabBounds.Y + this.Radius, tabBounds.Right, tabBounds.Bottom - this.Radius);
					path.AddLine(tabBounds.Right - 2, tabBounds.Bottom - this.Radius + 2, tabBounds.X, tabBounds.Bottom);
					break;
			}
		}

	}
}
