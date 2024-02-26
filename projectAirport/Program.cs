using projectAirport;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void SerializeJson(List<Crew> lista)
        {
            string fileName = "JsonSerialization.json";
            string jsonString = "";
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            foreach (Crew cr in lista)
            {
                jsonString += JsonSerializer.Serialize(cr, options)+"\n";
            }
           
            File.WriteAllText(fileName, jsonString);
        }
        static void DeserializeJson(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                // Read and display lines from the file until the end of the file is reached
                while ((line = reader.ReadLine()) != null)
                {

                    Crew? cr = JsonSerializer.Deserialize<Crew>(line)!;
                }
            }

        }
        static void SerializeXML(List<Crew> lista)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Crew>));
            using (TextWriter writer = new StreamWriter("people.xml"))
            {
                serializer.Serialize(writer, lista);
            }


        }
       
        static void DeserializeXML(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Crew>));
            using (TextReader reader = new StreamReader(filePath))
            {
                var people = (List<Crew>)serializer.Deserialize(reader);

            }

        }
        static void ReadFile(string filePath, char fieldSeparator=',')
        {
            if (File.Exists(filePath))
            {
                List<Crew> crews = new List<Crew>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    // Read and display lines from the file until the end of the file is reached
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] separated=line.Split(fieldSeparator);
                        if (separated[0] != "C") continue;

                        Crew cr = new Crew(separated);
                        crews.Add(cr);
                        Console.WriteLine(line);
                    }
                }
                SerializeJson(crews);
                SerializeXML(crews);
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
                Environment.Exit(1);
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ReadFile("example_data.ftr");
            DeserializeJson("JsonSerialization.json");
            DeserializeXML("people.xml");
        }
    }
}