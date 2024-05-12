using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class MakeCommand
    {

        public ConditionsMakeComand[]? conditions;
        public string[] fieldsToDisplay;
        public string objectClass;
        private ParseCommand pc;

        private Dictionary<string, string[]> fields = new Dictionary<string, string[]>() {
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
        }
        public bool Execute()
        {
            if (!fields.ContainsKey(pc.objectClass))
                return false; // nie ma takiej tablicy
            
            if (pc.operation == operations.display)
                MakeDisplayComand();

            objectClass = pc.objectClass;
            MakeConditions(pc);
            return true;
        }

        private bool MakeDisplayComand()
        {
            if (pc.fieldsToDisplay.Count() == 1 && pc.fieldsToDisplay[0] == "*")
            {
                fieldsToDisplay = fields[pc.objectClass];
            }
            else
            {
                for (int i = 0; i < pc.fieldsToDisplay.Count(); i++)
                {
                    if (pc.fieldsToDisplay[i].Contains("."))
                    {
                        string[] s = pc.fieldsToDisplay[i].Split(new char[] { '.' });
                        if (!fields[pc.objectClass].Contains(s[0]))
                        {
                            Console.WriteLine("wrong class field");
                            return false; // zle pole do wyswietlenia
                        }


                    }
                    else if (!fields[pc.objectClass].Contains(pc.fieldsToDisplay[i]))
                    {
                        Console.WriteLine("wrong class field");
                        return false; // zle pole do wyswietlenia
                    }

                }


                fieldsToDisplay = pc.fieldsToDisplay;
            }
            return true;
        }

        private bool MakeConditions(ParseCommand pc)
        {
            if (pc.conditions == null)
                return false;


            conditions = new ConditionsMakeComand[pc.conditions.Length];

            for (int i = 0; i < pc.conditions.Length; i++)
            {
                conditions[i].field = pc.conditions[i].value1;
                conditions[i].value = pc.conditions[i].value2;
                //if (fields[pc.objectClass].Contains(pc.conditions[i].value1) && fields[pc.objectClass].Contains(pc.conditions[i].value2))
                //    return false;
                //if (!fields[pc.objectClass].Contains(pc.conditions[i].value1) && !fields[pc.objectClass].Contains(pc.conditions[i].value2))
                //    return false;
                //if (fields[pc.objectClass].Contains(pc.conditions[i].value1))
                //{
                //    conditions[i].field = pc.conditions[i].value1;
                //    conditions[i].value = pc.conditions[i].value2;
                //}
                //else
                //{
                //    conditions[i].field = pc.conditions[i].value2;
                //    conditions[i].value = pc.conditions[i].value1;
                //}

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
        public string typeOfVariable;
    }

    internal class Comparer<T> where T : IComparable<T>
    {
        public bool Less(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        public bool Equal(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }
    }

}
