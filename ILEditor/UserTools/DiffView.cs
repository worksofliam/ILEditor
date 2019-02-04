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
using System.IO;
using ILEditor.Classes;
using System.Windows.Documents;

namespace ILEditor.UserTools
{
    public partial class DiffView : DockContent
    {
        public static void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public Color TextColor = Color.Black;
        public void HandleTheme(RichTextBox box)
        {
            var darkMode = (Program.Config.GetValue("darkmode") == "true");

            box.Font = new Font(IBMi.CurrentSystem.GetValue("FONT"), float.Parse(IBMi.CurrentSystem.GetValue("ZOOM")));
            box.WordWrap = false;

            if (darkMode)
            {
                box.BackColor = Color.FromArgb(30, 30, 30);
                TextColor = Color.White;
            }
            else
            {
                TextColor = Color.Black;
            }
        }

        public DiffView(string LocalOld, string LocalNew)
        {
            InitializeComponent();

            HandleTheme(newContent);
            HandleTheme(oldContent);

            this.Text = "Diff " + Path.GetFileNameWithoutExtension(LocalNew) + " -> " + Path.GetFileNameWithoutExtension(LocalOld);

            var dmp = new diff_match_patch();
            var diffs = dmp.diff_main(File.ReadAllText(LocalNew), File.ReadAllText(LocalOld), false);

            foreach (var aDiff in diffs)
            {
                switch (aDiff.Operation)
                {
                    case Operation.INSERT:
                        AppendText(newContent, aDiff.Text, Color.Green);
                        break;
                    case Operation.DELETE:
                        AppendText(oldContent, aDiff.Text, Color.Red);
                        break;
                    case Operation.EQUAL:
                        AppendText(oldContent, aDiff.Text, TextColor);
                        AppendText(newContent, aDiff.Text, TextColor);
                        break;
                }
            }

            oldContent.SelectionStart = 0;
            newContent.SelectionStart = 0;
        }
    }
}
