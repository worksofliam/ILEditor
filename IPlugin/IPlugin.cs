using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditorPlugin
{
    public interface IPlugin
    {
        string Name { get; }
        string DisplayName { get; }

        bool IsTool { get; }
        void OnPluginSelected();
    }
}
