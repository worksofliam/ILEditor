using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ILEditorPlugin;

namespace ILEditor.Classes
{
    class Plugins
    {
        public static List<IPlugin> PluginList { get; set; }

        public static void LoadPlugins(string Dir)
        {
            PluginList = new List<IPlugin>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists(Dir))
            {
                string[] files = Directory.GetFiles(Dir);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IPlugin);
            Assembly[] assem = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly Plugin in assem)
            {
                foreach(Type type in Plugin.GetTypes())
                {
                    Type mytype = type.GetInterface("IPlugin");
                    if (mytype != null)
                    {
                        PluginList.Add((IPlugin)Activator.CreateInstance(type));
                    }
                }
            }
        }

        public static IPlugin[] GetTools()
        {
            return Plugins.PluginList.Where(p => p.IsTool == true).ToArray();
        }

        public static IPlugin GetPlugin(string Name)
        {
            return Plugins.PluginList.Where(p => p.Name == Name).FirstOrDefault();
        }
    }
}
