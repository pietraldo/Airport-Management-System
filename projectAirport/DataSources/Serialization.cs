using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class Serialization
    {
       
        public static void SerializeJson(List<Thing> lista, string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve, };
            string jsonString = JsonSerializer.Serialize(lista, options);

            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Data written to file: {fileName}");

            DeserializeJson(fileName);
        }
        public static List<Thing>? DeserializeJson(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            var options = new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve, };

            List<Thing>? lista = JsonSerializer.Deserialize<List<Thing>>(jsonString, options);
            return lista;
        }
    }
}
