using NetTopologySuite.Algorithm.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projectAirport.SQL
{
    internal class MakeCommand
    {

        public ConditionsMakeComand[] conditions= Array.Empty<ConditionsMakeComand>();
        public string[] fieldsToDisplay;
        public string objectClass;
        public ParseCommand pc;
        public string operation;


        private Dictionary<string, string[]> fieldsToAccess = new Dictionary<string, string[]>() {
            { "Flight", new string[] { "ID", "Origin", "Target", "TakeofTime", "LandingTime", "WorldPosition","AMSL", "Plane" } },
            { "Airport", new string[] { "ID", "Name", "Code", "WorldPosition", "AMSL", "CountryCode"} },
            { "PassengerPlane", new string[]{ "ID", "Serial", "CountryCode", "Model"} },
            { "CargoPlane", new string[]{ "ID", "Serial", "CountryCode", "Model", "MaxLoad"} },
            { "Cargo", new string[]{ "ID", "Weight", "Code", "Description"} },
            { "Passenger", new string[]{ "ID", "Name", "Age", "Phone", "Email", "Class", "Miles"}},
            { "Crew", new string[]{ "ID", "Name", "Age", "Phone", "Email", "Practise", "Role" } },
            { "Plane", new string[]{ "ID", "Model", "Country", "Serial"} },
        };

        public MakeCommand(ParseCommand pc)
        {
            this.pc = pc;
            objectClass = pc.objectClass;
            operation =pc.operation;
        }
        public bool Execute()
        {
            if (!CheckObjectClass()) return false;
            if (!CheckDisplayFields()) return false;
            if (!MakeConditions(pc)) return false;
            if (!CheckSetFieldsAndValues()) return false;

            return true;
        }
        private bool CheckSetFieldsAndValues()
        {
            if (operation != "update") return true;

            Flight f = new Flight();
            Airport ar = new Airport();
            Plane plane = new PassengerPlane();
            f.Plane=plane;
            f.Origin = ar;
            f.Target= ar;

            Thing thing1= f;
            
            switch (objectClass)
            {
                case "Flight":
                    thing1 = f;
                    break;
                case "Airport":
                    thing1= ar;
                    break;
                case "PassengerPlane":
                    thing1= new PassengerPlane();
                    break;
                case "CargoPlane":
                    thing1= new CargoPlane();
                    break;
                case "Cargo":
                    thing1 = new Cargo();
                    break;
                case "Passenger":
                    thing1 = new Passenger();
                    break;
                case "Crew":
                    thing1= new Crew();
                    break;
            }

            foreach (var update in pc.fieldsToSet)
            {
                (bool correctField, string value, string typeofVariable) = thing1.GetFieldAndType(update.field);
                if (!correctField)
                {
                    Console.WriteLine($"Wrong name of field: {update.field}");
                    return false;
                }
                bool valueOk = true;
                switch (typeofVariable)
                {
                    case "int":
                        int a;
                        if (!int.TryParse(update.value, out a))
                            valueOk = false;
                        break;
                    case "Single":
                        Single b;
                        if (!Single.TryParse(update.value, out b))
                            valueOk = false;
                        break;
                    case "float":
                        float c;
                        if (!float.TryParse(update.value, out c))
                            valueOk = false;
                        break;
                    case "struct":
                    case "uint":
                        uint d;
                        if (!uint.TryParse(update.value, out d))
                            valueOk = false;
                        break;
                    case "DateTime":
                        DateTime e;
                        if (!DateTime.TryParse(update.value, out e))
                            valueOk = false;
                        break;
                }
                if (!valueOk)
                {
                    Console.WriteLine($"Wrong value({update.value}) of field: {update.field}");
                    return false;
                }
            }
            return true;
        }
        private bool CheckObjectClass()
        {
            if (fieldsToAccess.ContainsKey(pc.objectClass)) return true;
            Console.WriteLine($"Table: {pc.objectClass} does not exists");
            return false;
        }
        private bool CheckDisplayFields()
        {
            if (pc.operation != "display") return true;

            List<string> fieldsToDisplayList = new List<string>();

            for (int i = 0; i < pc.fieldsToDisplay.Count(); i++)
            {
                string field = pc.fieldsToDisplay[i].Split(".")[0];

                if (field == "*")
                {
                    fieldsToDisplayList.AddRange(fieldsToAccess[objectClass]);
                }
                else if (fieldsToAccess[objectClass].Contains(field))
                {
                    fieldsToDisplayList.Add(pc.fieldsToDisplay[i]);
                }
                else
                {
                    Console.WriteLine("Wrong class field");
                    return false;
                }
            }
            fieldsToDisplay = fieldsToDisplayList.ToArray();
            return true;
        }
        private bool MakeConditions(ParseCommand pc)
        {
            if (pc.operation == "add") return true;
            if (pc.conditions.Length == 0) return true;

            conditions = new ConditionsMakeComand[pc.conditions.Length];

            for (int i = 0; i < pc.conditions.Length; i++)
            {
                string value1 = pc.conditions[i].value1.Split(".")[0];
                string value2 = pc.conditions[i].value2.Split(".")[0];

                if (fieldsToAccess[objectClass].Contains(value1) && fieldsToAccess[objectClass].Contains(value2))
                {
                    Console.WriteLine($"Error in condition: {conditions[i]}, Can't have to fields");
                    return false;
                }
                if (!fieldsToAccess[objectClass].Contains(value1) && !fieldsToAccess[objectClass].Contains(value2))
                {
                    Console.WriteLine($"Error in condition: {conditions[i]}, Can't have to values");
                    return false;
                }
                if (fieldsToAccess[objectClass].Contains(value1))
                {
                    conditions[i].field = pc.conditions[i].value1;
                    conditions[i].value = pc.conditions[i].value2;
                }
                else
                {
                    conditions[i].field = pc.conditions[i].value2;
                    conditions[i].value = pc.conditions[i].value1;
                }
                
                conditions[i].andOr = pc.conditions[i].andOr;
                conditions[i].comparer = pc.conditions[i].compare;
            }

            return true;
        }
    }
    internal struct ConditionsMakeComand
    {
        public string field;
        public string value;
        public string andOr;
        public string comparer;
    }

}
