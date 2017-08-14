using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Classes;
using FastColoredTextBoxNS;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ILEditor.UserTools
{
    public enum ILELanguage
    {
        None,
        CPP
    }

    public partial class SourceEditor : UserControl
    {
        private FastColoredTextBox mybox = null;
        //private CurrentLanguage Language;

        public SourceEditor(String LocalFile, ILELanguage Language = ILELanguage.None)
        {
            InitializeComponent();
            
            //https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting

            mybox = new FastColoredTextBox();
            mybox.Dock = DockStyle.Fill;

            switch (Language) {
                case ILELanguage.CPP:
                    mybox.TextChanged += SetCPP;
                    break;
            }

            mybox.Text = File.ReadAllText(LocalFile);
            this.Controls.Add(mybox);
        }

        #region Styles
        private static readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        private static readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private static readonly Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        private static readonly Style BoldStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);
        private static readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        #endregion

        private void SetCPP(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(BlueStyle, @"\b(and|and_eq|asm|auto|bitand|bitor|bool|break|case|catch|char|compl|const|const_cast|continue|default|delete|do|double|dynamic_cast|else|exit|explicit|export|extern|extern|FALSE|float|for|friend|goto|if|inline|int|long|mutable|namespace|new|not|not_eq|operator|or|or_eq|private|protected|public|register|reinterpret_cast|short|signed|sizeof|static|static_cast|switch|template|this|throw|TRUE|try|typedef|typeid|typename|union|unsigned|using|virtual|void|volatile|wchar_t|while|xor|xor_eq)\b");
            e.ChangedRange.SetStyle(MagentaStyle, @"#\b(include|pragma|if|else|elif|ifndef|ifdef|endif|undef|define|line|error)\b", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(MagentaStyle, @"\*", RegexOptions.Singleline);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block 
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

    }
}
