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
        private ChromiumWebBrowser chromeBrowser;
        private string LocalFile;

        public ObjectDiagram(string Local)
        {
            InitializeComponent();
            this.LocalFile = Local;

            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(this.LocalFile);
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void ObjectDiagram_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            chromeBrowser.Load(this.LocalFile);
        }
    }
}
