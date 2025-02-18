using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CSScriptLibrary;
using Autodesk.Revit.DB;
using System.IO;
using Common;
using System.Runtime.Remoting.Messaging;

namespace plugin_revit_api
{
    public class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //TEN KOD NIE PÓJDZIE BO REVIT MA NEWTONSOFTA 13.0.1 A NIE 13.0.3
            #region notworking
            //var settings = new JsonSerializerSettings
            //{
            //    Converters = { new UnixDateTimeConverter { AllowPreEpoch = true } }
            //};

            //DateTime beforeEpoch = new DateTime(1950, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //string json = JsonConvert.SerializeObject(beforeEpoch, settings);
            //Console.WriteLine("Serialized JSON: " + json);

            //DateTime deserialized = JsonConvert.DeserializeObject<DateTime>(json, settings);

            //TaskDialog.Show("Hello", $"Deserialized DateTime: {deserialized}");
            #endregion

            string dllDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var dllPath = Path.Combine(dllDirectory, @"Core.dll");
            // Tworzenie nowej AppDomain dla pluginu
            AppDomainSetup setup = new AppDomainSetup
            {
                ApplicationBase = Path.GetDirectoryName(dllPath),
                ShadowCopyFiles = "true",  // Pozwala na odświeżenie DLL bez restartu aplikacji
            };
            AppDomain pluginDomain = AppDomain.CreateDomain("PluginDomain", null, setup);
            // Tworzenie instancji klasy PluginLoader w nowej domenie
            var loader = (PluginLoader.PluginLoader)pluginDomain.CreateInstanceAndUnwrap(
                typeof(PluginLoader.PluginLoader).Assembly.FullName,
                typeof(PluginLoader.PluginLoader).FullName);

            // Uruchomienie kodu w odizolowanym kontekście
            loader.LoadAndRun(dllPath);

            return Result.Succeeded;
        }
    }
}
