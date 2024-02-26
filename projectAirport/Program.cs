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
        static void SerializeXML(List<Object> lista)
        {
            try
            {
                
                using (FileStream fs = new FileStream("ss.xml", FileMode.OpenOrCreate))
                {
                    foreach (var obj in lista)
                    {
                        
                        XmlSerializer serializer = new XmlSerializer(obj.GetType());
                        serializer.Serialize(fs, obj);
                    }
                }
                Console.WriteLine("XML serialization successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during XML serialization: {ex.Message}");
            }
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
            SerializeXML(objList);
            
            
        }
    }
}

