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
        private static string pathFileFTR = "example_data.ftr";
        private static string pathFileJson = "things.json";

        static void Main(string[] args)
        {
            List<string[]> readedLines = ReadFile.ReadFileMethod(pathFileFTR);
            List<Thing> things = ReadFile.ConvertToObjects(readedLines);
            Serialization.SerializeJson(things, pathFileJson);
        }
    }
}

