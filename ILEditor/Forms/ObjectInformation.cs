using System.Windows.Forms;
using ILEditor.Classes;

namespace ILEditor.Forms
{
	public partial class ObjectInformation : Form
	{
		public ObjectInformation(ILEObject Object)
		{
			InitializeComponent();

			objInfo.Items.Add(new ListViewItem(new[] {"Location", Object.Library + "/" + Object.Name}));
			objInfo.Items.Add(new ListViewItem(new[] {"Text", Object.Text}));
			objInfo.Items.Add(new ListViewItem(new[] {"Size (KB)", Object.SizeKB.ToString()}));
			objInfo.Items.Add(new ListViewItem(new[] {"Program Type", Object.Type}));
			objInfo.Items.Add(new ListViewItem(new[] {"Owner", Object.Owner}));

			if (Object.SrcSpf != "")
			{
				objInfo.Items.Add(new ListViewItem(new[] {"Extension", Object.Extension}));
				objInfo.Items.Add(new ListViewItem(new[]
				{
					"Source Path", Object.SrcLib + "/" + Object.SrcSpf + "(" + Object.SrcMbr + ")"
				}));
			}
		}
	}
}