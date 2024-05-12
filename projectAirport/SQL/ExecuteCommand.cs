using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class ExecuteCommand
    {
        public MakeCommand mc;
        DataSource data;
        public string[,] toPrint;
        public ExecuteCommand(MakeCommand mc, DataSource dataSource)
        {
            this.mc = mc;
            data = dataSource;
        }

        public bool Execute()
        {
            List<Thing> thingList = new List<Thing>();
            switch (mc.objectClass)
            {
                case "Flight":
                    foreach(var a in data.divider.Flights)
                        thingList.Add(a);
                    break;
                case "Airport":
                    foreach(var a in data.divider.Airports)
                        thingList.Add(a);
                    break;
                case "PassengerPlane":
                    foreach(var a in data.divider.PassengerPlanes)
                        thingList.Add(a);
                    break;
                case "CargoPlane":
                    foreach(var a in data.divider.CargoPlanes)
                        thingList.Add(a);
                    break;
                case "Cargo":
                    foreach(var a in data.divider.Cargos)
                        thingList.Add(a);
                    break;
                case "Passenger":
                    foreach(var a in data.divider.Passengers)
                        thingList.Add(a);
                    break;
                case "Crew":
                    foreach(var a in data.divider.Crews)
                        thingList.Add(a);
                    break;
                   
            }
            return ChooseFieldsToPrintFlight(FilterFlights(thingList));
        }

        private List<Thing> FilterFlights(List<Thing> flights)
        {

            List<Thing> flightsPassed = new List<Thing>();
            if (mc.conditions != null)
            {
                for (int i = 0; i < flights.Count; i++)
                {
                    bool add = false;
                    for (int j = 0; j < mc.conditions.Length; j++)
                    {
                        string value = mc.conditions[j].value;
                        (bool correct, string field, string typeOfVariable) = flights[i].GetFieldAndType(mc.conditions[j].field);
                        if (!correct) continue;
                        
                        bool res = Comparer.Compare(field,value , mc.conditions[j].comparer, typeOfVariable);
                        
                        add = (mc.conditions[j].andOr == "and") ? add && res : add || res;
                    }
                    if (add)
                    {
                        flightsPassed.Add(flights[i]);
                    }
                }
            }
            else
            {
                flightsPassed = flights;
            }

            return flightsPassed;
        }
        private bool ChooseFieldsToPrintFlight(List<Thing> objects)
        {
            toPrint = new string[objects.Count, mc.fieldsToDisplay.Length];
            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = 0; j < mc.fieldsToDisplay.Length; j++)
                {
                    string[] fields = mc.fieldsToDisplay[j].Split(".");

                    string objectField = "";

                    (bool correct, string field, string typeOfVariable) = objects[i].GetFieldAndType(mc.fieldsToDisplay[j]);
                    if (!correct) return false;
                    objectField = field;
                    

                    toPrint[i, j] = objectField;
                }
            }
            return true;
        }

    }

    internal class Comparer
    {
        public static bool Compare(string s1, string s2, string comparer, string typeofVariable)
        {
            switch (typeofVariable)
            {
                case "int":
                    return MakeComparation(int.Parse(s1), int.Parse(s2), comparer);
                case "Single":
                    return MakeComparation(Single.Parse(s1), Single.Parse(s2), comparer);
                case "float":
                    return MakeComparation(float.Parse(s1), float.Parse(s2), comparer);
                case "string":
                    return MakeComparation(s1,s2, comparer);
                case "uint":
                    return MakeComparation(uint.Parse(s1), uint.Parse(s2), comparer);
                case "DateTime":
                    return MakeComparation(DateTime.Parse(s1), DateTime.Parse(s2), comparer);
            }

            return false;
        }
        private static bool MakeComparation<T>(T field, T value, string comparer) where T : IComparable
        {
            switch (comparer)
            {
                case ">":
                    return field.CompareTo(value) > 0;
                case "<":
                    return field.CompareTo(value) < 0;
                case ">=":
                    return field.CompareTo(value) >= 0;
                case "<=":
                    return field.CompareTo(value) <= 0;
                case "=":
                    return field.CompareTo(value) == 0;
                case "!=":
                    return field.CompareTo(value) != 0;
            }
            return false;
        }
    }

}
