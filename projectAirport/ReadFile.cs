﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class ReadFile
    {
        static private Dictionary<string, Factory> factoryFunctions = new Dictionary<string, Factory>()
        {
            { "C", new FactoryCrew()},
            { "P", new FactoryPassenger()},
            { "CA", new FactoryCargo()},
            { "CP", new FactoryCargoPlane()},
            { "PP", new FactoryPassengerPlane()}
        };

        public static List<string[]> ReadFileMethod(string filePath, char fieldSeparator = ',')
        {
            List<string[]> readedLines = new List<string[]>();
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
        public static List<Thing> ConvertToObjects(List<string[]> stringList)
        {
            List<Thing> objects = new List<Thing>();
            
            foreach(string[] str in stringList)
            {
                if (factoryFunctions.ContainsKey(str[0]))
                    objects.Add(factoryFunctions[str[0]].makeObjectFromString(str));
            }

            return objects;
        }
    }
}
