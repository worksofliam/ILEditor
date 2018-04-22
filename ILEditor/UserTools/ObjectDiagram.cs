using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using CefSharp;
using CefSharp.WinForms;

namespace ILEditor.UserTools
{
    public partial class ObjectDiagram : DockContent
    {
        public static bool ChromiumActive = false;

        private ChromiumWebBrowser chromeBrowser;
        private string LocalFile;

        public ObjectDiagram(string Local)
        {
            if (ChromiumActive == false)
            {
                CefSettings settings = new CefSettings();
                Cef.Initialize(settings);
                ChromiumActive = true;
            }

            InitializeComponent();
            this.LocalFile = Local;

            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(this.LocalFile);
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            chromeBrowser.Load(this.LocalFile);
        }
    }
}
