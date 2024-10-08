﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using projectAirport.Factory;

namespace projectAirport.DataSources
{
    internal class ReadFile
    {
        static private Dictionary<string, FactoryFromString> factoryFunctions = new Dictionary<string, FactoryFromString>()
        {
            { "C", new FactoryFromStringCrew()},
            { "P", new FactoryFromStringPassenger()},
            { "CA", new FactoryFromStringCargo()},
            { "CP", new FactoryFromStringCargoPlane()},
            { "PP", new FactoryFromStringPassengerPlane()},
            { "AI", new FactoryFromStringAirport()},
            { "FL", new FactoryFromStringFlight()}
        };

        public static List<string[]> ReadFileMethod(string filePath, char fieldSeparator = ',')
        {
            List<string[]> readedLines = new List<string[]>();
            if (File.Exists(filePath))
            {
                List<object> crews = new List<object>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        readedLines.Add(line.Split(fieldSeparator));
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found: " + Environment.CurrentDirectory + "\\" + filePath);
                Environment.Exit(1);
            }
            return readedLines;
        }
        public static List<Thing> ConvertToObjects(List<string[]> stringList,ListDivider divider)
        {
            List<Thing> objects = new List<Thing>();

            foreach (string[] str in stringList)
            {
                if (factoryFunctions.ContainsKey(str[0]))
                {
                    Thing thing = factoryFunctions[str[0]].makeObjectFromString(str, divider);
                    objects.Add(thing);
                    thing.devideList(divider);
                }
                    
            }

            return objects;
        }
    }
}
