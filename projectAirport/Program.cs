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
        static void SerializeJson(List<Object> lista, string fileName)
        {
            string jsonString = "";
            var options = new JsonSerializerOptions{IncludeFields = true };
            foreach (Object obj in lista)
            {
                jsonString += JsonSerializer.Serialize(obj, obj.GetType(), options)+"\n";
            }
           
            File.WriteAllText(fileName, jsonString);
        }
       
        static void DeserializeJson(string filePath)
        {
            List<Object> lista = new List<Object>();

            try
            {
                // Read JSON file content line by line
                string[] lines = File.ReadAllLines(filePath);

                // Deserialize each line into a Crew object
                foreach (string line in lines)
                {
                    if(line.Contains("\"Miles\""))
                    {
                        Object crewMember = JsonSerializer.Deserialize<Passenger>(line);
                        lista.Add(crewMember);
                        JsonDocument jsonDocument = JsonDocument.Parse(line);
                        JsonElement root = jsonDocument.RootElement;

                        // Access properties of the JSON object
                        string name = root.GetProperty("Name").GetString();
                        Console.WriteLine(name);
                    }
                    else
                    {
                        Object crewMember = JsonSerializer.Deserialize<Crew>(line);
                        lista.Add(crewMember);
                    }
                    
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }
            Console.WriteLine();
        }

        static List<Object> ConvertToObjectsList(List<string[]> stringList)
        {
            List<Object> objList = new List<Object>();
            
            foreach(var line in stringList)
            {
                Object obj;
                switch(line[0])
                {
                    case "C":
                        obj = new Crew(line);
                        objList.Add(obj);
                        break;
                    case "P":
                        obj = new Passenger(line);
                        objList.Add(obj);
                        break;
                }
               
            }
            return objList;
        }
        static List<string[]> ReadFile(string filePath, char fieldSeparator=',')
        {
            List<string[]> readedLines= new List<string[]>();
            if (File.Exists(filePath))
            {
                List<Object> crews = new List<Object>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        readedLines.Add(line.Split(fieldSeparator));
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
                Environment.Exit(1);
            }
            return readedLines;
        }


        static void Main(string[] args)
        {
            List<string[]> readedLines=ReadFile("example_data.ftr");
            List<Object> objList = ConvertToObjectsList(readedLines);
            SerializeJson(objList, "objects.json");
            
            DeserializeJson("objects.json");

            //DeserializeXML("people.xml");
        }
    }
}

//static void SerializeXML(List<Crew> lista)
//{
//    XmlSerializer serializer = new XmlSerializer(typeof(List<Crew>));
//    using (TextWriter writer = new StreamWriter("people.xml"))
//    {
//        serializer.Serialize(writer, lista);
//    }


//}

//static void DeserializeXML(string filePath)
//{
//    XmlSerializer serializer = new XmlSerializer(typeof(List<Crew>));
//    using (TextReader reader = new StreamReader(filePath))
//    {
//        var people = (List<Crew>)serializer.Deserialize(reader);

//    }

//}
