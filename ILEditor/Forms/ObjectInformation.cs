using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class ObjectInformation : Form
    {
        public ObjectInformation(ILEObject Object)
        {
            InitializeComponent();

            objInfo.Items.Add(new ListViewItem(new string[] { "Location", Object.Library + "/" + Object.Name }));
            objInfo.Items.Add(new ListViewItem(new string[] { "Text", Object.Text }));
            objInfo.Items.Add(new ListViewItem(new string[] { "Size (KB)", Object.SizeKB.ToString() }));
            objInfo.Items.Add(new ListViewItem(new string[] { "Program Type", Object.Type }));
            objInfo.Items.Add(new ListViewItem(new string[] { "Owner", Object.Owner }));

            if (Object.SrcSpf != "")
            {
                objInfo.Items.Add(new ListViewItem(new string[] { "Extension", Object.Extension }));
                objInfo.Items.Add(new ListViewItem(new string[] { "Source Path", Object.SrcLib + "/" + Object.SrcSpf + "(" + Object.SrcMbr + ")" }));
            }

        }
    }
}
