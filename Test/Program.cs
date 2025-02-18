using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var settings = new JsonSerializerSettings
            {
                Converters = { new UnixDateTimeConverter { AllowPreEpoch = true } }
            };

            DateTime beforeEpoch = new DateTime(1950, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            string json = JsonConvert.SerializeObject(beforeEpoch, settings);
            Console.WriteLine("Serialized JSON: " + json);

            DateTime deserialized = JsonConvert.DeserializeObject<DateTime>(json, settings);
            Console.WriteLine($"Deserialized DateTime: {deserialized}");
        }
    }
}
