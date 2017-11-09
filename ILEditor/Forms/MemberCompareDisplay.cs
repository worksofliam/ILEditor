using DiffMatchPatch;
using ILEditor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Forms
{
    public partial class MemberCompareDisplay : Form
    {
        public MemberCompareDisplay(Member MemberA, Member MemberB)
        {
            InitializeComponent();

            this.Text = MemberA.GetLibrary() + "/" + MemberA.GetObject() + "." + MemberA.GetMember() + " -> " + MemberB.GetLibrary() + "/" + MemberB.GetObject() + "." + MemberB.GetMember();

            var dmp = new diff_match_patch();
            var res = dmp.diff_main(File.ReadAllText(MemberA.GetLocalFile()), File.ReadAllText(MemberB.GetLocalFile()), false);
            string html = dmp.diff_prettyHtml(res);
            string style = @"<head>
    <meta charset=" + '"' + "utf-8" + '"' + @" />
      <style>
          body {
                font-family: " + '"' + "Lucida Console" + '"' + @", Monaco, monospace
            }
    </style>
</head> ";
            webBrowser1.DocumentText = style + html;
        }
    }
}
