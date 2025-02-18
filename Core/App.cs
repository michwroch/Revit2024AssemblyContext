using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public class App: IApp
    {
        public void Run()
        {
			try
			{
                var settings = new JsonSerializerSettings
                {
                    Converters = { new UnixDateTimeConverter { AllowPreEpoch = true } }
                };

                DateTime beforeEpoch = new DateTime(1950, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                string json = JsonConvert.SerializeObject(beforeEpoch, settings);
                Console.WriteLine("Serialized JSON: " + json);

                DateTime deserialized = JsonConvert.DeserializeObject<DateTime>(json, settings);
                MessageBox.Show("Deserialized DateTime: " + deserialized);
            }
			catch (Exception ex)
			{
                MessageBox.Show("Deserialized DateTime: " + ex.Message);
            }
        }
    }
}
