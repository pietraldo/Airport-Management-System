using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class Serialization
    {
        public static void SerializeJson(List<Thing> lista, string fileName)
        {
            var options = new JsonSerializerOptions { IncludeFields = true };
            string jsonString = JsonSerializer.Serialize(lista, options);

            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Data writed to file: {fileName}");
        }
    }
}
