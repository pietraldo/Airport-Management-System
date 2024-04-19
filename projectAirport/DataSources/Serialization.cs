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
        class MyReferenceResolver : ReferenceResolver
        {
            private uint _referenceCount;
            private readonly Dictionary<string, object> _referenceIdToObjectMap = [];
            private readonly Dictionary<object, string> _objectToReferenceIdMap = new(ReferenceEqualityComparer.Instance);

            public override void AddReference(string referenceId, object value)
            {
                if (!_referenceIdToObjectMap.TryAdd(referenceId, value))
                {
                    throw new JsonException();
                }
            }

            public override string GetReference(object value, out bool alreadyExists)
            {
                if (_objectToReferenceIdMap.TryGetValue(value, out string? referenceId))
                {
                    alreadyExists = true;
                }
                else
                {
                    _referenceCount++;
                    referenceId = _referenceCount.ToString();
                    _objectToReferenceIdMap.Add(value, referenceId);
                    alreadyExists = false;
                }

                return referenceId;
            }

            public override object ResolveReference(string referenceId)
            {
                if (!_referenceIdToObjectMap.TryGetValue(referenceId, out object? value))
                {
                    throw new JsonException();
                }

                return value;
            }
        }
        class MyReferenceHandler : ReferenceHandler
        {
            public MyReferenceHandler() => Reset();
            private ReferenceResolver? _rootedResolver;
            public override ReferenceResolver CreateResolver() => _rootedResolver!;
            public void Reset() => _rootedResolver = new MyReferenceResolver();
        }
        public static void SerializeJson(List<Thing> lista, string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve, };
            //var myReferenceHandler = new MyReferenceHandler();
            //options.ReferenceHandler = myReferenceHandler;

            string jsonString = JsonSerializer.Serialize(lista, options);

            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Data written to file: {fileName}");
            //myReferenceHandler.Reset();


            DeserializeJson(fileName);

        }
        public static List<Thing> DeserializeJson(string fileName)
        {

            string jsonString = File.ReadAllText(fileName);
            var options = new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve, };

            //var myReferenceHandler = new MyReferenceHandler();
            //options.ReferenceHandler = myReferenceHandler;

            List<Thing> lista = JsonSerializer.Deserialize<List<Thing>>(jsonString, options);
            


            //myReferenceHandler.Reset();

            return lista;
        }
    }
}
