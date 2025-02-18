using Common;
using CSScriptLibrary;
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
            using (var helper = new AsmHelper(assembly))
            {
                IApp app = helper.CreateObject("Core.App") as IApp;
                app.Run();
            }
        }
    }
}
