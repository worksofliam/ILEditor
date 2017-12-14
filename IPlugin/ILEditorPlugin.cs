using System.Collections.Generic;
using System.Windows.Forms;

namespace ILEditorPlugin
{
    public interface Plugin
    {
        Dictionary<string, string> Config { get; set; }

        void Initialize();

        string Name { get; }
        string DisplayName { get; }

        bool IsTool { get; }
        UserControl OnPluginSelected();

        bool OnMemberDownloading(string Lib, string Spf, string Mbr, string Ext);
        UserControl OnMemberOpening(string Lib, string Spf, string Mbr, string Ext);
    }
}
