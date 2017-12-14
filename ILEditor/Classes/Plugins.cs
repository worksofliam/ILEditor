using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ILEditorPlugin;
using System.Windows.Forms;

namespace ILEditor.Classes
{
    class Plugins
    {
        public static List<Plugin> PluginList { get; set; }

        public static void LoadPlugins(string Dir)
        {
            PluginList = new List<Plugin>();

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

            Type interfaceType = typeof(Plugin);
            Assembly[] assem = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly Plugin in assem)
            {
                foreach(Type type in Plugin.GetTypes())
                {
                    Type mytype = type.GetInterface("Plugin");
                    if (mytype != null)
                    {
                        PluginList.Add((Plugin)Activator.CreateInstance(type));
                    }
                }
            }

            foreach (Plugin Plugin in PluginList)
            {
                Plugin.Config = new Dictionary<string, string>()
                {
                    { "system", IBMi.CurrentSystem.GetValue("system") },
                    { "username", IBMi.CurrentSystem.GetValue("username") },
                    { "pluginDir", Program.PLUGINSDIR + @"\" + Plugin.Name }
                };
                Plugin.Initialize();
            }
        }

        public static Plugin[] GetTools()
        {
            return Plugins.PluginList.Where(p => p.IsTool == true).ToArray();
        }

        public static Plugin GetPlugin(string Name)
        {
            return Plugins.PluginList.Where(p => p.Name == Name).FirstOrDefault();
        }

        public static bool OnMemberDownloading(string Lib, string Spf, string Mbr, string Ext)
        {
            bool result;

            foreach (Plugin Plugin in Plugins.PluginList)
            {
                result = Plugin.OnMemberDownloading(Lib, Spf, Mbr, Ext);
                if (result == true)
                    return true;
            }

            return false;
        }

        public static UserControl OnMemberOpening(string Lib, string Spf, string Mbr, string Ext)
        {
            UserControl result = null;

            foreach (Plugin Plugin in Plugins.PluginList)
            {
                result = Plugin.OnMemberOpening(Lib, Spf, Mbr, Ext);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
