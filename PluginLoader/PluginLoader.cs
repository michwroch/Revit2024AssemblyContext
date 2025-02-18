using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader
{
    //plugin load and execute
    public class PluginLoader : MarshalByRefObject
    {
        public void LoadAndRun(string dllPath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            Type pluginType = assembly.GetType("Core.App");
            IApp instance = Activator.CreateInstance(pluginType) as IApp;
            instance.Run();
        }
    }
}
