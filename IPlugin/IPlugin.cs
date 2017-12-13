using System.Windows.Forms;

namespace ILEditorPlugin
{
    public interface IPlugin
    {
        string Name { get; }
        string DisplayName { get; }

        bool IsTool { get; }
        UserControl OnPluginSelected();
        UserControl OnMemberOpening(string Lib, string Spf, string Mbr, string Ext);
    }
}
