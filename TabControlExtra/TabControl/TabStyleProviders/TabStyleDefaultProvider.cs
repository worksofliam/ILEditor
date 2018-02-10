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
	public class TabStyleDefaultProvider : TabStyleProvider
	{
		public TabStyleDefaultProvider(TabControlExtra tabControl) : base(tabControl){
			this.Radius = 2;

            this.SelectedTabIsLarger = true;

            this.TabColorHighLighted1 = Color.FromArgb(236, 244, 252);
            this.TabColorHighLighted2 = Color.FromArgb(221, 237, 252);

            this.PageBackgroundColorHighlighted = TabColorHighLighted1;
        }
		
	}
}
