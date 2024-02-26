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
            string jsonString = "";
            var options = new JsonSerializerOptions { IncludeFields = true };
            foreach (Thing obj in lista)
            {
                jsonString += JsonSerializer.Serialize(obj, options) + "\n";
            }

            File.WriteAllText(fileName, jsonString);
        }
    }
}
