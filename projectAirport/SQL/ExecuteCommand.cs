using DynamicData;
using Microsoft.Win32.SafeHandles;
using ReactiveUI;
using System;
using System.Collections;
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
        public string objectClass;
        DataSource data;
        public string operation;
        public string[,] toPrint;
        public (string field, string value)[] fieldsToSet;
        public ExecuteCommand(MakeCommand mc, DataSource dataSource)
        {
            this.mc = mc;
            objectClass=mc.objectClass;
            data = dataSource;
            operation = mc.operation;
            fieldsToSet = mc.pc.fieldsToSet;
        }

        public bool Execute()
        {
            List<Thing> thingList = new List<Thing>();

            if (!SetObjectList(ref thingList)) return false;
            if (!FilterList(ref thingList)) return false;

            if(operation=="display")
                if(!ChooseFieldsToPrint(ref thingList)) return false;
            if (operation == "delete")
                if (!DeleteObjects(ref thingList)) return false;
            if (operation == "update")
                if (!UpdateObjects(ref thingList)) return false;
            if (operation == "add")
                if (!AddObjects()) return false;

            return true;
        }

        private bool UpdateObjects(ref List<Thing> list)
        { 
            foreach (Thing thing in list)
            {
                foreach (var update in fieldsToSet)
                {
                    if (!thing.SetField(update.field, update.value, data))
                    {
                        Console.WriteLine($"Error in setting field: {update.field}={update.value}");
                        return false;
                    }
                }
            }

            Console.WriteLine($"{list.Count} rows affected");
            return true;
        }
        private bool AddObjects()
        {
            Thing thing1 = new Flight();

            switch (objectClass)
            {
                case "Flight":
                    thing1 = new Flight();
                    break;
                case "Airport":
                    thing1 = new Airport();
                    break;
                case "PassengerPlane":
                    thing1 = new PassengerPlane();
                    break;
                case "CargoPlane":
                    thing1 = new CargoPlane();
                    break;
                case "Cargo":
                    thing1 = new Cargo();
                    break;
                case "Passenger":
                    thing1 = new Passenger();
                    break;
                case "Crew":
                    thing1 = new Crew();
                    break;
            }
            foreach (var update in fieldsToSet)
            {
                if (!thing1.SetField(update.field, update.value, data))
                {
                    Console.WriteLine($"Error in setting field: {update.field}={update.value}");
                    return false;
                }
            }

            thing1.devideList(data.divider);
            data.thingList.Add(thing1);

            Console.WriteLine($"Added row");
            return true;
        }

        private bool DeleteObjects(ref List<Thing> list)
        {
            foreach(var thing in list)
                data.thingList.Remove(thing);

            //TODO: i want to delete it from whole aplication

            Console.WriteLine($"{list.Count} rows affected");

            // re divide all object to proper lists
            ListDivider divider = new ListDivider();
            data.divider = divider;
            foreach(var thing in data.thingList)
                thing.devideList(divider);


            return true;
        }

        private bool SetObjectList(ref List<Thing> list)
        {
            switch (objectClass)
            {
                case "Flight":
                    foreach (var a in data.divider.Flights)
                        list.Add(a);
                    break;
                case "Airport":
                    foreach (var a in data.divider.Airports)
                        list.Add(a);
                    break;
                case "PassengerPlane":
                    foreach (var a in data.divider.PassengerPlanes)
                        list.Add(a);
                    break;
                case "CargoPlane":
                    foreach (var a in data.divider.CargoPlanes)
                        list.Add(a);
                    break;
                case "Cargo":
                    foreach (var a in data.divider.Cargos)
                        list.Add(a);
                    break;
                case "Passenger":
                    foreach (var a in data.divider.Passengers)
                        list.Add(a);
                    break;
                case "Crew":
                    foreach (var a in data.divider.Crews)
                        list.Add(a);
                    break;
                default:
                    Console.WriteLine("Wrong class");
                    return false;

            }
            return true;
        }

        private bool FilterList(ref List<Thing> list)
        {
            List<Thing> listFiltered = new List<Thing>();

            if (mc.conditions.Count() == 0) return true;

            for (int i = 0; i < list.Count; i++)
            {
                bool add = false;
                for (int j = 0; j < mc.conditions.Length; j++)
                {
                    string value = mc.conditions[j].value;
                    (bool correct, string field, string typeOfVariable) = list[i].GetFieldAndType(mc.conditions[j].field);
                    if (!correct)
                    {
                        Console.WriteLine($"Wrong Condition Field {mc.conditions[j].field}");
                        return false;
                    }

                    bool res = Comparer.Compare(field, value, mc.conditions[j].comparer, typeOfVariable);

                    add = (mc.conditions[j].andOr == "and") ? add && res : add || res;
                }
                if (add)
                {
                    listFiltered.Add(list[i]);
                }
            }
            list = listFiltered;

            return true;
        }
        private bool ChooseFieldsToPrint(ref List<Thing> objects)
        {
            toPrint = new string[objects.Count, mc.fieldsToDisplay.Length];
            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = 0; j < mc.fieldsToDisplay.Length; j++)
                {
                    string[] fields = mc.fieldsToDisplay[j].Split(".");

                    string objectField = "";

                    (bool correct, string field, string typeOfVariable) = objects[i].GetFieldAndType(mc.fieldsToDisplay[j]);
                    if (!correct)
                    {
                        Console.WriteLine($"Wrong Condition Field {mc.conditions[j].field}");
                        return false;
                    }

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
