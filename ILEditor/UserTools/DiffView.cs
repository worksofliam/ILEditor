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
using DiffMatchPatch;
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

        public Color textColor = Color.Black;
        public void HandleTheme(RichTextBox box)
        {
            bool DarkMode = (Program.Config.GetValue("darkmode") == "true");

            box.Font = new Font(IBMi.CurrentSystem.GetValue("FONT"), float.Parse(IBMi.CurrentSystem.GetValue("ZOOM")));
            box.WordWrap = false;

            if (DarkMode)
            {
                box.BackColor = Color.FromArgb(30, 30, 30);
                textColor = Color.White;
            }
            else
            {
                textColor = Color.Black;
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

            foreach (Diff aDiff in diffs)
            {
                switch (aDiff.operation)
                {
                    case Operation.INSERT:
                        AppendText(newContent, aDiff.text, Color.Green);
                        break;
                    case Operation.DELETE:
                        AppendText(oldContent, aDiff.text, Color.Red);
                        break;
                    case Operation.EQUAL:
                        AppendText(oldContent, aDiff.text, textColor);
                        AppendText(newContent, aDiff.text, textColor);
                        break;
                }
            }

            oldContent.SelectionStart = 0;
            newContent.SelectionStart = 0;
        }
    }
}
