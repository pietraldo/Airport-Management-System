using NetTopologySuite.Algorithm.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class MakeCommand
    {

        public ConditionsMakeComand[] conditions= Array.Empty<ConditionsMakeComand>();
        public string[] fieldsToDisplay;
        public string objectClass;
        private ParseCommand pc;

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
        }
        public bool Execute()
        {
            if (!CheckObjectClass()) return false;
            if (!CheckDisplayFields()) return false;
            if (!MakeConditions(pc)) return false;

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
                    fieldsToDisplayList.Add(field);
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
